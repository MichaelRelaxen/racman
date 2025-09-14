using racman.offsets;
using racman.offsets.RAC4;
using System;
using System.Collections.Generic;
using System.Linq;
using DiscordRPC;
using Timer = System.Windows.Forms.Timer;

namespace racman
{
    public class RaC4Addresses : IAddresses
    {
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

        public uint playerCoords => 0x10D7334;

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

        public uint mobyInstances => throw new NotImplementedException();
    }
    public class rac4 : IGame, IAutosplitterAvailable
    {
        public Timer fastloadTimer = new Timer();

        public static RaC4Addresses addr = new RaC4Addresses();
        
        public DiscordRpcClient DiscordClient;
        
        private Timestamps initialTimestamp;
        
        private uint lastPlanetIndex = 100;
        
        public void InitializeDiscordRPC()
        {
            if (DiscordClient != null)
            {
                DiscordClient.Dispose();
                DiscordClient = null;
            }
            DiscordClient = new DiscordRpcClient("1373373412264771705");
            DiscordClient.Initialize();
            initialTimestamp = Timestamps.Now;
        }

        private List<BotsUnlocks> botsUnlocks;

        int ghostRatchetSubID = -1;
        public rac4(IPS3API api) : base(api)
        {
            this.planetsList = new string[] {
                "Dreadzone-Station",
                "Dreadzone-Station",
                "Catacrom-IV",
                "Dreadzone-Station",
                "Sarathos",
                "Kronos",
                "Shaar",
                "Valix-belt",
                "Orxon",
                "Dreadzone-Station",
                "Torval",
                "Stygia",
                "Dreadzone-Station",
                "Maraxus",
                "Ghost-Station",
                "Dreadzone-Station"
            };
            
            botsUnlocks = BotsUnlocksFactory.GetUpgrades();
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

        public override void CheckInputs(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void CheckPlanetForDiscordRPC(object sender = null, EventArgs e = null)
        {
            Console.WriteLine("UpdateRichPresence");
            if (!DiscordTimer.Enabled) {
                if (DiscordClient != null)
                {
                    DiscordClient.Dispose();
                    DiscordClient = null;
                    lastPlanetIndex = 100; // Valeur invalide pour forcer une mise à jour
                }
                return;
            }
            
            byte[] planetData = api.ReadMemory(pid, rac4.addr.currentPlanet, 4);
            if (planetData?.Length != 4) return; 
            
            uint planetindex = BitConverter.ToUInt32(planetData.Reverse().ToArray(), 0);
            Console.WriteLine(planetindex);
            
            if (planetindex != lastPlanetIndex) {
                if (DiscordClient == null) InitializeDiscordRPC();
                lastPlanetIndex = planetindex;
                if (planetindex < planetsList.Length)
                    UpdateRichPresence(planetsList[planetindex]);
            }
        }
        
        public void UpdateRichPresence(string planetname)
        {
            Console.WriteLine(planetname);
            if (DiscordClient == null)
                return;
            var imageKey = planetname.ToLower();
            try {
                DiscordClient.SetPresence(new RichPresence()
                {
                    Details = planetname,
                    Timestamps = initialTimestamp,
                    Assets = new Assets()
                    {
                        LargeImageKey = "rac4",
                        LargeImageText = "Ratchet Gladiator",
                        SmallImageKey = imageKey,
                        SmallImageText = planetname,
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cant update : {ex.Message}");
            }
        }
    }
}
