state("racman") { }

startup
{
    settings.Add("SPLIT_ROUTE", false, "Use split route based on split names");
    settings.SetToolTip("SPLIT_ROUTE", "Only split when entering the next planet on your LiveSplit.");
}


init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter-lc");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

    // Update values from memory
    vars.UpdateValues = (Action) (() => {
        vars.reader.BaseStream.Position = 0;

        // Read values from memory
        current.command = vars.reader.ReadByte();
        current.paused = vars.reader.ReadByte();
        current.planet = vars.reader.ReadByte();
    });
    vars.UpdateValues();

    vars.planetNames = new List<List<string>>();

    // Get file with planet names
    var basePath = Path.GetDirectoryName(game.MainModule.FileName);
    var planetsPath = Path.Combine(basePath, "autosplitters", "dlplanets.txt");

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
    vars.UpdateValues();
}

split
{
    if (current.command == 2 && old.command != 2) 
    {
        if (!settings["SPLIT_ROUTE"]) return true;
        
        var nextSplitName = "";
        var splitIndex = timer.CurrentSplitIndex + 1;
        if (splitIndex < timer.Run.Count) 
        {
            nextSplitName = timer.Run[splitIndex].Name;
        }
        print(nextSplitName);

        var validNames = new List<string>();
        if (current.planet >= 0 && current.planet <= 15) 
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
    return false;
}

reset
{
    return (current.command == 1 && old.command != 1);
}

start
{
    return (current.command == 1 && old.command != 1);
}

isLoading
{
    return (current.paused == 1);
}
