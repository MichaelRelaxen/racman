using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace racman
{
    class Autosplitter
    {
        static byte[] junk = new byte[] { 0x63, 0x9a, 0x4d, 0xa2, 0x66, 0x19, 0xaa, 0xff, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        static System.IO.MemoryMappedFiles.MemoryMappedFile mmfFile;
        static System.IO.MemoryMappedFiles.MemoryMappedViewStream mmfStream;

        public static int planetIndex;

        public static void InitializeAutosplitter()
        {
            /* mmfFile = System.IO.MemoryMappedFiles.MemoryMappedFile.CreateNew("racman-autosplitter", 21);
             {
                 mmfStream = mmfFile.CreateViewStream();
                 {
                     BinaryWriter writer = new BinaryWriter(mmfStream);

                     int destinationPlanetSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.destination_planet, 4, (value) => {
                         junk[8] = value[0];
                         writer.Seek(0, SeekOrigin.Begin);
                         writer.Write(junk, 0, 21);
                     });

                     int currentPlanetSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.current_planet, 4, (value) =>
                     {
                         planetIndex = BitConverter.ToInt32(value, 0);
                         writer.Seek(0, SeekOrigin.Begin);
                         junk[9] = value[0];

                         writer.Write(junk, 0, 21);
                     });

                     int playerStateSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.player_state, 4, (value) =>
                     {
                         junk[10] = value[0];
                         junk[11] = value[1];
                         writer.Seek(0, SeekOrigin.Begin);
                         writer.Write(junk, 0, 21);
                     });

                     int planetFrameCountSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, 0xA10710, 4, (value) =>
                     {
                         junk[12] = value[0];
                         junk[13] = value[1];
                         junk[14] = value[2];
                         junk[15] = value[3];

                         writer.Seek(0, SeekOrigin.Begin);

                         writer.Write(junk, 0, 21);
                     });

                     int gameStateSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, 0x00A10708, 4, (value) =>
                     {
                         junk[16] = value[0];
                         junk[17] = value[1];
                         junk[18] = value[2];
                         junk[19] = value[3];
                         writer.Seek(0, SeekOrigin.Begin);
                         writer.Write(junk, 0, 21);
                     });

                     int loadingScreenSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, 0x9645C8, 4, (value) =>
                     {
                         junk[20] = value[0];
                         writer.Seek(0, SeekOrigin.Begin);
                         writer.Write(junk, 0, 21);
                     });

                     int playerCoordsSubID = ((Ratchetron)func.api).SubMemory(AttachPS3Form.pid, rac1.player_coords, 8, (value) =>
                     {
                         Console.WriteLine($"X: {BitConverter.ToSingle(value, 0)}, Y: {BitConverter.ToSingle(value, 4)}");
                         junk[0] = value[0];
                         junk[1] = value[1];
                         junk[2] = value[2];
                         junk[3] = value[3];
                         junk[4] = value[4];
                         junk[5] = value[5];
                         junk[6] = value[6];
                         junk[7] = value[7];
                         writer.Seek(0, SeekOrigin.Begin);
                         writer.Write(junk, 0, 21);
                     });
                 }
             }*/
        }
    }
}
