function OnLoad()
    hue = 0
    addr1 = 0x13185a0
    addr2 = 0x1318590
end

local function abgr_from_hue(alpha, hue)
    local r = math.floor((math.sin(hue * math.pi * 2) * 0.5 + 0.5) * 255)
    local g = math.floor((math.sin((hue * math.pi * 2) + (math.pi * 2 / 3)) * 0.5 + 0.5) * 255)
    local b = math.floor((math.sin((hue * math.pi * 2) + (math.pi * 4 / 3)) * 0.5 + 0.5) * 255)

    -- Convert to ABGR (0xAABBGGRR format)
    local a = alpha
    local abgr = (a << 24) | (b << 16) | (g << 8) | r
    return abgr
end

function OnTick()
    local color = abgr_from_hue(0x40, hue)
    local color2 = abgr_from_hue(0x20, hue + 0.2) -- Offset hue for second color
	
    Ratchetron:WriteMemory(GAME_PID, addr1, color)
    Ratchetron:WriteMemory(GAME_PID, addr1 + 4, color2)
    Ratchetron:WriteMemory(GAME_PID, addr2, color)
    Ratchetron:WriteMemory(GAME_PID, addr2 + 4, color2)
    
    hue = (hue + 0.01) % 1 -- Cycle hue
    
end

function OnUnload()

end