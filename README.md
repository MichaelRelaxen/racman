# racman
Practice tool for Ratchet & Clank 1 and Ratchet & Clank 3 for PS3 tailored for speed runners

[Download here.](https://github.com/MichaelRelaxen/racman/releases/tag/RaCMAN.v1.3.1.0) - Download the zip file

## Custom input display skins
Input display skins are all in the `controllerskins` folder. Skin names (as you see them in the skin selection drop down) are the names of the folders directly in the `controllerskins` folder.

### Making my own skin
Make a copy of any of the skin folders already in the `controllerskins` folder to get started. Replace the skin image in that folder with yours. If you want a different layout than the skin you
copied, you need to edit the `skin.txt` file in the folder. This is to adjust the sprite cutouts to fit your layout. Example `skin.txt` looks like this: 
```
# Add in numbers following the structure below.
# Name: drawX, drawY, spriteX, spriteY, spriteWidth, spriteHeight

# Base controller image
base: 0, 0, 0, 0, 800, 558

# Analog sticks
r3: 469, 328, 106, 627, 105, 105
r3Press: 469, 328, 0, 627, 105, 105
l3: 210, 328, 106, 627, 105, 105
l3Press: 210, 328, 0, 627, 105, 105

# Add pitch that should be used for the analog stick.
analogPitch: 32

# D-Pad buttons
dpadLeft: 74, 244, 0, 560, 52, 38
dpadRight: 162, 244, 130, 560, 52, 38
dpadDown: 124, 276, 53, 560, 38, 52
dpadUp: 124, 198, 92, 560, 38, 52

# Face buttons
cross: 609, 303, 389, 560, 62, 62
circle: 680, 232, 326, 560, 62, 62
triangle: 609, 161, 263, 560, 62, 62
square: 538, 232, 200, 560, 62, 62

# Pause buttons
select: 291, 252, 460, 561, 38, 20
start: 459, 252, 499, 561, 37, 20

# Shoulder buttons
r1: 596, 73, 458, 654, 89, 27
l1: 99, 73, 458, 654, 89, 27
l2: 99, 0, 460, 586, 86, 65
r2: 599, 0, 460, 586, 86, 65

# Add in name of the image you used. Needs to be in the same folder as the .txt
imageName: skin.png
```
`drawX` and `drawY`: X and Y positions to draw the sprite on screen  
`spriteX` and `spriteY`: X and Y positions for the top left corner where the sprite starts in the png image.  
`spriteWidth` and `spriteHeight`: Width and height in pixels of the sprite in your png image.  


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
