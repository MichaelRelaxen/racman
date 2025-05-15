state("racman") {}

startup
{	
    settings.Add("SPLIT_ROUTE", false, "Use split route based on split names");
    settings.SetToolTip("SPLIT_ROUTE", "Only split when entering the next planet on your LiveSplit.");
}

init
{
    System.IO.MemoryMappedFiles.MemoryMappedFile mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter");
    System.IO.MemoryMappedFiles.MemoryMappedViewStream stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);
    
    vars.reader.BaseStream.Position = 0;

    current.savePlanetID = vars.reader.ReadByte();
    current.loadScreenType = vars.reader.ReadByte();

     vars.planetNames = new List<List<string>>();
    
    // Get file with planet names
    var basePath = Path.GetDirectoryName(game.MainModule.FileName);
    var planetsPath = Path.Combine(basePath, "autosplitters", "todplanets.txt");

    print(planetsPath);
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

    current.savePlanetID = vars.reader.ReadByte();
    current.loadScreenType = vars.reader.ReadByte();
}

start
{

}

reset
{

}

split
{
    // Exception: Cobalia first-visit loads differently, so always split here
    var shouldSplit = false;
    shouldSplit |= (current.savePlanetID != old.savePlanetID && current.savePlanetID != 0 && old.savePlanetID != 0);
    shouldSplit |= current.savePlanetID == 1 && old.savePlanetID == 0; // Cobalia splits differently

    if (!shouldSplit) return;

    if (settings["SPLIT_ROUTE"]) 
    {
        var nextSplitName = "";
        var splitIndex = timer.CurrentSplitIndex + 1;
        if (splitIndex < timer.Run.Count) 
        {
            nextSplitName = timer.Run[splitIndex].Name;
            print(nextSplitName);
        }

        var validNames = new List<string>();
        if (current.savePlanetID >= 0 && current.savePlanetID <= 18) 
        {
            int idx = (int)current.savePlanetID;
            validNames = vars.planetNames[idx];
        }

        foreach (var name in validNames) 
        {
            print("name option:");
            print(name);
            if (nextSplitName.ToLower().Contains(name)) 
            {
                return true;
            }
        }
    } 
    else 
    {
        return true;
    }
}

isLoading 
{
    return current.savePlanetID == 0 && current.loadScreenType > 1;
}
