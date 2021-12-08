Ratchet = class('Ratchet')

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
		return Ratchet.__instanceDict[key]
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


function Ratchet:initialize()
	self._addresses = {
		x = {
			addr = 0x00969D60,
			size = 4,
			_type = "float"
		},
		y = {
			addr = 0x00969D64,
			size = 4,
			_type = "float"
		},
		z = {
			addr = 0x00969D68,
			size = 4,
			_type = "float"
		},
		bolts = {
			addr = 0x969CA0,
			size = 4,
			_type = "int"
		},
		health = {
			addr = 0x96BF88,
			size = 4,
			_type = "int"
		},
		state = {
			addr = 0x96BD64,
			size = 4,
			_type = "int"
		},
		moon_jump = {
			addr = 0x0969DC8,
			size = 4, 
			_type = "float"
		}
	}
	setmetatable(self, mt)
end

ratchet = Ratchet()