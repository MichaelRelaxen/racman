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
			addr = 0xC1E438,
			size = 4,
			_type = "int"
		},
		state = {
			addr = 0xEE9334,
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

function Game:setFastLoads(enabled)
	if enabled then
		print("Enabling fast loads")
		Ratchetron:WriteMemory(GAME_PID, 0x27C2E8, 0x38600004);
		Ratchetron:WriteMemory(GAME_PID, 0x27C2EC, 0x4E800020);
		Ratchetron:WriteMemory(GAME_PID, 0x280E68, 0x60000000);
		Ratchetron:WriteMemory(GAME_PID, 0xAB6688, 0x60000000);
		Ratchetron:WriteMemory(GAME_PID, 0x1D29FC, 0x60000000);
	else
		print("Disabling fast loads")
		Ratchetron:WriteMemory(GAME_PID, 0x27C2E8, 0xf821ff81);
		Ratchetron:WriteMemory(GAME_PID, 0x27C2EC, 0x7c0802a6);
		Ratchetron:WriteMemory(GAME_PID, 0x280E68, 0x9bfe0071);
		Ratchetron:WriteMemory(GAME_PID, 0xAB6688, 0x409effe8);
		Ratchetron:WriteMemory(GAME_PID, 0x1D29FC, 0x408200a4);
	end
end

function Game:loadPlanet(id)
	print("Loading planet ID: " .. id)
	Ratchetron:WriteMemory(GAME_PID, 0xEE9314, id)
	Ratchetron:WriteMemory(GAME_PID, 0xEE9310, 1)
end

game = Game()