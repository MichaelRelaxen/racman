state("racman") { }

startup
{
    settings.Add("SPLIT_ON_NEW_CHALLENGE", false, "Split everytime a new challenge starts.");
	settings.Add("SPLIT_PLANET", true, "Split everytime planet changes.");
}


init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

    // Update values from memory
    vars.UpdateValues = (Action) (() => {
        vars.reader.BaseStream.Position = 0;

        // Read values from memory
        current.planet = vars.reader.ReadUInt32();
        current.loadPlanet = vars.reader.ReadUInt32();
        current.voxHP = vars.reader.ReadSingle();
        current.cutscene = vars.reader.ReadUInt32();
        current.inGame = vars.reader.ReadUInt32();
        current.tutorialFlag = vars.reader.ReadUInt32();
        current.isLoading = vars.reader.ReadUInt32();
		current.currentChallenge = vars.reader.ReadUInt32();
    });
    vars.UpdateValues();

    // Initialize run values
    vars.ResetRunValues = (Action) (() => {
        vars.splitOnCurrentPlanet = false;
    });
	
	 Tuple<string, int>[] dreadzoneChallenges = {
		Tuple.Create("Advanced Qualifier", 37),
		Tuple.Create("Grist for the mill", 10),
		Tuple.Create("The big sleep", 11),
		Tuple.Create("Manic speed demon", 13),
		Tuple.Create("The tower of power", 9),
		Tuple.Create("Climb the tower of power", 15),
		Tuple.Create("Perfect chrome finish", 12),
		Tuple.Create("Close and personal", 14),
		Tuple.Create("Static deathtrap", 17),
		Tuple.Create("Marathon", 18),
		Tuple.Create("Reactor", 23),
		Tuple.Create("Zombie attack", 16),
		Tuple.Create("Heavy metal", 19),
		Tuple.Create("Endzone", 20),
		Tuple.Create("Murphys law", 21),
		Tuple.Create("Air drop", 25),
		Tuple.Create("Eviscerator", 38),
		Tuple.Create("Higher ground", 22),
		Tuple.Create("Cockscrew", 24),
		Tuple.Create("Swarmer surprise", 28),
		Tuple.Create("Accelerator", 34),
		Tuple.Create("Ace hardlight", 39),
		Tuple.Create("Dynamite baseball", 26),
		Tuple.Create("Less is more", 36)
	};
	vars.dreadzoneChallenges = dreadzoneChallenges;

}

update
{
    vars.UpdateValues();
    //print(current.tutorialFlag.ToString() + " - " + current.isLoading.ToString());
    //print(old.isLoading.ToString() + " - " + current.isLoading.ToString() + " - " + current.tutorialFlag.ToString() + " - " + current.loadPlanet.ToString());
}

onReset
{
    vars.ResetRunValues();
}

split
{
    if (!settings.SplitEnabled)
    {
        return false;
    }

    if (old.planet != current.planet)
    {
        vars.splitOnCurrentPlanet = false;
    }
	
    // planet split
    if (settings["SPLIT_PLANET"] && old.loadPlanet != current.loadPlanet && current.loadPlanet != 15 && old.loadPlanet != 0 && current.loadPlanet != current.planet && !vars.splitOnCurrentPlanet && current.planet != 0)
    {
        print("Split on planet " + current.loadPlanet + "->" + old.loadPlanet);
        vars.splitOnCurrentPlanet = true;
        return true;
    }
	

    // Vox split
    if (current.planet == 15 && current.voxHP <= 0.0f && old.cutscene == 0 && current.cutscene == 1)    
    {
        print("Split on Vox");
        return true;
    }
	
	if(settings["SPLIT_ON_NEW_CHALLENGE"]) {
		if(current.currentChallenge == 4294967295)
			current.currentChallenge = old.currentChallenge;
			
		if(current.planet == 0 && old.planet == 1) {
			current.planet = old.planet;
			// print("setting current planet to 1");
			}
			

			
		// Only split on actual challenges in dreadzone
		if(current.planet == 1) {
			if (current.currentChallenge != old.currentChallenge) {
				bool challengeFound = false;
				foreach (var challenge in vars.dreadzoneChallenges) {
					if(challenge.Item2 == current.currentChallenge) {
						challengeFound = true;
						break;
					}
				}
				if (!challengeFound) {
					current.currentChallenge = old.currentChallenge;
				}
			}
		}
		
		if (current.currentChallenge != old.currentChallenge) {
			print("current challenge is " + current.currentChallenge + "and old chall is " + old.currentChallenge);
			return true;
			}
		}

}

reset
{
    if (!settings.ResetEnabled)
    {
        return false;
    }

    if (current.planet == 0 && old.inGame == 1 && current.inGame == 0)
    {
        return true;
    }

    if (old.isLoading == 0 && current.isLoading == 1 && current.tutorialFlag == 0 && current.loadPlanet == 1 && old.inGame == 1)
    {
        return true;
    }
}

start
{
    if (!settings.StartEnabled)
    {
        return false;
    }

    // if in main menu
    if (old.inGame == 0 && current.inGame == 1 && old.cutscene == 0 && current.tutorialFlag == 0)
    {
        return true;
    }

    // if in game
    if (old.isLoading == 0 && current.isLoading == 1 && current.tutorialFlag == 0 && current.loadPlanet == 1)
    {
        return true;
    }
}
