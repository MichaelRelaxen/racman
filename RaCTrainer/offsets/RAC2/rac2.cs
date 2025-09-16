using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using DiscordRPC;

namespace racman
{
    public class RaC2Addresses : IAddresses
    {
        // Current bolt count
        public uint boltCount => 0x1329A90;

        // Ratchet's coordinates
        public uint playerCoords => 0x147F260;

        // The coordinates ratchet wil respawn at
        public uint respawnCoords => 0x15D26E0;

        // Ratchet's movement state
        public uint playerState => 0x1481474;

        // Controller inputs mask address
        public uint inputOffset => 0x147A430;

        // Controller analog sticks address
        public uint analogOffset => 0x147A60C;

        // First 0x4 for if planet should be loaded, the 0x4 after for planet to load.
        public uint loadPlanet => 0x156B050;

        // Currently loaded planet.
        public uint currentPlanet => 0x1329A3C;

        // Frames until "Ghost Ratchet" runs out.
        public uint ghostTimer => 0x147F3ce;

        // Current raritanium.
        public uint currentRaritanium => 0x1329A94;

        // Challenge mode
        public uint challengeMode => 0x1329AA2;

        // Values corresponding to the location of the internal table for game objects.
        public uint mobyInstances => 0x15927b0;
        public uint mobyInstancesEnd => 0x15927b8;

        // Values that increment when you die to the Snivelak boss. Used for act tuning.
        // public uint snivBoss1 => 0x01569BF7; // not used according to elkon
        public uint snivBoss => 0x1A6FB73;
        // Angela on Siberius
        public uint sibBoss => 0x1A5A99F;
        // Protopet on Yeedil
        public uint yeedilBoss => 0x1A9DF90;

        public uint slotsHit => 0x14817AF;
        public uint pBolts => 0x1390C27;
        public uint pJackpot => 0x1390C37;


        // Float value controlling jump-pad speed. Changes on visiting Snivelak.
        public uint padManip => 0x13185B8;

        // Item ID of ratchet's previously held weapon. Used for gadget storage.
        public uint prevHeldWeapon => 0x1329A9F;

        // Start of ammo array (136 length)
        public uint ammoArray => 0x148185C;

        // Boosts exp values earned when killing enemies.
        public uint expEconomy => 0x1329AA8;

        // Determines ratchet's current health
        public uint healthExp => 0x1329AA4;

        // Set to 1 if insomniac museum shortcut is avaliable.
        public uint imInShortcuts => 0x135268C;

        // Index of selected item in shortcuts menu.
        public uint shortcutsIndex => 0x1352684;

        // Stored race on Barlow (maybe on Joba too idk).
        public uint savedRaceIndex => 0x1A4D7E0;

        // When set to 1, the cutscene on planet Gorn is skipped. 4 bytes. Credit to Elkkon for finding this.
        public uint gornManip => 0x1A99A4C;

        // First cutscene on Feltzin that only plays once per session.
        public uint feltzinOpening => 0x1A8495B;
        // Same thing but on Gorn
        public uint gornOpening => 0x1A99A34;

        // Should be set to 0 but isn't reset properly - causes menu bug
        public uint feltzinMissionComplete => 0x1A84973;
        // Same thing but on Hrugis
        public uint hrugisMissionComplete => 0x143DB0F;
        // And Gorn
        public uint gornMissionComplete => 0x1A99A5B;

        // 0 Right-to-left (3.6, 218 frames)
        // 1 Curved (3.7, 226 frames)
        // 2 Left-to-right (3.6, 217 frames)
        // 3 Top-to-bottom (4.0, 238 frames)
        // 4 Planet loading screen
        public uint loadingScreenType => 0x147A257;
        public uint loadingScreenCount => 0x147A25B;

        // "Current active save slot" used for tracking savefiles (PS2 leftover).
        // This gets set to -1 when you do QE (and is subsequently overwritten).
        public uint selectedSaveSlot => 0x013298cc;

        // Internal debug flag that controls some debug features. Set to 1 to enable.
        // For "documentation", see: https://www.youtube.com/watch?v=AwIoPo1NstU
        public uint debugFeatures => 0x015b3070;

        // What it says on the tin
        public uint platinumBoltArray => 0x1562540;

        // Level specific info (spawnpoint, completed missions etc)
        public uint levelFlags => 0x15625B0;

        public uint currentChunk => 0x157CE03;

        public uint chargebootsPrimaryFrontColor => 0x1318590;
        public uint chargebootsPrimaryBackColor => 0x1318594;
        public uint chargebootsTintFrontColor => 0x13185a0;
        public uint chargebootsTintBackColor => 0x13185a0;
    }

