require 'deepcopy'
require 'io'
require 'crc32'
require 'logicChosen'

function GetItem(name)
	for i, item in ipairs(items) do
		if name == item[name] then
			return item
		end
	end
end

function PlanetSpecificFix(planet, replaced_planet)
	---------
	-- Infobot vendors
	---------

	-- Aridia infobot, assign right coords to plumber at Novalis update func
	if planet == 2 then
		Ratchetron:WriteMemory(GAME_PID, 0x4558b8 + 3, 1, inttobytes(replaced_planet, 1))
	end

	-- Umbris infobot, assign right coords to Rilgar Bouncer update func
	if planet == 7 then
		Ratchetron:WriteMemory(GAME_PID, 0x4b1ba8 + 3, 1, inttobytes(replaced_planet, 1))
	end

	-- Gaspar infobot, assign right coords to Deserter at Batalia update func
	if planet == 9 then
		Ratchetron:WriteMemory(GAME_PID, 0x1df984 + 3, 1, inttobytes(replaced_planet, 1))
	end

	-- Orxon infobot, assign right coords to Commando at Batalia update func
	if planet == 10 then
		Ratchetron:WriteMemory(GAME_PID, 0x1d82a0 + 3, 1, inttobytes(replaced_planet, 1))
	end

	-- Quartu infobot, assign right coords to Oltanis Scrap Merchant update func
	if planet == 15 then
		Ratchetron:WriteMemory(GAME_PID, 0x4b7e04 + 3, 1, inttobytes(replaced_planet, 1))
	end

	-- Kalebo III infobot, assign right coords to Quartu Scientist update func
	if planet == 16 then
		Ratchetron:WriteMemory(GAME_PID, 0x267d80 + 3, 1, inttobytes(replaced_planet, 1))
	end

	---------
	-- Other
	---------

	-- Hoven infobot, something specific in the infobot update func
	if planet == 0xc then
		Ratchetron:WriteMemory(GAME_PID, 0x44b884 + 3, 1, inttobytes(replaced_planet, 1))
	end

	-- Redirect Veldin 1 to not Novalis
	if planet == 1 then
		Ratchetron:WriteMemory(GAME_PID, 0x4814dc + 3, 1, inttobytes(replaced_planet, 1))
	end
end

function ReplaceItem(item, replaced_item)
	-- Vendor items (ignoring Swingshot, Succ, Morpho and RYNO)
	if (item >= 0xb and item <= 0x19 and item ~= 0xc and item ~= 0x16 and item ~= 0x15 and item ~= 0x17) then
		for index, planet in ipairs(planets) do
			if InArray(planet.items, item) then
				if replaced_item >= 0x30 then
					print("YO WTF I'M WRITING ILLEGAL ITEM " .. replaced_item .. " TO VENDOR ITEM " .. item)
				end

				Ratchetron:WriteMemory(GAME_PID, 0x737bf0 + (planet.id * 4), 4, inttobytes(replaced_item, 4))
				Ratchetron:WriteMemory(GAME_PID, 0xB00150 + replaced_item, 1, inttobytes(1, 1))
			end
		end
	end

	Ratchetron:WriteMemory(GAME_PID, 0xB00100 + item, 1, inttobytes(replaced_item, 1))
end

function PlanetForItem(item_id)
	for i, planet in pairs(planets) do
		for j, item in ipairs(planet.items) do
			if item == item_id then
				return planet.id
			end
		end
	end

	return -1
end

--- Deep copy of tables

-- Shuffle table
function shuffle(tbl)
  for i = #tbl, 2, -1 do
    local j = math.random(i)
    tbl[i], tbl[j] = tbl[j], tbl[i]
  end
end

function GetItemWithID(id)
	for i, item in ipairs(items) do
		if item.id == id then
			return item
		end
	end

	print("Couldn't find item: " .. id)

	return -1
end

function RemoveItem(tbl, id)
	for i, item in ipairs(tbl) do
		if item.id == id then
			table.remove(tbl, i)
			return
		end
	end

	print("Couldn't find item: " .. id)

	return -1
end

function InArray (tab, val)
    for index, value in ipairs(tab) do
        if value == val then
            return true
        end
    end

    return false
end

file = nil

write_debug_graph = true

function filewrite(text)
	if write_debug_graph == false then
		return
	end

	file:write(text)
	file:flush()
end


