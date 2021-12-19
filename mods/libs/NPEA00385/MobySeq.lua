MobySeq = class('MobySeq')

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


function MobySeq:initialize(base_addr)
	self.base_addr = base_addr

	self._offsets = {
		frame_cnt = {
			offset = 0x10,
			size = 1,
			_type = "int"
		},
		sound = {
			offset = 0x11,
			size = 1,
			_type = "int"
		},
		trigs_cnt = {
			offset = 0x12,
			size = 1,
			_type = "int"
		},
		frame_data = {
			offset = 0x1c,
			size = 4,
			_type = "int"
		}
	}
	
	setmetatable(self, mt)
end