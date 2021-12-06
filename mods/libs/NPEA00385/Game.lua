Game = class('Game')

local mt = {}

mt.__index = function(self, key) 
	if self._addresses[key] ~= nil then
		local obj = self._addresses[key]
		
		local mem = Ratchetron:ReadMemory(GAME_PID, obj.addr, obj.size)
		
		return bytestoint(mem)
	else
		return self.class.__instanceDict[key]
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
		}
	}
	local old = getmetatable(self)
	old.__index = mt.__index
	old.__newindex = mt.__newindex
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