function Randomize(seed)
	math.randomseed(seed)

	-- GraphViz file for debugging purposes. Use something like https://dreampuf.github.io/GraphvizOnline/ to view the graph.
	file = io.open('randomizer_graph.dot', 'w')
	filewrite("#Copy the contents in: https://dreampuf.github.io/GraphvizOnline/\n\n")
	filewrite("digraph {\nbgcolor=black\nnode [shape=rect, color=white, fontcolor=white]\n")

	-- Pool of planets we haven't used, planets with outs we haven't used, and free item slots where we can place item requirements we might need to fill
	local available_planets = table.deepcopy(planets)
	local available_outs = {{planet=0, infobot=table.deepcopy(infobots[1])}}  -- Add Veldin1 with Novalis infobot as first available out
	local available_item_slots = {}

	-- List to keep track of items we haven't placed yet so we can fill them out at the end.
	-- When making the planet graph we only place items that are required to traverse that path, other items are just placed in remaining available item slots at the end of the program.
	local remaining_items = table.deepcopy(items)

	-- Planets and items placed and locked
	local planet_list = {}
	local item_list = {}

	local finished = false

	while not finished do
		file:flush()
		-- Find next planet to add to graph
		local found_planet = nil
		local planet_index = 0

		::continue_planet_search::
		while found_planet == nil do
			planet_index = math.ceil(math.random() * #available_planets)

			local working_planet = available_planets[planet_index]

			-- If we only have 1 available out, we need to select a planet with at least 1 infobot, otherwise we're stuck in a dead end.
			if #available_outs <= 1 and #working_planet["infobots"] <= 0 and #available_planets > 1 then
				goto continue_planet_search
			end

			found_planet = working_planet
		end

		--print("Found planet: " .. found_planet.id)

		local n_min_item_slots_needed = 0
		local n_max_item_slots_needed = 0


		::find_available_out::
		-- Find next out we meet the requirements for
		local out_index = #available_outs
		for i=#available_outs, 1, -1 do
			if #available_outs[i].infobot.req_items <= 0 then
				out_index = i
				--print("No items required")
				goto found_available_out  -- No item requirements, just continue
			end

			-- Since there are different combinations of items that can be used to get an infobot, look through all of them
			filewrite("# Finding requirements for infobot: " .. available_outs[i].infobot.id .. "\n")
			for j, req_combination in ipairs(available_outs[i].infobot.req_items) do
				if #req_combination > n_max_item_slots_needed then
					n_max_item_slots_needed = #req_combination
				end

				if #req_combination < n_min_item_slots_needed or n_min_item_slots_needed == 0 then
					n_min_item_slots_needed = #req_combination
				end

				for m, required in pairs(req_combination) do
					local found_items = 0

					-- Find out if the current item requirement is in the list of items we've already placed
					for l, available_item in pairs(item_list) do
						if required == available_item then
							found_items = found_items + 1
						end
					end

					if found_items >= #req_combination then
						out_index = i
						filewrite("# Requirements filled for " .. available_outs[i].infobot.id .. "\n")
						goto found_available_out  -- We met the requirements for at least 1 combination of items
					end
				end
			end
		end

		if n_min_item_slots_needed > 0 and #available_item_slots <= 0 then
			--print("Can't continue. No available item slots to place items.")
			found_planet = nil

			return -1
		end

		--  Didn't find an available out; count available item slots that we can fill
		for i, item_slot in pairs(available_item_slots) do
			local requirement_combinations = table.deepcopy(item_slot.item.req_items)

			local n_min_item_slots = 0
			local n_max_item_slots = 0

			for j, requirements in pairs(requirement_combinations) do
				local n_requirements = 0

				local next_req = {}

				for jj, requirement in ipairs(requirements) do
					next_req[#next_req + 1] = requirement
				end

				local passes2 = 0
				while #next_req > 0 do
					passes2 = passes2 + 1

					if passes2 > 100 then
						--print("Shit's fucking fucked dude")
						return -1
					end

					local item = GetItemWithID(next_req[1])

					-- Find out if the current item requirement is in the list of items we've already placed
					local has_item = false
					for l, available_item in pairs(item_list) do
						if item.id == available_item then
							has_item = true
						end
					end

					if not has_item then
						n_requirements = n_requirements + 1

						if #item.req_items > 0 then
							for k, req in ipairs(item.req_items[math.ceil(math.random() * #item.req_items)]) do
								local has_requirement = false
								for l, available_item in pairs(item_list) do
									if item.id == available_item then
										has_requirement = true
									end
								end

								if not has_requirement then
									n_requirements = n_requirements + 1
								end
							end
						end
					end

					table.remove(next_req, 1)
				end

				if n_requirements < n_min_item_slots or n_min_item_slots == 0 then
					n_min_item_slots = n_requirements
				end

				if n_requirements > n_max_item_slots then
					n_max_item_slots = n_requirements
				end
			end

			n_max_item_slots_needed = n_max_item_slots_needed + n_max_item_slots
			n_min_item_slots_needed = n_min_item_slots_needed + n_min_item_slots
		end


		-- Try to fill available item slots to meet requirements for another out, starting with first available out
		--filewrite("# Filling available outs for " .. found_planet.name .. "\n")
		for i, out in pairs(available_outs) do
			--filewrite("# Available out: " .. out .. "\n")
			-- Try to fill the requirement in previous planets first
			local requirement_combinations = table.deepcopy(out.infobot.req_items)

			shuffle(requirement_combinations)
			for j, requirements in ipairs(requirement_combinations) do
				local n_requirements = 0  -- Requirements we have to fill

				local next_req = {}

				-- Recursively count requirements for combination
				for jj, requirement in ipairs(requirements) do
					next_req[#next_req + 1] = requirement
				end

				local passes1 = 0
				while #next_req > 0 do

					passes1 = passes1 + 1

					if passes1 > 100 then
						--print("Dude shit's fucked")
						return -1
					end

					local item = GetItemWithID(next_req[1])

					-- Find out if the current item requirement is in the list of items we've already placed
					local has_item = false
					for l, available_item in ipairs(item_list) do
						if item.id == available_item then
							has_item = true
						end
					end

					if not has_item then
						n_requirements = n_requirements + 1

						if #item.req_items > 0 then
							for k, req in ipairs(item.req_items[math.ceil(math.random() * #item.req_items)]) do
								local has_requirement = false
								for l, available_item in pairs(item_list) do
									if item.id == available_item then
										has_requirement = true
									end
								end

								if not has_requirement then
									n_requirements = n_requirements + 1
								end
							end
						end
					end

					table.remove(next_req, 1)
				end

				if n_requirements <= n_min_item_slots_needed then
					-- We can start filling out items with the most convoluted route
					local requirements_left = table.deepcopy(requirements)
					local passes = 0

					::continue_requirement_search::

					local num_requirements_left = #requirements_left
					while num_requirements_left > 0 do
						passes = passes + 1

						if passes > 60 then
							--print("Dude shit's fucked")
							return -1
						end

						for l, available_item in pairs(item_list) do
							if requirements_left[1] == available_item then
								table.remove(requirements_left, 1)

								--print("Met requirement " .. available_item .. ". Still need to meet:")
								for iii, r in ipairs(requirements_left) do
									--print("\t" .. r)
								end

								goto continue_requirement_search
							end
						end

						local item_slot_id = math.ceil(math.random() * #available_item_slots)
						local item_slot = available_item_slots[item_slot_id]

						-- Check if this item slot has any illegal item replacements
						if item_slot.item.ill_items ~= nil then
							for index, value in ipairs(item_slot.item.ill_items) do
								if value == requirements_left[1] then
									requirements_left[#requirements_left + 1] = requirements_left[1]
									table.remove(requirements_left, 1)
									filewrite("# Encountered illegal item to replace " .. item_slot.item.name .. "\n")
									goto continue_requirement_search
								end
							end
						end

						-- Try to prevent putting items on the planets they are required for, makes for more interesting generation.
						-- It's still possible, this just does a reroll.
						if #available_item_slots > 1 and planets[available_outs[out_index].planet].id == item_slot.planet then
							filewrite("# Trying to pick a different item slot\n")
							item_slot_id = math.ceil(math.random() * #available_item_slots)
							item_slot = available_item_slots[item_slot_id]
						elseif planets[available_outs[out_index].planet].id == item_slot.planet then
							filewrite("# Putting item on same planet as its requirement because only " .. #available_item_slots .. " were available.\n")
						end

						if #available_item_slots <= 0 then
							--print("No more available slots, shit's fucked.")
							return  -1
						end

						local n_requirements_met = 0

						-- Check that this item isn't a requirement for the available slot unless we already meet other requirements
						can_meet = true

						for ii, reqs in pairs(item_slot.item.req_items) do
							n_requirements_met = 0
							for kk, requirement in pairs(reqs) do
								if requirement == requirements_left[1] then
									--print(item_slot.item.name .. " can not be replaced with " .. GetItemWithID(requirements_left[1]).name)
									can_meet = false
									n_requirements_met = 0
								end

								can_meet = false
								for jj, available_item in pairs(item_list) do
									if requirement == available_item then
										--print(item_slot.item.name .. " met a requirement for " .. GetItemWithID(requirements_left[1]).name .. ", even though it is a requirement in a different combination")
										can_meet = true
										--filewrite("# " .. GetItemWithID(requirements_left[1]).name .. " met requirement for " .. GetItemWithID(requirement).name .. "\n")
										--n_requirements_met = n_requirements_met + 1
									end
								end

								if can_meet or has_item then
									filewrite("# " .. GetItemWithID(requirements_left[1]).name .. " met requirement for " .. GetItemWithID(requirement).name .. "\n")

									n_requirements_met = n_requirements_met + 1
								end
							end

							if n_requirements_met >= #requirements then
								filewrite("# " .. GetItemWithID(requirements_left[1]).name .. " n_requirements_met: " .. n_requirements_met .. ", #requirements: " .. #requirements .. "\n")
								can_meet = true
								goto continue_item_assignment
							end
						end

						if #item_slot.item.req_items > 0 then
						  can_meet = false
						end

						::continue_item_assignment::

						if not can_meet then  -- Can't meet requirements yet, put this requirement at end of list
							--print("Can't meet requirements for " .. GetItemWithID(requirements_left[1]).name .. ", bumping to end of list. Available slots left: " .. #available_item_slots)

							local req = table.deepcopy(requirements_left[1])
							table.remove(requirements_left, 1)
							requirements_left[#requirements_left + 1] = req
						else
							local required_items = item_slot.item.req_items[math.ceil(math.random() * #item_slot.item.req_items)]

							if n_requirements < n_max_item_slots_needed then
								-- Find smallest item requirement combinations
								local item_requirement_combinations = item_slot.item.req_items
								shuffle(item_requirement_combinations)
								local required_items = {}
								for ii, item_reqs in pairs(item_requirement_combinations) do
									if #required_items <= 0 or #item_reqs < #required_items then
										required_items = item_reqs
									end
								end
							end

							if required_items ~= nil and #required_items > 0 then
								for ii, item in pairs(required_items) do
									-- Find out if the current item requirement is in the list of items we've already placed
									local has_item = false
									for l, available_item in ipairs(item_list) do
										if item.id == available_item then
											has_item = true
										end
									end

									if not has_item then
										requirements_left[#requirements_left + 1] = item
									end
								end
							end

							if item_slot.item.ill_items ~= nil then
								filewrite('"' .. planets[item_slot.planet].name .. '" -> "' .. GetItemWithID(requirements_left[1]).name .. '\"[color="#ff0000",label="' .. item_slot.item.name .. '",fontcolor=white,fontsize=8]\n')
							else
								filewrite('"' .. planets[item_slot.planet].name .. '" -> "' .. GetItemWithID(requirements_left[1]).name .. '\"[color="#00ff00",label="' .. item_slot.item.name .. '",fontcolor=white,fontsize=8]\n')
							end

							--print(item_slot.item.name .. " -> " .. GetItemWithID(requirements_left[1]).name)
							item_list[item_slot.item.id] = requirements_left[1]

							RemoveItem(remaining_items, requirements_left[1])

							table.remove(available_item_slots, item_slot_id)
							table.remove(requirements_left, 1)
						end

						num_requirements_left = #requirements_left

						if (#requirements_left <= 0) then
							goto find_available_out
						end
					end
				end
			end
		end

		::found_available_out::

		-- Register new available item slots
		shuffle(found_planet.items)  -- Randomize items
		for i, item in pairs(found_planet.items) do
			available_item_slots[#available_item_slots + 1] = {planet=found_planet.id, item=GetItemWithID(item)}
		end

		-- Register new available outs
		shuffle(found_planet.infobots)  -- Randomize infobots
		for i, infobot in pairs(found_planet.infobots) do
			available_outs[#available_outs + 1] = {planet=found_planet.id, infobot=table.deepcopy(infobots[infobot])}
		end

		--print("out_index: " .. out_index)
		--for i, out in pairs(available_outs) do
		--	print(i .. ": " .. out.planet .. " : " .. out.infobot.id)
		--end

		--print(available_outs[out_index].infobot.id .. " -> " .. found_planet.id)

		if available_outs[out_index].planet == 0 then
			filewrite('"Veldin1" -> "' .. found_planet.name .. '"[color="#ffffff",label="Novalis",fontcolor=white,fontsize=8]\n')
		else
			local orig_infobot_name = "Veldin2"

			if available_outs[out_index].infobot.id < 18 then
				orig_infobot_name = planets[available_outs[out_index].infobot.id].name  -- Fix special case for Veldin2
			end

			filewrite('"' .. planets[available_outs[out_index].planet].name .. '" -> "' .. found_planet.name .. '"[color="#ffffff",label="' .. orig_infobot_name .. '",fontcolor=white,fontsize=8]\n')
		end

		planet_list[available_outs[out_index].infobot.id] = found_planet.id  -- Replace infobot with next planet we found
		table.remove(available_outs, out_index)  -- We just used an available out, remove it
		table.remove(available_planets, planet_index)

		-- Add Veldin2 to list of available planets if we've placed all other planets.
		-- This doesn't necessarily make the longest path, but it's good enough for me.
		if #available_planets <= 0 and #available_outs > 0 then
			available_planets[1] = {id=18,name="Veldin2",  infobots={}, items={}}
			planets[18] = {id=18,name="Veldin2",  infobots={}, items={}}
		end

		if #available_planets <= 0 then
			finished = true
		end

		--print("-")
		--print("")
	end

	-- Fill remaining items in remaining available item slots
	--print("**** Filling remaining " .. #available_item_slots .. " item slots!")
	shuffle(remaining_items)
	local passes3 = 0
	for i, item_slot in ipairs(available_item_slots) do
		::fuck::

		-- Deadlock prevention, idk if it's strictly necessary. Shit code is shit
		passes3 = passes3 + 1
		if passes3 > 200 then
			return -1
		end

		if item_slot.item.ill_items ~= nil then
			if InArray(item_slot.item.ill_items, remaining_items[1].id) then
				shuffle(remaining_items)
				goto fuck
			end
		end

		if item_slot.item.ill_items ~= nil then
			filewrite('"' .. planets[item_slot.planet].name .. '" -> "' .. remaining_items[1].name .. '\"[color="#ff00ff",label="' .. item_slot.item.name .. '",fontcolor=white,fontsize=8]\n')
		else
			filewrite('"' .. planets[item_slot.planet].name .. '" -> "' .. remaining_items[1].name .. '\"[color="#5555ff",label="' .. item_slot.item.name .. '",fontcolor=white,fontsize=8]\n')
		end

		--print(item_slot.item.name .. " -> " .. remaining_items[1].name)


		item_list[item_slot.item.id] = remaining_items[1].id

		table.remove(remaining_items, 1)
	end

	if #remaining_items > 0 then
		print("*************** Yo why are there " .. #remaining_items .. " items still not placed?")
	end

	filewrite("}\n")
	file:close()

	-- Apply planet and gadget/item replacements
	for i=1,18 do
		Ratchetron:WriteMemory(GAME_PID, 0xB00000 + i, 1, inttobytes(planet_list[i], 1))

		PlanetSpecificFix(i, planet_list[i])
	end

	for id, replacement in pairs(item_list) do
		ReplaceItem(id, replacement)
	end

	return 1
end

function trim1(s)
   return (s:gsub("^%s*(.-)%s*$", "%1"))
end

function OnLoad()
	-- Clear any previous randomizer data
	memset(0xb00000, 0, 0x300)

	local seed = os.time()

	local seed_file = io.open("seed.txt", "r")
	if seed_file ~= nil then
		local seed_text = seed_file:read("*a*")

		seed = LibDeflate:Crc32(trim1(seed_text), 0)

		if string.find(seed_text:gsub("%s+", ""), "#graph:false") then
			write_debug_graph = false
		end

		seed_file:close()
	end

	-- Repeatedly generate new path until it works. Bad code makes generation hard
	while Randomize(seed) <= 0 do
		--print("")
		--print("-----------------")
		--print("-----------------")  -- Just print a bunch of newlines so it's easy to distinguish attempts in the console
		--print("")
		seed = seed + 1
	end

	print("* Done!")

	if (game.planet == 0) then
		game:loadPlanet(0)
	end
end

function OnTick(ticks)

end

function OnUnload()

end
