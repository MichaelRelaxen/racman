require 'effects'  -- Look at effects.lua in this folder to see how effects are applied.

-- Function that runs once when the automation is loaded.
function OnLoad()
	print("Loading automation...")
	application_frequency = 60 * 10  -- Every 15 seconds at 60 ticks per second
	
	last_application_tick = 0 - application_frequency  -- To apply the first effect at mod startup
	
	applied_effect = nil
end

-- OnTick runs ever 16ms, so that should be about 60 times per second
-- The `ticks` argument is the current tick, it counts up by 1 for every tick.
function OnTick(ticks)
	if ticks > last_application_tick + application_frequency then
		-- Set last effect application tick counter to current ticks
		last_application_tick = ticks
	
		-- Unload the currently running effect, if any.
		if applied_effect ~= nil then
			print("Unloading effect: " .. applied_effect.name)
		
			-- Some effects don't need to have an unload function
			if applied_effect.onunload ~= nil then
				applied_effect.onunload()
			end
		end
		
		-- Select a random effect
		math.randomseed(os.time())
		local effect_index = math.floor(math.random() * #effects) + 1
		
		applied_effect = effects[effect_index]
		
		print("Applying effect: " .. applied_effect.name)
		
		-- Run the effect's onload function, if any
		if applied_effect.onload ~= nil then
			applied_effect.onload()
		end
	end
	
	-- Run current effect's tick-function, if any
	if applied_effect ~= nil and applied_effect.tick ~= nil then
		applied_effect.tick(ticks)
	end
end

-- This function runs as the mod is being unloaded, either through an execution error or the user unloading the mod manually
function OnUnload()
	print("Unloading automation")

	-- Unload current effect, if any
	if applied_effect ~= nil then
		if applied_effect.onunload ~= nil then
			applied_effect.onunload()
		end
	end
end