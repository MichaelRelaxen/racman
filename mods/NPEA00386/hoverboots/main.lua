-- Function that runs once when the automation is loaded.
function OnLoad()

end

-- OnTick runs ever 16ms, so that should be about 60 times per second
-- The `ticks` argument is the current tick, it counts up by 1 for every tick.
function OnTick(ticks)
    if Inputs:R1Pressed() then
        ratchet.state = 123
    end
end

-- This function runs as the mod is being unloaded, either through an execution error or the user unloading the mod manually
function OnUnload()

end