--[[
Customized by Alados5 (Jun'22)
Basically removed Batalia SI, ILJ/ISF ship to boss on Umbris, and a couple clips
--]]
require 'table_id'

items = {
    {id=0x02, name="Heli-pack",       req_items={} },
    {id=0x03, name="Thruster-pack",   req_items={} },
    {id=0x04, name="Hydro-pack",      req_items={{keyid.hydrodisplacer}, {keyid.heli_pack}, {keyid.thruster_pack}} },
    {id=0x05, name="Sonic Summoner",  req_items={{keyid.zoomerator}} },
    {id=0x06, name="O2 Mask",         req_items={{keyid.pilots_helmet}} },
    {id=0x07, name="Pilot's Helmet",  req_items={} },
    {id=0x09, name="Suck Cannon",     req_items={{keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} },
    {id=0x0B, name="Devastator",      req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x0C, name="Swingshot",       req_items={} },
    {id=0x0D, name="Visibomb",        req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x0E, name="Taunter",         req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x0F, name="Blaster",         req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x10, name="Pyrocitor",       req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x11, name="Mine Glove",      req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x12, name="Walloper",        req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x13, name="Tesla Claw",      req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x14, name="Glove of Doom",   req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x15, name="Morph-O-Ray",     req_items={{keyid.swingshot}, {keyid.pda}} },
    {id=0x16, name="Hydrodisplacer",  req_items={{keyid.trespasser}, {keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} },
    {id=0x17, name="R.Y.N.O.",        req_items={{keyid.metal_detector,keyid.heli_pack}, {keyid.metal_detector,keyid.thruster_pack}} },
    {id=0x18, name="Drone Device",    req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x19, name="Decoy Glove",     req_items={}, ill_items={0x30,0x31,0x32,0x34,0x35} },
    {id=0x1A, name="Trespasser",      req_items={{keyid.swingshot}, {keyid.heli_pack}, {keyid.thruster_pack}} },
    {id=0x1B, name="MetalDetector",   req_items={{keyid.magneboots}, {keyid.heli_pack}, {keyid.thruster_pack}} },
    {id=0x1C, name="Magneboots",      req_items={} },
    {id=0x1D, name="Grindboots",      req_items={{keyid.swingshot}, {keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} },
    {id=0x1E, name="Hoverboard",      req_items={} },
    {id=0x1F, name="Hologuise",       req_items={{keyid.hoverboard,keyid.heli_pack}, {keyid.hoverboard,keyid.visibomb}, {keyid.thruster_pack}, {keyid.pda}} },
    {id=0x20, name="PDA",             req_items={{keyid.magneboots}} },
    {id=0x21, name="Map-O-Matic",     req_items={{keyid.grindboots}, {keyid.pda,keyid.thruster_pack}} },
    {id=0x22, name="Bolt Grabber",    req_items={{keyid.hydro_pack,keyid.o2_mask}, {keyid.pda}} },
    {id=0x23, name="Persuader",       req_items={{keyid.raritanium,keyid.trespasser,keyid.hydrodisplacer}, {keyid.raritanium,keyid.o2_mask}} },
    {id=0x30, name="Zoomerator",      req_items={{keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} },
    {id=0x31, name="Raritanium",      req_items={{keyid.swingshot}, {keyid.heli_pack}, {keyid.thruster_pack}} },
    {id=0x32, name="Codebot",         req_items={} },
    {id=0x34, name="Premium Nanotech",req_items={{keyid.o2_mask,keyid.heli_pack}, {keyid.o2_mask,keyid.thruster_pack}, {keyid.o2_mask,keyid.pda}} },
    {id=0x35, name="Ultra Nanotech",  req_items={{keyid.premium_nanotech,keyid.o2_mask,keyid.heli_pack}, {keyid.premium_nanotech,keyid.o2_mask,keyid.thruster_pack}, {keyid.premium_nanotech,keyid.o2_mask,keyid.pda}} },
}

infobots = {
    {id=0x01, req_items={} }, -- Veldin1 -> Novalis
    {id=0x02, req_items={} }, -- Novalis -> Aridia
    {id=0x03, req_items={} }, -- Novalis -> Kerwan
    {id=0x04, req_items={{keyid.heli_pack}, {keyid.thruster_pack}} }, -- Kerwan -> Eudora
    {id=0x05, req_items={} }, -- Blarg -> Rilgar
    {id=0x06, req_items={{keyid.heli_pack}, {keyid.thruster_pack}, {keyid.decoy_glove}} }, -- Eudora -> Blarg
    {id=0x07, req_items={{keyid.heli_pack}, {keyid.thruster_pack}} }, -- Rilgar -> Umbris
    {id=0x08, req_items={{keyid.swingshot,keyid.heli_pack}, {keyid.swingshot,keyid.thruster_pack}} }, -- Umbris -> Batalia
    {id=0x09, req_items={{keyid.grindboots}, {keyid.heli_pack,keyid.pda}, {keyid.thruster_pack,keyid.pda}} }, -- Batalia -> Gaspar
    {id=0x0A, req_items={} }, -- Batalia -> Orxon
    {id=0x0B, req_items={} }, -- Orxon -> Pokitaru
    {id=0x0C, req_items={{keyid.o2_mask,keyid.heli_pack}, {keyid.o2_mask,keyid.thruster_pack}} }, -- Orxon -> Hoven
    {id=0x0D, req_items={} }, -- Hoven -> Gemlik
    {id=0x0E, req_items={{keyid.swingshot,keyid.magneboots,keyid.trespasser,keyid.devastator}, {keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} }, -- Gemlik -> Oltanis
    {id=0x0F, req_items={{keyid.grindboots}, {keyid.pda}} }, -- Oltanis -> Quartu
    {id=0x10, req_items={{keyid.swingshot}, {keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} }, -- Quartu -> KaleboIII
    {id=0x11, req_items={{keyid.heli_pack}, {keyid.thruster_pack}, {keyid.pda}} }, -- Quartu -> Fleet
    {id=0x12, req_items={{keyid.thruster_pack}, {keyid.magneboots}, {keyid.decoy_glove}} } -- Fleet -> Veldin2
}

planets = {
    {id=1,  name="Novalis",  infobots={keyid.aridia,keyid.kerwan},  items={keyid.pyrociter} },
    {id=2,  name="Aridia",   infobots={},                           items={keyid.hoverboard,keyid.trespasser,keyid.sonic_summoner} },
    {id=3,  name="Kerwan",   infobots={keyid.eudora},               items={keyid.swingshot,keyid.heli_pack,keyid.blaster} },
    {id=4,  name="Eudora",   infobots={keyid.blarg},                items={keyid.glove_of_doom,keyid.suck_cannon} },
    {id=5,  name="Rilgar",   infobots={keyid.umbris},               items={keyid.zoomerator,keyid.mine_glove,keyid.ryno} },
    {id=6,  name="Blarg",    infobots={keyid.rilgar},               items={keyid.grindboots,keyid.hydrodisplacer,keyid.taunter} },
    {id=7,  name="Umbris",   infobots={keyid.batalia},              items={} },
    {id=8,  name="Batalia",  infobots={keyid.gaspar,keyid.orxon},   items={keyid.metal_detector,keyid.devastator} },
    {id=9,  name="Gaspar",   infobots={},                           items={keyid.pilots_helmet,keyid.walloper} },
    {id=10, name="Orxon",    infobots={keyid.pokitaru,keyid.hoven}, items={keyid.premium_nanotech,keyid.ultra_nanotech,keyid.magneboots,keyid.visibomb} },
    {id=11, name="Pokitaru", infobots={},                           items={keyid.thruster_pack,keyid.o2_mask,keyid.persuader,keyid.decoy_glove} },
    {id=12, name="Hoven",    infobots={keyid.gemlik},               items={keyid.raritanium,keyid.hydro_pack,keyid.drone_device} },
    {id=13, name="Gemlik",   infobots={keyid.oltanis},              items={} },
    {id=14, name="Oltanis",  infobots={keyid.quartu},               items={keyid.pda,keyid.tesla_claw,keyid.morph_o_ray} },
    {id=15, name="Quartu",   infobots={keyid.kalebo,keyid.fleet},   items={keyid.bolt_grabber} },
    {id=16, name="KaleboIII",infobots={},                           items={keyid.hologuise,keyid.map_o_matic} },
    {id=17, name="Fleet",    infobots={keyid.veldin},               items={keyid.codebot} }
}