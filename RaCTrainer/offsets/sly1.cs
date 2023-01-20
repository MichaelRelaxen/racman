using System;
using System.Collections.Generic;
using System.Linq;

namespace racman
{
    public class Sly1Addresses : IAddresses
    {
        public uint slyObjectPointer => 0x428D04;
        public uint boltCount => throw new NotImplementedException();

        public uint playerCoords => throw new NotImplementedException();

        public uint playerHealth => throw new NotImplementedException();

        public uint inputOffset => throw new NotImplementedException();

        public uint analogOffset => throw new NotImplementedException();

        public uint loadPlanet => throw new NotImplementedException();

        public uint currentPlanet => throw new NotImplementedException();

        public uint levelFlags => throw new NotImplementedException();

        public uint miscLevelFlags => throw new NotImplementedException();

        public uint infobotFlags => throw new NotImplementedException();

        public uint moviesFlags => throw new NotImplementedException();

        public uint unlockArray => throw new NotImplementedException();
    }

    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
    }

    public class Sly1 : IGame
    {
        public static Sly1Addresses addr = new Sly1Addresses();

        public Sly1(Ratchetron api) : base(api)
        {

        }

        public void PauseGame()
        {
            api.WriteMemory(pid, 0x3E62C0, 0);
        }

        public void UnpauseGame()
        {
            api.WriteMemory(pid, 0x3E62C0, 1);
        }

        public uint GetSlyObjectAddress()
        {
            return BitConverter.ToUInt32(api.ReadMemory(pid, addr.slyObjectPointer, 4).Reverse().ToArray(), 0);
        }

        public Vector3 GetPosition()
        {
            Vector3 position = new Vector3();

            position.x = BitConverter.ToSingle(api.ReadMemory(pid, GetSlyObjectAddress() + 0x100, 4).Reverse().ToArray(), 0);
            position.y = BitConverter.ToSingle(api.ReadMemory(pid, GetSlyObjectAddress() + 0x104, 4).Reverse().ToArray(), 0);
            position.z = BitConverter.ToSingle(api.ReadMemory(pid, GetSlyObjectAddress() + 0x108, 4).Reverse().ToArray(), 0);

            return position;
        }

        public void SetPosition(Vector3 position) {
            PauseGame();

            api.WriteMemory(pid, GetSlyObjectAddress() + 0x150, BitConverter.GetBytes(0.0f).Reverse().ToArray());
            api.WriteMemory(pid, GetSlyObjectAddress() + 0x154, BitConverter.GetBytes(0.0f).Reverse().ToArray());
            api.WriteMemory(pid, GetSlyObjectAddress() + 0x158, BitConverter.GetBytes(0.0f).Reverse().ToArray());

            api.WriteMemory(pid, GetSlyObjectAddress() + 0x100, BitConverter.GetBytes(position.x).Reverse().ToArray());
            api.WriteMemory(pid, GetSlyObjectAddress() + 0x104, BitConverter.GetBytes(position.y).Reverse().ToArray());
            api.WriteMemory(pid, GetSlyObjectAddress() + 0x108, BitConverter.GetBytes(position.z).Reverse().ToArray());

            UnpauseGame();
        }

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
