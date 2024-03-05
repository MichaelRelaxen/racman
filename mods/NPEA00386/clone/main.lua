function OnLoad()
    unlock_grabber = 0x01481a87
    unlock_breaker = 0x01481ab2
    substi_breaker = 0x01329a72
    
    breaker_rep = inttobytes(50, 1)
    ratchet_rep = inttobytes(227, 1)

    unlocked = inttobytes(1, 1)
    locked = inttobytes(0, 1)

    grabber_unlocked = Ratchetron:ReadMemory(GAME_PID, unlock_grabber, 1)
    breaker_unlocked = Ratchetron:ReadMemory(GAME_PID, unlock_breaker, 1)

    -- Disable bolt grabber, enable and replace box breaker
    Ratchetron:WriteMemory(GAME_PID, unlock_grabber, 1, locked)
    Ratchetron:WriteMemory(GAME_PID, unlock_breaker, 1, unlocked)
    Ratchetron:WriteMemory(GAME_PID, substi_breaker, 1, ratchet_rep)
    
    ratchet.health = 0
end

function OnTick(ticks)

end

function OnUnload()
    -- Replace with original unlock values
    Ratchetron:WriteMemory(GAME_PID, unlock_grabber, 1, grabber_unlocked)
    Ratchetron:WriteMemory(GAME_PID, unlock_breaker, 1, breaker_unlocked)
    -- Replace box breaker with, well, the box breaker
    Ratchetron:WriteMemory(GAME_PID, substi_breaker, 1, breaker_rep)

    ratchet.health = 0
end