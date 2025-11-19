using racman.offsets;
using racman.offsets.RAC4;
using System;
using System.Collections.Generic;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public class RaC4Addresses : IAddresses
    {
        public uint savefile_api_enabled = 0x15CD71D;
        public uint savefile_api_load = 0x15CD71E;
        public uint savefile_api_setaside = 0x15CD71F;

        // Input stuff
        public uint inputOffset => 0xB11F30;
        public uint analogOffset => 0xB1210C;

        // Currently not implemented, probably works a bit different in RaC3 anyway.
        public uint levelFlags => throw new System.NotImplementedException();
        public uint miscLevelFlags => throw new System.NotImplementedException();
        public uint infobotFlags => throw new System.NotImplementedException();
        public uint moviesFlags => throw new System.NotImplementedException();

        public uint botsUnlock => 0x9D2775;
        // unlocks are not saved, until the save flag is set to 1
        public uint botsUnlockSave => 0x9C3325;

        public uint qualifierSoftlock => 0x11C04C0;

        // Act tuning addresses for each boss
        public uint shellshockTuning => 0x0A947D3;
        public uint reactorTuning => 0x0A94944;
        public uint evisceratorTuning => 0x0A969E3;
        public uint aceTuning => 0x0A96E43;


        // Vox HP
        public uint voxHP => 0x449BEAD0;

        // Cutscene
        public uint cutscenePtr = 0xB36DE8;

        public uint boltCount => 0x9C32E8;

        public uint playerCoords => 0x10D44D0;
        // needed for save/load pos
        public uint playerCoords2 => 0x10D7334;

        // In Game (0 in main menu | 1 in game)
        public uint inGame => 0xB1F460;

        // load planet
        public uint loadPlanet => 0x9C3240;

        // current planet   (it's 0 in main menu)
        public uint currentPlanet => 0x119353C;

        // 0 = tutorial not completed | 1 = tutorial completed
        public uint tutorialFlags => 0xB1F46C;

        // 0 = not loading, 1 = loading
        public uint isLoading => 0xB0FD84;

        // 0 = third person, 1 = lock-strafe, 2 = first person
        public uint cameraMode => 0x9C287C;

        public uint playerHealth => 0x10D7250;
        public uint playerState => 0x10D69FC;

        public uint currentChallenge => 0x9D195C;

        public uint frameCounter => 0xB3C59C;

        public uint saveInfo => 0x11B1BD8;

        public uint prevPlanet => 0x0B1F465;
        // The challenge that is selected in the menu
        public uint challengeSelected => 0x11A0AE0;
        public uint dreadPoints => 0x09C32F4;
        // Byte, 21 avaliable
        public uint skin => 0x09C32FB;
        // Array of byte (6) (00 XX(Blue) 00 XX(Green) XX(Orange) XX(Grey))
        public uint badges => 0x09C3308;
        // Byte
        public uint CM => 0x09C330E;
        // Byte, 0= Marauder, 1= Avenger, 2= Crusader, 3= Vindicator, 4= Liberator
        public uint range => 0x09C331F;
        // Change to 1 to change planet
        public uint loadPlanet2 => 0x0B36DCC;
        public uint targetPlanet => 0x0B36DD0;

        public uint mobyInstances => throw new NotImplementedException();
    }
    public class rac4 : IGame, IAutosplitterAvailable
    {
        public Timer fastloadTimer = new Timer();

        public static RaC4Addresses addr = new RaC4Addresses();

        private List<BotsUnlocks> botsUnlocks;

        int ghostRatchetSubID = -1;
        public rac4(IPS3API api) : base(api)
        {
            this.planetsList = new string[] {
                "DreadZoneStation",
                "CatacromFour",
                "INFLOOP",
                "Sarathos",
                "Kronos",
                "Shaar",
                "TheValixBelt",
                "Orxon",
                "INFLOOP",
                "Torval",
                "Stygia",
                "INFLOOP",
                "Maraxus",
                "GhostStation",
            };
            this.skinsList = new string[] {
                "Marauder",
                "Avenger",
                "Crusader",
                "Vindicator",
                "Liberator",
                "AlphaClank",
                "Squidzor",
                "LandShark",               
                "TheMuscle",
                "W3RM",
                "Starshield",
                "KingClaude",
                "Vernon",                             
                "KidNova",
                "Venus",
                "Jak",            
                "Ninja",
                "SaurusRatchet",
                "GenomeRatchet",
                "SantaRatchet",
                "PipoSaruRatchet",
                "Clankchet",
            };
            botsUnlocks = BotsUnlocksFactory.GetUpgrades();
            this.planetsList = new string[]
            {
                "Multiplayer",
                "Dreadzone",
                "Catacrom",
                "Sarathos", 
                "Kronos", 
                "Shaar", 
                "Valix", 
                "Orxon", 
                "Torval", 
                "Stygia", 
                "Maraxus",
                "Interior"
            };
        }

    public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (addr.currentPlanet, 4),    // current planet
            (addr.loadPlanet, 4),       // load planet
            (addr.voxHP, 4),            // Vox HP
            (addr.cutscenePtr, 4),      // final (Vox) cutscene
            (addr.inGame, 4),           // in game boolean
            (addr.tutorialFlags, 4),    // tutorial flags
            (addr.isLoading, 4),        // loading boolean
            (addr.currentChallenge, 4), // current challenge ur on
        };

        public void UpdateUnlocks()
        {
            byte[] memory = api.ReadMemory(pid, addr.botsUnlock, ACITWeaponFactory.weaponCount * ACITWeaponFactory.weaponMemoryLenght);

            for (int i = 0; i < botsUnlocks.Count; i++)
            {
                botsUnlocks[i].IsUnlocked = BitConverter.ToBoolean(memory, i);
            }
        }

        public void SetUnlockState(BotsUnlocks unlock, bool state)
        {
            api.WriteMemory(pid, addr.botsUnlock + unlock.index, BitConverter.GetBytes(state));
            api.WriteMemory(pid, addr.botsUnlockSave + unlock.index, BitConverter.GetBytes(state));
        }

        public void SetGhostRatchet(bool enabled)
        {
            if (enabled)
            {
                // ghostRatchetSubID = api.FreezeMemory(pid, 0x10D47D0, 0x00100000);
                ghostRatchetSubID = api.FreezeMemory(pid, 0x10D47ce, 10);
            }
            else
            {
                api.ReleaseSubID(ghostRatchetSubID);
            }
        }

        public List<BotsUnlocks> GetBotsUnlocks()
        {
            UpdateUnlocks();
            return botsUnlocks;
        }

        public override void ResetLevelFlags()
        {
            throw new NotImplementedException();
        }

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new NotImplementedException();
        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public virtual void LoadPositionRac4()
        {
            string position = func.GetConfigData("config.txt", planetsList[planetIndex] + "SavedPos" + selectedPositionIndex);
            if (position != "")
            {
                api.WriteMemory(pid, addr.playerCoords, 30, position);
                api.WriteMemory(pid, addr.playerCoords2, 30, position);
            }
        }

        public override void CheckInputs(object sender, EventArgs e)
        {
            if (Inputs.RawInputs == ConfigureCombos.saveCombo && inputCheck)
            {
                SavePosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadCombo && inputCheck)
            {
                LoadPositionRac4();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.dieCombo && inputCheck)
            {
                KillYourself();
                inputCheck = false;
            }
            //if (Inputs.RawInputs == ConfigureCombos.loadPlanetCombo && inputCheck)
            //{
            //    enableDisableFastLoads(true);
            //    LoadPlanet(resetFlags: resetFlagsRequested);
            //    inputCheck = false;
            //}
            if (Inputs.RawInputs == ConfigureCombos.runScriptCombo && inputCheck)
            {
                AttachPS3Form.scripting?.RunCurrentCode();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadSetAsideCombo && inputCheck)
            {
                loadSetAsideFile();
                inputCheck = false;
            }
            if (Inputs.RawInputs == 0x00 && !inputCheck)
            {
                inputCheck = true;
            }
        }

        public void loadSetAsideFile()
        {
            var pid = api.getCurrentPID();
            api.WriteMemory(pid, addr.savefile_api_load, new byte[] { 1 });
        }

        public override void CheckPlanetForDiscordRPC(object sender = null, EventArgs e = null)
        {
            throw new NotImplementedException();
        }
    }
}
