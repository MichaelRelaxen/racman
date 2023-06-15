the_scale = 10
the_moby = nil
flag_loc = nil

function LookForBike()
    local mobies = Moby:findAll(-1)
    for _, moby in ipairs(mobies) do
        if moby.o_class == 3004 then
            print("Found bike")
            -- pVar = *(uint32*)(pMoby+0x68) 
            -- address_of_flag = pVar + 0x31C  
            -- thanks creep
            local p_var = moby.p_var_bike_thing
            flag_loc = p_var + 0x31F -- 31c was wrong lol
            print("flag at:" .. flag_loc)
            set_lap_flag_addr(flag_loc)

            return
        end
    end
end

function MoveBarlowThing()
    -- Couldn't get Moby:findFirst to work so lets try this instead
    local mobies = Moby:findAll(-1)
    for _, moby in ipairs(mobies) do
        -- Look for one of those arrow signs
        if moby.o_class == 2538 then 
            print("Found a cool terrain moby to steal")
            the_moby = moby

            -- local mode_bits = the_moby.mode_bits
            -- the_moby.mode_bits = mode_bits | 0x20
            break
        end
    end

    if the_moby == nil then
        print("No moby?? Crap")
    end
   
	-- the_moby.x = 789.299
	-- the_moby.y = 725.176
	-- the_moby.z = 348.381
    -- the_moby.x = ratchet.x
	-- the_moby.y = ratchet.y
	-- the_moby.z = ratchet.z
	-- the_moby.rotation_x = -3.142
	-- the_moby.rotation_y = -1.556
	-- the_moby.rotation_z = -1.556
	-- the_moby.scale = the_scale
end

	-- local x = ratchet.x
	-- local y = ratchet.y
	-- local z = ratchet.z - 4.0
	-- local z_rot = ratchet.rotation_z + 1.5
	
	-- print("X: " .. x .. "\tY: " .. y .. "\tZ: " .. z .. "\tZRot: " .. z_rot)
	
    

-- end

function MoveRatchet()
	ratchet.x = 387.59375
	ratchet.y = 273.19262695313
	ratchet.z = 56.0
end

function OnLoad()
    barlow = 4
	waiting_for_planet_load = false

	if game.planet == barlow then
		MoveBarlowThing()
		MoveRatchet()
        LookForBike()
	else
		waiting_for_planet_load = true
		-- game:setFastLoads(true)
		game:loadPlanet(barlow)
	end
end

function OnTick(ticks)
	if waiting_for_planet_load then
		if game.state == 0 and ratchet.state == 0 then  -- If game state is player controllable, level must have finished loading
			MoveBarlowThing()
			MoveRatchet()
            LookForBike()

			waiting_for_planet_load = false
		end
	else

    if (not waiting_for_planet_load) and (ticks % 1000) == 0 then
        if game.planet ~= barlow then
            -- User isn't on Barlow anymore, they should be! 
            -- We don't want people to use this in runs.
            game:loadPlanet(barlow)
        end
    end

		-- if (ticks % 10) == 0 then  -- Do this every 10 ticks
		-- 	if the_moby.scale ~= the_scale then  -- The thing must have moved back, prob after player died
		-- 		MoveBarlowThing()
		-- 	end
		-- end 
	end
end

function OnUnload()

end