    public class rac2 : IGame, IAutosplitterAvailable
    {
        public static RaC2Addresses addr = new RaC2Addresses();
        
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
            DiscordClient = new DiscordRpcClient("1357302807769907341");
            DiscordClient.Initialize();
            initialTimestamp = Timestamps.Now;
        }

        public rac2(IPS3API api) : base(api)
        {
            this.planetsList = new string[] {
                "Aranos",
                "Oozla",
                "Maktar",
                "Endako",
                "Barlow",
                "Feltzin",
                "Notak",
                "Siberius",
                "Tabora",
                "Dobbo",
                "Hrugis",
                "Joba",
                "Todano",
                "Boldan",
                "Aranos2",
                "Gorn",
                "Snivelak",
                "Smolg",
                "Damosel",
                "Grelbin",
                "Yeedil",
                "InsomniacMuseum",
                "DobboOrbit",
                "DamoselOrbit",
                "SlimCognito",
                "Wupash",
                "JammingArray",
            };
        }
        private int ghostRatchetSubID = -1;
        public bool resetFlagsRequested = true;

        public IEnumerable<(uint addr, uint size)> AutosplitterAddresses => new (uint, uint)[]
        {
            (0x0156B064, 4), // Game state
            (0x01481474, 4), // Ratchet state
            (0x0133EE7C, 4), // Protopet's health bar (Float, ranges 0-1)
            (0x1329A3C, 4), // current planet
            (0x156B054, 4), // destination planet
            (rac2.addr.currentChunk, 1),
            (0x1562699, 1), // clank level flag on a2
            (rac2.addr.loadingScreenType, 1),
            (0x1478991, 1) // yeedil scene ID
        };


        /// <summary>
        /// Resets level flag of destination planet
        /// </summary>
        public override void ResetLevelFlags()
        {
            var flagsForPlanet = rac2.addr.levelFlags + (planetToLoad * 0x10);
            var reset = Enumerable.Repeat((byte)0x00, 0x10).ToArray();
            api.WriteMemory(pid, flagsForPlanet, reset);
        }


        public override void ResetGoldBolts(uint planetIndex)
        {   }

        /// <summary>
        /// Ghost ratchet works by having a frame countdown, we hard enable ghost ratchet by freezing the frame countdown to 10.
        /// </summary>
        /// <param name="enabled">if true freezes frame countdown to 10, if false releases the freeze</param>
        public void SetGhostRatchet(bool enabled)
        {
            if (enabled)
            {
                ghostRatchetSubID = api.FreezeMemory(pid, addr.ghostTimer, 10);
            }
            else
            {
                api.ReleaseSubID(ghostRatchetSubID);
            }
        }

        /// <summary>
        /// Overwrites game code that decreases ammo when you use a weapon
        /// </summary>
        /// <param name="toggle">Overwrites ammo decreasement code with nops on true, restores original game code on false</param>
        public override void ToggleInfiniteAmmo(bool toggle = false)
        {

        }

        public override void SetupFile()
        {
            throw new NotImplementedException();
        }

        public void enableDisableFastLoads(bool enable)
        {
            var pid = api.getCurrentPID();
            uint fastLoadInstr = 0xBEA8A0;

            if (enable)
            {
                // NOP
                api.WriteMemory(pid, fastLoadInstr, 0x60000000);
            }
            else
            {
                // Default instr
                api.WriteMemory(pid, fastLoadInstr, 0x4BFFEA69);

            }
        }

        public void loadSetAsideFile()
        {
            enableDisableFastLoads(true);
            api.WriteMemory(pid, 0x10cd71e, new byte[] { 1 });
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
                LoadPosition();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.dieCombo && inputCheck)
            {
                KillYourself();
                inputCheck = false;
            }
            if (Inputs.RawInputs == ConfigureCombos.loadPlanetCombo && inputCheck)
            {
                enableDisableFastLoads(true);
                LoadPlanet(resetFlags: resetFlagsRequested);
                inputCheck = false;
            }
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
            
            byte[] planetData = api.ReadMemory(pid, rac2.addr.currentPlanet, 4);
            if (planetData?.Length != 4) return; 
            
            uint planetindex = BitConverter.ToUInt32(planetData.Reverse().ToArray(), 0);
            
