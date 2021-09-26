using System;
using System.Linq;

namespace racman
{
    public class RaC3Addresses : IAddresses
    {
        // Input stuff
        public uint inputOffset => 0xD99370;
        public uint analogOffset => 0xD9954C;

        // State stuff
        public uint currentPlanet => 0xC1E438;
        public uint destinationPlanet => 0xEE9314;
        public uint playerState => 0xDA4DB4;
        public uint gameState => 0xEE9334;
        public uint loadingScreenID => 0xD99114;
        public uint ghostTimer => 0xDA29de;
        public uint deathCount => 0xED7F14;
        public uint planetFrameCount => 0x1A70B30;
        public uint marcadiaMission => 0xD3AABC;

        // Player variables
        public uint boltCount => 0xc1e4dc;
        public uint healthXP => 0xc1e510;
        public uint playerHealth => 0xda5040;
        public uint playerCoords => 0xDA2870;
        public uint currentArmor => 0xc1e51c;
        public uint challengeMode => 0xC1E50D;

        // Arrays
        public uint titaniumBoltsArray => 0xECE53D;
        public uint skillPointsArray => 0xDA521d;
        public uint itemArray => 0xc1e43c;
        public uint ammoArray => 0xDA5240;
        public uint vidComics => 0xda650b;
        public uint unlockArray => 0xDA5710;

        // Toggles / Menus
        public uint quickSelectPause => 0xC1E652;
        public uint ccHelpDesk => 0x148A100;
        public uint vidComicMenu => 0xC4F918;

        // Boss stuff
        public uint klunkTuning1 => 0xC9165C;
        public uint klunkTuning2 => 0xC36BCC;

        // Load stuff
        public uint fastLoad1 => 0x134EBD4; // Set to "00000003" to force third load screen.
        public uint fastLoad2 => 0x134EE70; // Set to 0x0101 to force fast load.
        public uint loadPlanet => 0xEE9310;

        // Currently not implemented, probably works a bit different in RaC3 anyway.
        public uint levelFlags => throw new System.NotImplementedException();
        public uint miscLevelFlags => throw new System.NotImplementedException();
        public uint infobotFlags => throw new System.NotImplementedException();
        public uint moviesFlags => throw new System.NotImplementedException();
    }
    public class rac3 : IGame
    {
        public static RaC3Addresses addr = new RaC3Addresses();

        int ghostRatchetSubID = -1;
        public rac3(Ratchetron api) : base(api)
        {
            this.planetsList = new string[] {
            "Rac3Veldin",
            "Florana",
            "StarshipPhoenix",
            "Marcadia",
            "Daxx",
            "PhoenixRescue",
            "AnnihilationNation",
            "Aquatos",
            "Tyhrranosis",
            "ZeldrinStarport",
            "ObaniGemini",
            "BlackwaterCity",
            "Holostar",
            "Koros",
            "Unknown",
            "Rac3Metropolis",
            "CrashSite",
            "Rac3Aridia",
            "QwarksHideout",
            "LaunchSite",
            "ObaniDraco",
            "CommandCenter",
            "Holostar2",
            "InsomniacMuseum",
            "Unknown2",
            "MetropolisRangers",
            "AquatosClank",
            "AquatosSewers",
            "TyhrranosisRangers",
            "VidComic6",
            "VidComic1",
            "VidComic4",
            "VidComic2",
            "VidComic3",
            "VidComic5",
            "VidComic1SpecialEdition"
            };
        }


