function FoVFuckeryOnLoad()
	original_fov = game.fov
	
	-- Need to set randomseed every time we want to get a new random number
	math.randomseed(os.time())
	game.fov = math.random() * 16
end

function FoVFuckeryOnUnload()
	game.fov = original_fov
end

function InvisibleRatchetOnLoad()
	ratchet_moby = Moby:findFirst(0)  -- Find Moby ID 0 (Ratchet)
	
	-- Set alpha to 0
	ratchet_moby.alpha = 0
end

function InvisibleRatchetTick(ticks)
	if ratchet_moby.alpha > 0 then
		ratchet_moby.alpha = 0
	end
end

function InvisibleRatchetOnUnload()
	local ratchet_moby = Moby:findFirst(0)  -- Find Moby ID 0 (Ratchet)
	
	-- 128 is max alpha
	ratchet_moby.alpha = 128
end

function YeetRatchetOnLoad()
	ratchet.z = ratchet.z + 100.0
end

function OneHitKOOnLoad()
	original_health = ratchet.health
end

function OneHitKOTick(ticks)
	if ratchet.health > 1 then
		ratchet.health = 1
	end
end

function OneHitKOOnUnload()
	ratchet.health = original_health
end

function BigRatchetOnLoad()
	ratchet_moby = Moby:findFirst(0)
	
	original_ratchet_scale = ratchet_moby.scale
	ratchet_moby.scale = 3.0
end

function BigRatchetTick(ticks)
	if ratchet_moby.scale < 3.0 then
		ratchet_moby.scale = 3.0
	end
end

function BigRatchetOnUnload()
	ratchet_moby.scale = original_ratchet_scale
end

function BlindToMobiesOnLoad()
	blinded_mobies = {}
	local moby_table = game.moby_table

	for i=1,0x400,1 do
		-- Sleep once in a while
		if (i % 50) == 0 then
			sleep(10)
		end
	
		local moby = Moby(moby_table + (0x100 * i))
		
		if moby.moby_id < 1 then
			goto continue
		end
		
		moby.original_draw_distance = moby.draw_distance
		moby.draw_distance = 2
	
		blinded_mobies[#blinded_mobies + 1] = moby
		
		::continue::
	end
end

function BlindToMobiesOnUnload()
	for key, moby in pairs(blinded_mobies) do
		if (key % 50) == 0 then
			sleep(10)
		end
	
		moby.draw_distance = moby.original_draw_distance
	end
end

function ChonkyMobiesOnLoad()
	chonky_mobies = {}
	local moby_table = game.moby_table

	for i=1,0x400,1 do
		-- Sleep once in a while
		if (i % 50) == 0 then
			sleep(10)
		end
	
		local moby = Moby(moby_table + (0x100 * i))
		
		if moby.moby_id < 1 then
			goto continue
		end
		
		moby.original_scale = moby.scale
		moby.scale = moby.original_scale * 2.0
	
		chonky_mobies[#chonky_mobies + 1] = moby
		
		::continue::
	end
end

function ChonkyMobiesOnUnload()
	for key, moby in pairs(chonky_mobies) do
		if (key % 50) == 0 then
			sleep(10)
		end
		moby.scale = moby.original_scale
	end
end

effescts = {
	{
		name = "Chonky mobies",
		onload = ChonkyMobiesOnLoad,
		onunload = ChonkyMobiesOnUnload,
		tick = nil
	}
}

effects = {
	{
		name = "Yeet Ratchet",
		onload = YeetRatchetOnLoad,
		onunload = nil,
		tick = nil
	},
	{
		name = "Blind to mobies",
		onload = BlindToMobiesOnLoad,
		onunload = BlindToMobiesOnUnload,
		tick = nil
	},
	{
		name = "FoV Fuckery",
		onload = FoVFuckeryOnLoad,
		onunload = FoVFuckeryOnUnload,
		tick = nil
	},
	{
		name = "Invisible Ratchet",
		onload = InvisibleRatchetOnLoad,
		onunload = InvisibleRatchetOnUnload,
		tick = InvisibleRatchetTick
	},
	{
		name = "Chonky mobies",
		onload = ChonkyMobiesOnLoad,
		onunload = ChonkyMobiesOnUnload,
		tick = nil
	},
	{
		name = "One-Hit KO",
		onload = OneHitKOOnLoad,
		onunload = OneHitKOOnUnload,
		tick = OneHitKOTick
	},
	{
		name = "Big Ratchet",
		onload = BigRatchetOnLoad,
		onunload = BigRatchetOnUnload,
		tick = BigRatchetTick
	}
}