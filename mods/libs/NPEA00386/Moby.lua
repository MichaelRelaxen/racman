Moby = class('Moby')

local mt = {}

mt.__index = function(self, key)
	if self._offsets[key] ~= nil then
		local obj = self._offsets[key]
		
		local mem
		
		if self.auto_commit ~= true then
			mem = get_ba_range(self.memory_cache, obj.offset, obj.size)
			-- mem = ba({table.unpack(self.memory_cache, obj.offset + 1, obj.offset + obj.size)})
		else
			mem = Ratchetron:ReadMemory(GAME_PID, self.base_addr + obj.offset, obj.size)
		end
		
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
		local memory_to_write
		
		if obj._type == "float" then
			memory_to_write = floattobytes(value)
		else
			-- Default to int
			memory_to_write = inttobytes(value, obj.size)
		end
		
		if self.auto_commit == true then
			Ratchetron:WriteMemory(GAME_PID, self.base_addr + obj.offset, obj.size, memory_to_write)
		end
		
		-- Always write to memory cache
		for i=0,obj.size - 1,1 do
			self.memory_cache[obj.offset + i] = memory_to_write[i]
		end
	else
		rawset(self, key, value)
	end
end


function Moby:initialize(base_addr, mem)
	self.base_addr = base_addr
	
	self.auto_commit = true
	
	if mem ~= nil then
		self.memory_cache = mem
	else
		-- Make memory cache for 
		self.memory_cache = {}
		for i=1, 0x100, 1 do
			self.memory_cache[i] = 0x0
		end
		
		self.memory_cache = ba(self.memory_cache)
	end

	self._offsets = {
		x = {
			offset = 0x10,
			size = 4,
			_type = "float"
		},
		y = {
			offset = 0x14,
			size = 4,
			_type = "float"
		},
		z = {
			offset = 0x18,
			size = 4,
			_type = "float"
		},
		state = {
			offset = 0x20,
			size = 1,
			_type = "int"
		},
		group = {
			offset = 0x21,
			size = 1,
			_type = "int"
		},
		m_class = {
			offset = 0x22,
			size = 1,
			_type = "int"
		},
		alpha = {
			offset = 0x23,
			size = 1,
			_type = "int"
		},
		p_class = {
			offset = 0x24,
			size = 4,
			_type = "int"
		},
		scale = {
			offset = 0x2c,
			size = 4,
			_type = "float"
		},
		update_distance = {
			offset = 0x30,
			size = 1,
			_type = "int"
		},
		enabled = {
			offset = 0x31,
			size = 1,
			_type = "int"
		},
		draw_distance = {
			offset = 0x32,
			size = 2,
			_type = "int"
		},
		mode_bits = {
			offset = 0x34,
			size = 2,
			_type = "int"
		},
		rotation_x = {
			offset = 0x40,
			size = 4,
			_type = "float"
		},
		rotation_y = {
			offset = 0x44,
			size = 4,
			_type = "float"
		},
		rotation_z = {
			offset = 0x48,
			size = 4,
			_type = "float"
		},
		update_func_ptr_ptr = {
			offset = 0x64,
			size = 4,
			_type = "int"
		},
        p_var_bike_thing = {
            offset = 0x68,
            size = 4,
            _type = "int"
        },
		o_class = {
			offset = 0xaa,
			size = 2,
			_type = "int"
		}
	}
	
	setmetatable(self, mt)
end

function Moby:commit() 
	Ratchetron:WriteMemory(GAME_PID, self.base_addr, 0x100, ba(self.memory_cache))
end

function Moby.static:findFirst(id)
	return Moby:findAll(id)[1]
end

function Moby.static:findAll(id)
	local moby_table = game.moby_table
	local max_search_space = game.moby_table_top - moby_table
	
	print("Searching mobies with ID " .. id .. " from addr " .. string.format("%x", moby_table))
	
	local mobies_address_space = read_large(moby_table, max_search_space)
	
	print("Done reading memory, looking for relevant mobies")
	
	local mobies = {}
	
	local current_addr = 0
	
	while true do
		local moby_id = bytestoint(get_ba_range(mobies_address_space, current_addr + 0xaa, 2)) -- ba({ table.unpack(mobies_address_space, current_addr + 1 + 0xa6, current_addr + 1 + 0xa6 + 2) }))
		if moby_id == id or id == -1 then
			mobies[#mobies + 1] = Moby(moby_table + current_addr, get_ba_range(mobies_address_space, current_addr, 0x100))
		end
		
		current_addr = current_addr + 0x100
		
		if current_addr > max_search_space then
			break
		end
	end
	
	print("Found " .. #mobies .. " with ID " .. id)
	
	return mobies
end
