Game = class('Game')

local mt = {}

mt.__index = function(self, key) 
	if self._addresses[key] ~= nil then
		local obj = self._addresses[key]
		
		local mem = Ratchetron:ReadMemory(GAME_PID, obj.addr, obj.size)
		
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
	if self._addresses[key] ~= nil then
		local obj = self._addresses[key]
		
		if obj._type == "float" then
			Ratchetron:WriteMemory(GAME_PID, obj.addr, obj.size, floattobytes(value))
		else
			-- Default to int
			Ratchetron:WriteMemory(GAME_PID, obj.addr, obj.size, inttobytes(value, obj.size))
		end
	else
		rawset(self, key, value)
	end
end


function Game:initialize()
	self._addresses = { 
		planet = {
			addr = 0x119353C,
			size = 4,
			_type = "int"
		},
		state = {
			addr = 0x9C3240,
			size = 4,
			_type = "int"
		},
		moby_table = {
			addr = 0x0f22260,
			size = 4,
			_type = "int"
		},
		moby_table_top = {
			addr = 0x00f22268,
			size = 4,
			_type = "int"
		}
	}
	
	setmetatable(self, mt)
end

function Game:loadPlanet(id)
	print("Loading planet ID: " .. id)
	Ratchetron:WriteMemory(GAME_PID, 0x0B36DD0, id)
	Ratchetron:WriteMemory(GAME_PID, 0x0B36DCC, 1)
end

game = Game()