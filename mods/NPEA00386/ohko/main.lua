function OnLoad()
	original_health = ratchet.health

	ratchet.health = 1
end

function OnTick(ticks)
	if ratchet.health > 1 then
		ratchet.health = 1
	end
end

function OnUnload()
	ratchet.health = original_health
end