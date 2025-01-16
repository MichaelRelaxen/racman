function OnLoad()
    height = ratchet.z
    fly = false
end

function OnTick(ticks)
    if Inputs:L3Pressed() and not oldl3 then
        height = ratchet.z
        fly = not fly
    end
    if fly then
        ratchet.z = height
        if Inputs:R1Pressed() then
            height = height + 0.2
        end
        if Inputs:R2Pressed() then
            height = height - 0.2
        end
    end
    oldl3 = Inputs:L3Pressed()
end

function OnUnload()

end