        public override void ResetLevelFlags()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Overwrites load segments with nops. Taken from username's UYA IL practice patch
        /// </summary>
        /// <param name="enabled"></param>
        public override void SetFastLoads(bool enabled = false)
        {
            if (enabled)
            {
                // single seg loads pt. 1 [li r3, 4; blr]
                api.WriteMemory(pid, 0x27C2E8, new byte[] { 0x38, 0x60, 0x00, 0x04 });
                api.WriteMemory(pid, 0x27C2EC, new byte[] { 0x4e, 0x80, 0x00, 0x20 });

                // single seg loads pt. 2 [nop]
                api.WriteMemory(pid, 0x280E68, new byte[] { 0x60, 0x00, 0x00, 0x00 });

                // single seg loads pt. 3 [nop]
                api.WriteMemory(pid, 0xAB6688, new byte[] { 0x60, 0x00, 0x00, 0x00 });

                // single seg loads pt. 4[nop]
                api.WriteMemory(pid, 0x1D29FC, new byte[] { 0x60, 0x00, 0x00, 0x00 });
            }
            else // Restore original game code
            {
                // Part 1
                api.WriteMemory(pid, 0x27C2E8, new byte[] { 0xf8, 0x21, 0xff, 0x81 });
                api.WriteMemory(pid, 0x27C2EC, new byte[] { 0x7c, 0x08, 0x02, 0xa6 });

                // Part 2
                api.WriteMemory(pid, 0x280E68, new byte[] { 0x9b, 0xfe, 0x00, 0x71 });

                // Part 3
                api.WriteMemory(pid, 0xAB6688, new byte[] { 0x40, 0x9e, 0xff, 0xe8 });

                // Part 4
                api.WriteMemory(pid, 0x1D29FC, new byte[] { 0x40, 0x82, 0x00, 0xa4 });
            }
        }



        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            if (toggle)
            {
                api.WriteMemory(pid, 0x182A88, 4, new byte[] { 0x60, 0x00, 0x00, 0x00 });
            }
            else
            {
                api.WriteMemory(pid, 0x182A88, 4, new byte[] { 0x7c, 0x85, 0x31, 0x2e });
            }
        }

        public override void SetupFile()
        {
            api.WriteMemory(pid, rac3.addr.klunkTuning1, 0x7);
            api.WriteMemory(pid, rac3.addr.klunkTuning2, 0x3);
            api.WriteMemory(pid, rac3.addr.vidComicMenu, new byte[] { 0x00, 0x00, 0x00, 0x02 });
            api.WriteMemory(pid, rac3.addr.ccHelpDesk, new byte[] { 0x00, 0x00, 0x00, 0x01 });
        }

        /// <summary>
        /// Ghost ratchet works by having a frame countdown, we hard enable ghost ratchet by freezing the frame countdown to 10.
        /// </summary>
        /// <param name="enabled">if true freezes frame countdown to 10, if false releases the freeze</param>
        public void SetGhostRatchet(bool enabled)
        {
            if (enabled)
            {
                ghostRatchetSubID = api.FreezeMemory(pid, rac3.addr.ghostTimer, 10);
            }
            else
            {
                api.ReleaseSubID(ghostRatchetSubID);
            }
        }

        public void ResetAllTitaniumBolts()
        {
            api.WriteMemory(pid, rac3.addr.titaniumBoltsArray, new byte[128]);
        }

        public void GiveAllTitaniumBolts()
        {
            api.WriteMemory(pid, rac3.addr.titaniumBoltsArray, Enumerable.Repeat((byte)0x01, 128).ToArray());
        }

        public void ResetAllSkillpoints()
        {
            api.WriteMemory(pid, rac3.addr.skillPointsArray, new byte[30]);
        }

        public void GiveAllSkillpoints()
        {
            api.WriteMemory(pid, rac3.addr.skillPointsArray, Enumerable.Repeat((byte)0x01, 30).ToArray());
        }

        public int GetChallengeMode()
        {
            return BitConverter.ToInt32(api.ReadMemory(pid, rac3.addr.challengeMode, 4), 0);
        }

        public void SetChallengeMode(int mode)
        {
            api.WriteMemory(pid, rac3.addr.challengeMode, BitConverter.GetBytes(mode));
        }

        public void SetVidComic(int number, bool enabled)
        {
            api.WriteMemory(pid, rac3.addr.vidComics + (uint)number, BitConverter.GetBytes(enabled));
        }

        public bool GetVidComic(int number)
        {
            return BitConverter.ToBoolean(api.ReadMemory(pid, rac3.addr.vidComics + (uint)number, 1), 0);
        }

        public void SetArmor(int number)
        {
            api.WriteMemory(pid, rac3.addr.currentArmor, BitConverter.GetBytes((ushort)number).Reverse().ToArray());
        }
    }
}