            if (planetindex != lastPlanetIndex) {
                if (DiscordClient == null) InitializeDiscordRPC();
                lastPlanetIndex = planetindex;
                if (planetindex < planetsList.Length)
                    UpdateRichPresence(planetsList[planetindex]);
            }
        }
        
        public void UpdateRichPresence(string planetname)
        {
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
                        LargeImageKey = "rac2",
                        LargeImageText = "Ratchet & Clank 2",
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

        public override void SetFastLoads(bool enabled = false)
        {
            throw new NotImplementedException();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Vec4
        {
            public float x;
            public float y;
            public float z;
            public float w;
        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct GamePtr
        {
            public uint addr;
        }


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct Moby
        {
            public fixed byte bSphere[16];
            public Vec4 position;
            public sbyte state;
            public byte group;
            public sbyte mClass;
            public sbyte alpha;
            public GamePtr pClass;
            public GamePtr pChain;
            public float size;
            public byte updateDistance;
            public byte drawn;
            public ushort drawDistance;
            public ushort modeBits1;
            public ushort modeBits2;
            public ulong lights;
            public byte field15_0x40;
            public byte field16_0x41;
            public byte field17_0x42;
            public byte cur_animation_seq;
            public byte field19_0x44;
            public byte field20_0x45;
            public byte field21_0x46;
            public byte field22_0x47;
            public float field23_0x48;
            public float field24_0x4c;
            public byte field25_0x50;
            public byte field26_0x51;
            public byte field27_0x52;
            public byte field28_0x53;
            public GamePtr field29_0x54;
            public GamePtr prev_anim;
            public GamePtr curr_anim;
            public byte field32_0x60;
            public byte field33_0x61;
            public byte field34_0x62;
            public byte field35_0x63;
            public GamePtr pUpdate;
            public GamePtr pVar;
            public byte field38_0x6c;
            public byte field39_0x6d;
            public byte field40_0x6e;
            public byte field41_0x6f;
            public float field42_0x70;
            public float field43_0x74;
            public int copiedFromModelHeader;
            public byte field45_0x7c;
            public byte field46_0x7d;
            public byte field47_0x7e;
            public uint field48_0x7f;
            public byte field49_0x83;
            public byte field50_0x84;
            public byte field51_0x85;
            public byte field52_0x86;
            public byte field53_0x87;
            public byte field54_0x88;
            public byte field55_0x89;
            public byte field56_0x8a;
            public byte field57_0x8b;
            public byte field58_0x8c;
            public byte field59_0x8d;
            public byte field60_0x8e;
            public byte field61_0x8f;
            public byte field62_0x90;
            public byte field63_0x91;
            public byte field64_0x92;
            public byte subState;
            public byte prevState;
            public byte stateType;
            public ushort stateTimer;
            public GamePtr collData;
            public int collActive;
            public uint collCnt;
            public byte field72_0xa4;
            public byte field73_0xa5;
            public byte field74_0xa6;
            public byte field75_0xa7;
            public byte collDamage;
            public byte field77_0xa9;
            public ushort oClass;
            public uint moby_counter_indexed;
            public byte field80_0xb0;
            public byte field81_0xb1;
            public short UID;
            public byte field83_0xb4;
            public byte field84_0xb5;
            public byte field85_0xb6;
            public byte field86_0xb7;
            public GamePtr multimoby_part;
            public byte substate;
            public sbyte field89_0xbd;
            public byte field90_0xbe;
            public byte field91_0xbf;
            public fixed byte rMtx[48];
            public Vec4 rotation;

            public static unsafe Moby ByteArrayToMoby(byte[] bytes)
            {
                if (BitConverter.IsLittleEndian)
                {
                    var type = typeof(Moby);
                    foreach (var field in type.GetFields())
                    {
                        // Skip byte fields and pointers
                        if (field.FieldType == typeof(byte))
                            continue;

                        // Get the offset of the field
                        var offset = Marshal.OffsetOf(type, field.Name).ToInt32();

                        if (field.FieldType == typeof(Vec4))
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                Array.Reverse(bytes, offset+(i*4), 4);
                            }
                        }

                        // Determine number of bytes to reverse based on field's type
                        int numBytesToReverse = 0;
                        if (field.FieldType == typeof(short) || field.FieldType == typeof(ushort))
                            numBytesToReverse = 2;
                        else if (field.FieldType == typeof(int) || field.FieldType == typeof(uint) ||
                                 field.FieldType == typeof(float) || field.FieldType == typeof(GamePtr))
                            numBytesToReverse = 4;
                        else if (field.FieldType == typeof(long) || field.FieldType == typeof(ulong) ||
                                 field.FieldType == typeof(double))
                            numBytesToReverse = 8;

                        // Reverse the bytes
                        Array.Reverse(bytes, offset, numBytesToReverse);
                    }
                }

                fixed (byte* ptr = &bytes[0])
                {
                    return (Moby)Marshal.PtrToStructure((IntPtr)ptr, typeof(Moby));
                }
            }
        };
    }
}
