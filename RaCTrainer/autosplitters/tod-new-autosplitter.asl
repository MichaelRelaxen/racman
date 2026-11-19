state("racman") { }

startup
{
    settings.Add("SPLIT_ROUTE_NAMED", false, "Use split route based on split names");
    settings.SetToolTip("SPLIT_ROUTE_NAMED", "Only split when entering the planet named by the next LiveSplit split.");

    settings.Add("SPLIT_ROUTE_CATS", false, "Use category split route:");
    settings.SetToolTip("SPLIT_ROUTE_CATS", "Use a fixed planet route for the selected category.");

    settings.Add("SPLIT_ROUTE_NGP", false, "Use NG+ split route", "SPLIT_ROUTE_CATS");
    settings.Add("SPLIT_ROUTE_ANY", false, "Use Any% split route", "SPLIT_ROUTE_CATS");
    settings.Add("SPLIT_ROUTE_AGB", false, "Use AGB split route", "SPLIT_ROUTE_CATS");
}

init
{
    var mmf = System.IO.MemoryMappedFiles.MemoryMappedFile.OpenExisting("racman-autosplitter-lc");
    var stream = mmf.CreateViewStream();
    vars.reader = new System.IO.BinaryReader(stream);

    vars.UpdateValues = (Action)(() =>
    {
        vars.reader.BaseStream.Position = 0;

        current.command = vars.reader.ReadByte();
        current.paused = vars.reader.ReadByte();
        current.planet = vars.reader.ReadByte();
        current.gameStatus = vars.reader.ReadByte();
    });
    vars.UpdateValues();

    vars.splitIndex = 0;
    vars.planetNames = new List<List<string>>();
    vars.routeNGP = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 11, 12, 13, 14, 15, 16, 17, 18 };
    vars.routeAny = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 11, 11, 12, 13, 14, 15, 16, 17, 18 };
    vars.routeAGB = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 5, 11, 11, 12, 13, 14, 15, 16, 17, 7, 18 };

    var basePath = Path.GetDirectoryName(game.MainModule.FileName);
    var planetsPath = Path.Combine(basePath, "autosplitters", "todplanets.txt");

    using (StreamReader reader = File.OpenText(planetsPath))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var names = new List<string>();
            foreach (string name in line.Split(','))
            {
                var normalizedName = name.Trim().ToLower();
                if (normalizedName.Length > 0)
                {
                    names.Add(normalizedName);
                }
            }
            vars.planetNames.Add(names);
        }
    }
}

update
{
    vars.UpdateValues();
}

onReset
{
    vars.splitIndex = 0;
}

start
{
    if (current.command == 1 && old.command != 1)
    {
        vars.splitIndex = 0;
        return true;
    }

    return false;
}

reset
{
    return current.command == 1 && old.command != 1;
}

split
{
    if (current.command != 2 || old.command == 2)
    {
        return false;
    }

    if (settings["SPLIT_ROUTE_NAMED"])
    {
        var splitIndex = timer.CurrentSplitIndex + 1;
        if (splitIndex >= timer.Run.Count)
        {
            return false;
        }

        var nextSplitName = timer.Run[splitIndex].Name.ToLower();
        var validNames = new List<string>();

        if (current.planet <= 18)
        {
            validNames = vars.planetNames[(int)current.planet];
        }

        foreach (var name in validNames)
        {
            if (nextSplitName.Contains(name))
            {
                return true;
            }
        }

        return false;
    }

    if (settings["SPLIT_ROUTE_CATS"])
    {
        var splitRoute = new List<int>();

        if (settings["SPLIT_ROUTE_NGP"])
        {
            splitRoute = vars.routeNGP;
        }
        else if (settings["SPLIT_ROUTE_ANY"])
        {
            splitRoute = vars.routeAny;
        }
        else if (settings["SPLIT_ROUTE_AGB"])
        {
            splitRoute = vars.routeAGB;
        }

        if (vars.splitIndex + 1 < splitRoute.Count &&
            splitRoute[vars.splitIndex + 1] == (int)current.planet)
        {
            vars.splitIndex += 1;
            return true;
        }

        return false;
    }

    return true;
}

isLoading
{
    // A QuitPending -> InGame transition is a full game reboot. Loading-screen
    // pauses do not change gameStatus and therefore do not receive a penalty.
    if (old.gameStatus == 1 && current.gameStatus == 0)
    {
        // Manual timing: from first frame of please wait to first frame of load dialog
        // Is slightly longer (2s with a bit of leeway) so subtract 19s
        timer.SetGameTime(timer.CurrentTime.GameTime.Value.Add(TimeSpan.FromSeconds(16.9)));
    }

    return current.paused == 1;
}
