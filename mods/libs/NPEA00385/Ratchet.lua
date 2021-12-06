Ratchet = class('Ratchet')

local mt = {}

mt.__index = function(self, key) 
	if self._addresses[key] ~= nil then
		local obj = self._addresses[key]
		
		local mem = Ratchetron:ReadMemory(GAME_PID, obj.addr, obj.size)
		
		return bytestoint(mem)
	else
		return Ratchet.__instanceDict[key]
	end
end

mt.__newindex = function(self, key, value)
	if self._addresses[key] ~= nil then
		local obj = self._addresses[key]
		
		Ratchetron:WriteMemory(GAME_PID, obj.addr, value)
	else
		rawset(self, key, value)
	end
end


function Ratchet:initialize()
	self._addresses = { 
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
		}
	}
	setmetatable(self, mt)
end

ratchet = Ratchet()