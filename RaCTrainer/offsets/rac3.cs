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

        public override void ToggleFastLoad(bool toggle = false)
        {
            throw new System.NotImplementedException();
        }

        public override void ToggleInfiniteAmmo(bool toggle = false)
        {
            throw new System.NotImplementedException();
        }

        public void SetGhostRatchet(bool enabled)
        {
            if (enabled)
                api.FreezeMemory(pid, addr.ghostTimer, 0x10);
            else
                api.ReleaseSubID(api.MemSubIDForAddress(addr.ghostTimer));
        }

        public override void SetupFile()
        {
            api.WriteMemory(pid, rac3.addr.klunkTuning1, 0x7);
            api.WriteMemory(pid, rac3.addr.klunkTuning2, 0x3);
            api.WriteMemory(pid, rac3.addr.vidComicMenu, new byte[] { 0x00, 0x00, 0x00, 0x02 });
            api.WriteMemory(pid, rac3.addr.ccHelpDesk, new byte[] { 0x00, 0x00, 0x00, 0x01 });
        }
    }
}
