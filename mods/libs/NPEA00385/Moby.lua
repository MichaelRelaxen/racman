Moby = class('Moby')

local mt = {}

mt.__index = function(self, key) 
	if self._offsets[key] ~= nil then
		local obj = self._offsets[key]
		
		local mem = Ratchetron:ReadMemory(GAME_PID, self.base_addr + obj.offset, obj.size)
		
		if obj._type == "float" then
			return bytestofloat(mem)
		end
		
		-- Fallback return int
		return bytestoint(mem)
	else
		return self.class.__instanceDict[key]
	end
end

mt.__newindex = function(self, key, value)
	if self._offsets[key] ~= nil then
		local obj = self._offsets[key]
		
		if obj._type == "float" then
			Ratchetron:WriteMemory(GAME_PID, self.base_addr + obj.offset, obj.size, floattobytes(value))
		else
			-- Default to int
			Ratchetron:WriteMemory(GAME_PID, self.base_addr + obj.offset, obj.size, inttobytes(value, obj.size))
		end
	else
		rawset(self, key, value)
	end
end


function Moby:initialize(base_addr)
	self.base_addr = base_addr

	self._offsets = { 
		alpha = {
			offset = 0x23,
			size = 1,
			_type = "int"
		},
		moby_id = {
			offset = 0xa6,
			size = 2,
			_type = "int"
		},
		scale = {
			offset = 0x2c,
			size = 4,
			_type = "float"
		},
		draw_distance = {
			offset = 0x32,
			size = 2,
			_type = "int"
		}
	}
	
	setmetatable(self, mt)
end

function Moby.static:findFirst(id)
	local current_addr = game.moby_table
	local max_search_space = current_addr + (0x100 * 0x2000)
	
	print("Searching mobies from addr " .. string.format("%x", current_addr))

	while true do
		local moby_id = bytestoint(Ratchetron:ReadMemory(GAME_PID, current_addr + 0xA6, 2))
		
		if moby_id == id then
			print("Found moby " .. tostring(id) .. " at " .. string.format("%x", current_addr))
			return Moby(current_addr)
		end
		
		current_addr = current_addr + 0x100
		
		if current_addr > max_search_space then
			break
		end
	end
	
	print("Couldn't find Moby " .. tostring(id))
end

function Moby.static:findAll(id)
	local current_addr = game.moby_table
	local max_search_space = current_addr + (0x100 * 0x2000)
	
	print("Searching mobies with ID " .. id .. " from addr " .. string.format("%x", current_addr))
	
	local mobies = {}

	while true do
		local moby_id = bytestoint(Ratchetron:ReadMemory(GAME_PID, current_addr + 0xA6, 2))
		
		if moby_id == id then
			print("Found moby " .. tostring(id) .. " at " .. string.format("%x", current_addr))
			mobies[#mobies + 1] = Moby(current_addr)
		end
		
		current_addr = current_addr + 0x100
		
		if current_addr > max_search_space then
			break
		end
	end
	
	print("Found " .. #mobies .. " with ID " .. id)
end