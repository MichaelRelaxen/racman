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
		field_0x36 = {
			offset = 0x36,
			size = 2,
			_type = "int"
		},
		state_timer = {
			offset = 0x38,
			size = 4,
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
		field_0x50 = {
			offset = 0x50,
			size = 1,
			_type = "int"
		},
		field_0x51 = {
			offset = 0x51,
			size = 1,
			_type = "int"
		},
		update_something = {
			offset = 0x52,
			size = 1,
			_type = "int"
		},
		field_0x53 = {
			offset = 0x53,
			size = 1,
			_type = "int"
		},
		field_0x58 = {
			offset = 0x58,
			size = 4,
			_type = "float"
		},
		field_0x5c = {
			offset = 0x5c,
			size = 4,
			_type = "float"
		},
		field_0x68 = {
			offset = 0x68,
			size = 4,
			_type = "int"
		},
		field_0x6c = {
			offset = 0x6c,
			size = 4,
			_type = "int"
		},
		field_0x71 = {
			offset = 0x71,
			size = 1,
			_type = "int"
		},
		field_0x72 = {
			offset = 0x72,
			size = 1,
			_type = "int"
		},
		field_0x73 = {
			offset = 0x73,
			size = 1,
			_type = "int"
		},
		update_func_ptr_ptr = {
			offset = 0x74,
			size = 4,
			_type = "int"
		},
		pvars = {
			offset = 0x78,
			size = 4,
			_type = "int"
		},
		field_0x7c = {
			offset = 0x7c,
			size = 1,
			_type = "int"
		},
		field_0x7d = {
			offset = 0x7d,
			size = 1,
			_type = "int"
		},
		field_0x7e = {
			offset = 0x7e,
			size = 1,
			_type = "int"
		},
		anim_state = {
			offset = 0x7f,
			size = 1,
			_type = "int"
		},
		parent_maybe = {
			offset = 0x90,
			size = 4,
			_type = "int"
		},
		collision = {
			offset = 0x94,
			size = 4,
			_type = "int"
		},
		field_0xa0 = {
			offset = 0xa0,
			size = 1,
			_type = "int"
		},
		field_0xa1 = {
			offset = 0xa1,
			size = 1,
			_type = "int"
		},
		field_0xa2 = {
			offset = 0xa2,
			size = 1,
			_type = "int"
		},
		field_0xa3 = {
			offset = 0xa3,
			size = 1,
			_type = "int"
		},
		field_0xa4 = {
			offset = 0xa4,
			size = 1,
			_type = "int"
		},
		o_class = {
			offset = 0xa6,
			size = 2,
			_type = "int"
		},
		field_0xa8 = {
			offset = 0xa8,
			size = 4,
			_type = "int"
		},
		field_0xac = {
			offset = 0xac,
			size = 4,
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
	local max_search_space = game.moby_table_max - moby_table
	
	print("Searching mobies with ID " .. id .. " from addr " .. string.format("%x", moby_table))
	
	local mobies_address_space = read_large(moby_table, max_search_space)
	
	print("Done reading memory, looking for relevant mobies")
	
	local mobies = {}
	
	local current_addr = 0
	
	while true do
		local moby_id = bytestoint(get_ba_range(mobies_address_space, current_addr + 0xa6, 2)) -- ba({ table.unpack(mobies_address_space, current_addr + 1 + 0xa6, current_addr + 1 + 0xa6 + 2) }))
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

function Moby.static:set_pvars(moby, p_class)
	print("Setting pvars")
	local update_something = moby.update_something
	local field_0x50 = moby.field_0x50
	
	local seqs1 = p_class.seqs
	
	print("Seqs1: " .. seqs1 .. " UpdateID: " .. update_something .. " -- All: " .. (seqs1 + (update_something * 4)))
	
	seq = MobySeq(seqs1 + (update_something * 4))
	
	if update_something == 0xff then
		-- idk how we'd get here tbh
		moby.field_0x7e = 0
		moby.field_0x7c = 0xff
		print("Oh god oh fuck we got to the weird place wtf.")
	else
		frame_data_addr = seq.frame_data
		print("Frame data addr " .. frame_data_addr)
	
		local field_0x68_addr = frame_data_addr + (field_0x50 * 4)
		print("field_0x68 addr " .. field_0x68_addr)
		
		moby.field_0x68 = field_0x68_addr
		moby.field_0x7e = seq.trigs_cnt
		moby.field_0x7c = seq.sound
	end
	
	seq2 = MobySeq(p_class.seqs + (moby.field_0x53 * 4))
	moby.field_0x6c = seq2.frame_data + (moby.field_0x51 * 4)
end

function Moby.static:init_moby(moby, oClass)
	memset(moby.base_addr, 0, 0x100)
	
	print("Initing Moby " .. oClass .. " at address " .. moby.base_addr)
	
	-- mClass table at 0x0a354c0
	m_class = bytestoint(Ratchetron:ReadMemory(GAME_PID, 0x00a354c0 + oClass, 1))
	
	print("mClass: " .. m_class)
	
	moby_offset = moby.base_addr - game.moby_table_end
	
	-- Dude I have no idea what's going on with this
	local thingy = 0
	if ((moby_offset < 0) and ((moby_offset & 0xff) ~= 0)) then
		thingy = 1
	end
	
	local mode_bits = 0
	
	moby.o_class = oClass
	moby.m_class = m_class
	moby.alpha = 0x80
	moby.field_0xa4 = 0xff
	moby.group = 0xff
	moby.field_0x71 = 0xff
	moby.field_0x72 = 0xff
	moby.state_timer = 0x40404000
	moby.field_0x36 = 0x7f80
	moby.field_0x7e = 0
	moby.field_0x7c = 0xff
	moby.field_0xa8 = ((moby_offset >> 8) + thingy) * 0x10000
	moby.field_0x7d = 0xff
	moby.field_0xac = ((moby_offset >> 8) + thingy)
	moby.field_0xa0 = 0x7f
	moby.field_0xa1 = 0x7f
	moby.field_0xa2 = 0x80
	moby.field_0xa3 = 0x80
	
	update_func_ptr = 0x0a34f80 + (m_class * 4)
	
	print("Pointer to update func: " .. update_func_ptr)
	
	update_func = bytestoint(Ratchetron:ReadMemory(GAME_PID, update_func_ptr, 4))
	if update_func == 0 then
		print("Moby has no update func")
		mode_bits = 2
		moby.mode_bits = mode_bits  -- updateless moby
	else
		print("Moby has update func at " .. update_func)
		moby.update_func_ptr_ptr = update_func
	end
	
	moby_class_ptr = bytestoint(Ratchetron:ReadMemory(GAME_PID, 0x00a34c00 + (m_class * 4), 4))
	
	if moby_class_ptr == 0 then
		print("Classless moby")
		mode_bits = (mode_bits | 1 | 4)
		moby.mode_bits = moby_bits
	else
		print("Classy moby at addr " .. moby_class_ptr)
		moby.p_class = moby_class_ptr
		
		local p_class = MobyClass(moby_class_ptr)
		
		mode_bits = (mode_bits | p_class.mode_bits)
		moby.mode_bits = mode_bits
		moby.collision = p_class.collision
		moby.scale = p_class.g_scale
		moby.field_0x58 = 1.0
		moby.field_0x5c = 1.0
		
		local unk_ptr = p_class.unk_ptr
		if unk_ptr ~= 0 then
			mode_bits = (mode_bits | 0x10)
			moby.mode_bits = mode_bits
			moby.parent_maybe = unk_ptr
		end
		
		if p_class.shadow ~= 0 then
			mode_bits = (mode_bits | 0x400)
			moby.mode_bits = mode_bits
			moby.anim_state = 0x18
		end
		
		if p_class.seqs ~= 0 then
			Moby:set_pvars(moby, p_class)
		end
		
	end
	
	print("Done initing")
end

-- Spawn function written based on RaC1 decompilation project
function Moby.static:spawn(oClass)
	local moby_table_end_ptr = game.moby_table_end
	local moby_table_max_ptr = game.moby_table_max
	local game_time = game._time
	
	local moby_ptr = moby_table_end_ptr

	if moby_ptr < moby_table_max_ptr then
		while moby_ptr < moby_table_max_ptr do
			local moby = Moby(moby_ptr)
		
			if ((moby.state > 0xfd) and (moby.state_timer <= game_time)) then
				if moby.state == 0xff then
					-- Mark next Moby as free
					local next_moby = Moby(moby_table_end_ptr + 0x100)
					next_moby.state = 0xff
				end
				
				moby.auto_commit = false
				
				Moby:init_moby(moby, oClass)
				
				moby_offset = moby_ptr - moby_table_end_ptr
				
				-- Dude I have no idea what's going on with this
				local thingy = 0
				if ((moby_offset < 0) and (moby_offset & 0xff) ~= 0) then
					thingy = 1
				end
				
				unk_moby_stuff = game.unk_moby_vars_ptr + (((moby_offset >> 8) + thingy) * 0x80)
				memset(unk_moby_stuff, 0, 0x80)
				
				moby.field_0x78 = unk_moby_stuff
				
				print("Should return Moby with unk_moby_stuff at " .. unk_moby_stuff)
				
				if game.unk_moby_counter == 0 then
					return moby
				end
				
				game.unk_moby_counter = game.unk_moby_counter - 1
				return moby
			end
			
			moby_ptr = moby_ptr + 0x100
		end
	end
	
	return nil
end