MobyClass = class('MobyClass')

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


function MobyClass:initialize(base_addr)
	self.base_addr = base_addr

	self._offsets = {
		shadow = {
			offset = 0x0f,
			size = 1,
			_type = "int"
		},
		collision = {
			offset = 0x10,
			size = 4,
			_type = "int"
		},
		g_scale = {
			offset = 0x24,
			size = 4,
			_type = "float"
		},
		b_sphere = {
			offset = 0x30,
			size = 4,
			_type = "int"
		},
		unk_ptr = {
			offset = 0x40,
			size = 4,
			_type = "int"
		},
		mode_bits = {
			offset = 0x44,
			size = 2,
			_type = "int"
		},
		seqs = {
			offset = 0x48,
			size = 4,
			_type = "int"
		}
	}
	
	setmetatable(self, mt)
end