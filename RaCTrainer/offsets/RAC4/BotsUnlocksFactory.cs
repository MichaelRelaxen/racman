using System.Collections.Generic;

namespace racman.offsets.RAC4
{
    public class BotsUnlocksFactory
    {
        public static uint UpgradesCount = 16;

        public static List<BotsUnlocks> GetUpgrades()
        {
            return new List<BotsUnlocks>
            {
                new BotsUnlocks("Pistol Flux LX", 0),
                new BotsUnlocks("Range Warrior", 1),
                new BotsUnlocks("Bogo", 2),
                new BotsUnlocks("Alpha Ravager", 3),
                new BotsUnlocks("Beta Ravager", 4),
                new BotsUnlocks("EMP Grenade", 5),
                new BotsUnlocks("Hacker Ray", 6),
                new BotsUnlocks("Shield Link", 7),
                new BotsUnlocks("Grind Cable", 8),
                new BotsUnlocks("Go-Comet A", 9),
                new BotsUnlocks("Go-Comet X", 10),
                new BotsUnlocks("Go-Comet XR", 11),
                new BotsUnlocks("Hyper-Tron", 12),
                new BotsUnlocks("Dreadinator", 13),
                new BotsUnlocks("DZ Ultra", 14),
                new BotsUnlocks("Ultranator", 15),
            };
        }
    }
}
