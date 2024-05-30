using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Threading;

namespace racman
{
    public interface IAutosplitterAvailable
    {
        IEnumerable<(uint addr, uint size)> AutosplitterAddresses { get; }
    }

    public class AutosplitterHelper
    {
        public static int mmfAddressBytes = 128;
        public static int mmfConfigBytes = 256;
        public static int mmfSize = mmfAddressBytes + mmfConfigBytes;

        MemoryMappedFile mmfFile;
        MemoryMappedViewStream mmfStream;
        BinaryWriter writer;

        Variables variables;
        Timer UpdatingTimer;

        List<int> subscriptionIDs = new List<int>();

        IGame currentGame = null;

        public bool IsRunning { get; private set; } = false;

        public AutosplitterHelper()
        {
            mmfFile = MemoryMappedFile.CreateOrOpen("racman-autosplitter", mmfSize);
            mmfStream = mmfFile.CreateViewStream();
            writer = new BinaryWriter(mmfStream);
        }

        public BinaryWriter GetWriter()
        {
            return writer;
        }

        public Mutex GetWriteLock()
        {
            return writeLock;
        }

        /// <summary>
        ///  Destructor/finalizer, called when the object is destroyed, on application close, garbage collection, etc.
        /// </summary>
        ~AutosplitterHelper()
        {
            if (writer != null && IsRunning)
            {
                this.Stop();
            }
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                throw new InvalidOperationException("Must start autosplitter before stopping.");
            }

            IsRunning = false;
            mmfStream.Close();
            writer?.Close();
            writer = null;

            if (currentGame == null) return;

            foreach (int subID in subscriptionIDs)
            {
                this.currentGame.api.ReleaseSubID(subID);
            }
        }

        private static Mutex writeLock = new Mutex();
        private void WriteToMemory(int offset, byte[] value)
        {
            writeLock.WaitOne();

            if (writer != null)
            {
                writer.Seek(offset, SeekOrigin.Begin);
                writer.Write(value, 0, value.Length);
            }

            writeLock.ReleaseMutex();
        }

        // Probably will only be used for UYA
        public void WriteConfig(byte[] value)
        {
            writeLock.WaitOne();

            if (writer != null)
            {
                writer.Seek(mmfAddressBytes, SeekOrigin.Begin);
                writer.Write(value, 0, value.Length);
                writer.Write(Enumerable.Repeat((byte)0, mmfConfigBytes - value.Length).ToArray());
            }

            writeLock.ReleaseMutex();
        }

        public int StartAutosplitterForGame(IGame game)
        {
            bool gameSupportsAutosplitterWithAddresses = game is IAutosplitterAvailable;
            bool gameSupportsAutosplitterWithValues = game.AutosplitterValues != null;
            if (!gameSupportsAutosplitterWithAddresses && !gameSupportsAutosplitterWithValues)
            {
                throw new NotSupportedException("This game doesn't support an autosplitter yet.");
            }

            var currentGame = game;
            var autosplitter = game as IAutosplitterAvailable;
            var autosplitterAddr = game as IAutosplitterAvailable;

            int pos = 0;

            if (gameSupportsAutosplitterWithAddresses)
            {
                foreach (var (addr, size) in autosplitter.AutosplitterAddresses)
                {
                    var _pos = pos; // If you can think of a better way to do this please tell me

                    // Write the initial value to the memory. This is necessary because the autosplitter will only
                    // trigger when the value changes. So at the start of the game all values will be 0;
                    var initialValue = game.api.ReadMemory(game.api.getCurrentPID(), addr, size).Reverse().ToArray();
                    WriteToMemory(_pos, initialValue);

                    subscriptionIDs.Add(game.api.SubMemory(game.api.getCurrentPID(), addr, size, (value) =>
                    {
                        WriteToMemory(_pos, value);
                    }));
                    pos += (int)size;
                }
            }

            /*if (gameSupportsAutosplitterWithValues)
            {
                var _pos = pos;

                if (variables == null)
                {
                    Console.WriteLine("Creating new variables");
                    variables = new Variables(game, _pos, WriteToMemory);
                    UpdatingTimer = new Timer((e) => variables.WriteValuesToMemory(IsRunning), null, 0, 1000 / 120);
                }
            }*/

            IsRunning = true;

            return pos;
        }
    }
    public class Variables
    {
        private uint ListDimensions;
        IGame Game;
        private List<int> Pos = new List<int>();

        private Action<int, byte[]> WriteToMemory;

        public Variables(IGame game, int pos, Action<int, byte[]> writeToMemory)
        {
            Game = game;
            IEnumerable<(uint addr, uint size)> values = Game.AutosplitterValues;
            ListDimensions = (uint)values.Count();
            WriteToMemory = writeToMemory;
            
            foreach (var value in values)
            {
                Pos.Add(pos);
                pos += (int)value.size;
            }
            //Console.WriteLine("AAAAAAAAAAA"+this.GetHashCode());
        }

        public void WriteValuesToMemory(bool IsRunning)
        {
            if (!IsRunning)
            {
                return;
            }

            IEnumerable<(uint addr, uint size)> values = Game.AutosplitterValues;
            if (values.Count() != ListDimensions)
            {
                throw new InvalidOperationException("The number of values in the autosplitter has changed.");
            }

            //Console.WriteLine(values.ElementAt(0).addr);
            //Console.WriteLine("A " + Values.ElementAt(0).addr.addr);

            for (int i = 0; i < values.Count(); i++)
            {
                //byte[] value = BitConverter.GetBytes(Values.ElementAt(i).addr);
                //Console.WriteLine($"Writing {Values.ElementAt(i).addr} to ");
                //WriteToMemory(Pos[i], value);
                //Console.WriteLine($"Writing {Values.ElementAt(i)} to {Pos[i]}");
                // print: Values.ElementAt(i).addr
                /*GCHandle objHandle = GCHandle.Alloc(Values.ElementAt(i).addr, GCHandleType.WeakTrackResurrection);
                Int64 address = GCHandle.ToIntPtr(objHandle).ToInt64();
                Console.WriteLine(address);*/
                //Console.WriteLine(Values.ElementAt(i).addr);
            }
        }
    }
}
