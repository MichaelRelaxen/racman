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
			addr = 0x969C70,
			size = 4,
			_type = "int"
		},
		state = {
			addr = 0x00A10708,
			size = 4,
			_type = "int"
		},
		moby_table = {
			addr = 0x0a390a0,
			size = 4,
			_type = "int"
		},
		moby_table_end = {
			addr = 0x0a390a4,
			size = 4,
			_type = "int"
		},
		moby_table_max = {
			addr = 0x00a390a8,
			size = 4,
			_type = "int"
		},
		fov = {
			addr = 0x0095D1C8,
			size = 4, 
			_type = "float"
		},
		_time = {
			addr = 0x00a10710,
			size = 4,
			_type = "int"
		},
		unk_moby_vars_ptr = {
			addr = 0x00a390b0, 
			size = 4,
			_type = "int"
		},
		unk_moby_counter = {
			addr = 0x00a4daf0,
			size = 4,
			_type = "int"
		},
		mobies_update_functions_table = {
			addr = 0x00a34f80,
			size = 4,
			_type = "int"
		},
		mobies_class_table = {
			addr = 0x00a34c00,
			size = 4,
			_type = "int"
		}
	}
	
	setmetatable(self, mt)
end

function Game:setFastLoads(enabled)
	if enabled then
		print("Enabling fast loads")
		Ratchetron:WriteMemory(GAME_PID, 0x0DF254, 0x60000000);
		Ratchetron:WriteMemory(GAME_PID, 0x165450, 0x2C03FFFF);
	else
		print("Disabling fast loads")
		Ratchetron:WriteMemory(GAME_PID, 0x0DF254, 0x40820188);
		Ratchetron:WriteMemory(GAME_PID, 0x165450, 0x2c030000);
	end
end

function Game:loadPlanet(id)
	print("Loading planet ID: " .. id)
	Ratchetron:WriteMemory(GAME_PID, 0xA10704, id)
	Ratchetron:WriteMemory(GAME_PID, 0xA10700, 1)
end

game = Game()