state("racman") {}

startup
{	
    settings.Add("SPLIT_ROUTE", false, "Use split route based on split names");
    settings.SetToolTip("SPLIT_ROUTE", "Only split when entering the next planet on your LiveSplit.");

    settings.Add("PROTO_SPLIT", true, "Split on defeating protopet");
    settings.SetToolTip("PROTO_SPLIT", "Splits on defeating the protopet, the final boss.");

    settings.Add("CLANK_SPLIT", true, "A2 clank subsplit");
    settings.SetToolTip("CLANK_SPLIT", "Splits when transitioning from clank to ratchet on Aranos 2.");

    settings.Add("ARENA_SPLIT", false, "Maktar arena subsplit");
    settings.SetToolTip("ARENA_SPLIT", "Splits when entering the arena on Maktar.");
}

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.gameState = vars.reader.ReadUInt32();
    current.playerState = vars.reader.ReadUInt32();
    current.protopetHealth = vars.reader.ReadSingle();
    current.planet = vars.reader.ReadUInt32();
    current.destinationPlanet = vars.reader.ReadUInt32();
    current.chunk = vars.reader.ReadByte();
    current.clank = vars.reader.ReadByte();
    current.loadScreen = vars.reader.ReadByte();
    current.yeedilScene = vars.reader.ReadByte();

    vars.planetNames = new List<List<string>>();
    
    // Get file with planet names
    var basePath = Path.GetDirectoryName(game.MainModule.FileName);
    var planetsPath = Path.Combine(basePath, "autosplitters", "rac2planets.txt");

    using (StreamReader reader = File.OpenText(planetsPath))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var names = new List<string>();
            foreach (string name in line.Split(','))
            {
                names.Add(name);
            }
            vars.planetNames.Add(names);
        }
    }
}

update
{
    vars.reader.BaseStream.Position = 0;

    current.gameState = vars.reader.ReadUInt32();
    current.playerState = vars.reader.ReadUInt32();
    current.protopetHealth = vars.reader.ReadSingle();
    current.planet = vars.reader.ReadUInt32();
    current.destinationPlanet = vars.reader.ReadUInt32();
    current.chunk = vars.reader.ReadByte();
    current.clank = vars.reader.ReadByte();
    current.loadScreen = vars.reader.ReadByte();
    current.yeedilScene = vars.reader.ReadByte();

    if (current.loadScreen != old.loadScreen) 
    {
        double norm = 0.0;
        // right to left (+1)
        if (current.loadScreen == 0) 
        {
            norm = 1.0 / 60;
        }
        // curved (+9)
        else if (current.loadScreen == 1) 
        {
            norm = 9.0 / 60;
        }
        // top to bottom (+21)
        else if (current.loadScreen == 3) 
        {
            norm = 21.0 / 60; 
        }

        if (norm != 0.0) 
        {
            timer.SetGameTime(timer.CurrentTime.GameTime.Value.Subtract(TimeSpan.FromSeconds(norm)));
        }
    }
}

start
{
    return current.planet == 0 && current.playerState == 98 && old.playerState == 0;
    print("Starting the run");
}

reset
{
    return current.planet == 0 && current.playerState == 98 && old.playerState == 0;
}

split
{
    if (current.planet != old.planet)
    {
        if (!settings["SPLIT_ROUTE"]) 
        {
            return true;
        }

        var nextSplitName = "";
        var splitIndex = timer.CurrentSplitIndex + 1;
        if (splitIndex < timer.Run.Count) 
        {
            nextSplitName = timer.Run[splitIndex].Name;
        }
        print(nextSplitName);

        var validNames = new List<string>();
        if (current.planet >= 0 && current.planet <= 26) 
        {
            int idx = (int)current.planet;
            validNames = vars.planetNames[idx];
        }

        foreach (var name in validNames) 
        {
            if (nextSplitName.ToLower().Contains(name)) 
            {
                return true;
            }
        }
    }

    if (settings["ARENA_SPLIT"] && current.planet == 2 && current.chunk == 1 && old.chunk == 0) 
    {
        return true;
    }

    if (settings["CLANK_SPLIT"] && current.planet == 14 && current.clank == 128 && old.clank == 0) 
    {
        return true;
    }

    if (settings["PROTO_SPLIT"] && current.yeedilScene == 6 && old.yeedilScene != 6 && current.planet == 20)
    {
        // frames before cutscene plays
        double sevenFrames = 7.0 / 60;
        timer.SetGameTime(timer.CurrentTime.GameTime.Value.Subtract(TimeSpan.FromSeconds(sevenFrames)));

        return true;
    }
}

isLoading {
    return false;
}
