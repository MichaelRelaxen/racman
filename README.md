# racman
Practice tool for Ratchet & Clank 1 and Ratchet & Clank 3 for PS3 tailored for speed runners

[Download here.](https://github.com/MichaelRelaxen/racman/releases/tag/RaCMAN.v1.3.1.0) - Download the zip file

## Game patches
Game patches are loaded while the game is running. They can be simple patches that mildy change the code flow or advanced patches with multiple blobs of code.
Patches for each supported game can be found on disk under `mods/<GAME TITLE ID>/`. A patch in the game patch folder is only loadable and visible in the patch loader
window if has `patch.txt` in it. Patch files have directives for meta data as well as patch data and references. 

Game patches can be loaded and unloaded on-demand while the game is running. RaCMAN does this by storing what's currently in the patch addresses when you open the
patch loader window. When you deactivate a patch, RaCMAN restores the original address values. 

### Metadata
Metadata tags start with `#-` and are key value seratated by `:` and may look like this:

```
#- name: My Cool Mod
```

#### Metadata tags

`name`: Name of the patch, which shows up as the patch name in the GUI.  
`author`: Name of the person/team who made it  
`version`: Version of your mod/patch.  
`description`: Description of your thing.  
`href`: Link to project page or author page.  
`unloadable`: Set to `false` if you don't want the user to be able to unload the patch without restarting the game. On by default.   

### Patches
Patches are hex addresses and either references to blobs or patch values. Example under: 
```
0x97C7E8: 0x489804CB
0x981000: input.bin
```
First patch is at address `0x97C7E8` with patch value `0x489804CB`. This means that the value `0x489804CB` will replace the current content of what's at address `0x97C7E8`.
The second patch puts all of the content of the file `input.bin` into memory starting at the specified address. 

### Comments
To add a comment to `patch.txt` a new line must start with `#` and not be directly followed by a `-` (that would make it a metadata tag). Comments are not parsed, they are
just wild thoughts you can add to the configuration file to remind your future self (or others) what the fuck you where thinking. 
