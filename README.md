# Godot: Distance Material Loader

This is a C# script for Godot that switches between different materials based on the distance between the camera and the material's node, all the while allocating/freeing the materials to/from memory depending on whether they are needed or not. This simulates texture streaming to an extent, a feature which (as of May 5th 2024) hasn't been implemented into Godot yet.



## Features:

- Switches between ~13 different materials, intended for materials with textures ranging from 2x2 to 8192x8192 in resolution.

- Configurable upper and lower boundaries placed upon the materials that should be selected.

- Automatically allocates memory for the materials and their respective textures depending on whether or not they are needed.

- Garbage collection to free the materials from memory once we are done with them.

- Everything takes place within a slower (custom) update loop as to not stress the computer too much.

- Scene included to demonstrate the effectiveness and purpose of this script.
  
  

## Limitations:

- The materials aren't directly configurable as an export fields; instead the paths to them are exported as strings. Pasting all of them repeatedly is tedious, and it doesn't sync with name-changes/moves/deletions. Storing the materials in export fields would keep all of them loaded in memory at all times, defeating the purpose of this script.

- All of the respective mipmaps must be split into their own image files, and their own materials. Managing them one by one is tedious if, for example, all of them have been created beforehand.

- The 'quality' parameters are counter-intuitive. You would think that using a higher number would mean higher quality, but in this case the lower the number, the higher the quality. Think of it as "the distance between the camera and the mesh".

- Doesn't support setting the material via the Geometry tab yet, only the Surface Material Override.

- Only one material can be set per DistanceMaterialLoader node.

- No support for built-in materials; all of the materials must be on the disk.

- Cannot fetch the camera from PackedScenes, it must be in the scene tree.



## Parameters:

- `setup/camera`: The camera that DistanceMaterialLoader will use to calculate the distance between the mesh and the camera.

- `setup/meshInstance`: The MeshInstance3D that DistanceMaterialLoader will use to calculate the distance between the camera and the mesh.

- `setup/materialSlot`: The Surface Material Override slot that DistanceMaterialLoader will replace. This only affects Surface Material Overrides right now, not Geometry materials.

- `setup/useGarbageCollection`: Whether or not to first use garbage collection on the update loop. I am unsure about what this actually does, but I added it in for safety.

- `parameters/update/updateRate`: How often the materials and calculations will update per second.

- `parameters/quality/maximumQuality`: The maximum texture quality that DistanceMaterialLoader will load, lower means higher quality. Ranges from 0 (8192x8192) to 13 (1 pixel).

- `parameters/quality/minimumQuality`: The minimum texture quality that DistanceMaterialLoader will load, higher means lower quality. Ranges from 0 (8192x8192) to 13 (1 pixel).

- `parameters/quality/distanceMultiplier`: The multiplier for the distance between the camera and the MeshInstance3D. Like maximumQuality and minimumQuality, this actually goes the opposite way; lower means that higher quality materials will be visible from farther away. Adjust to taste.

- `material paths/mp0001` to `material paths/mp8192`: The paths to the materials. Increases in resolution by powers of 2. Only the paths to the materials are held in exported strings, not the materials themselves. If they were then all of the materials would be loaded at all times, making this script useless. The materials are loaded manually.



## Notes:

- The material paths are designated `mp0001` to `mp8192` because they are designed to be used with textures 1x1 to 8192x8192 in size, but it isn't strictly necessary to use this convention. It *is* possible to go beyond these limitations, such as using textures bigger than 8192x8192, or different aspect ratios.



This is a very hacky way to implement texture streaming, and it doesn't help that this is my first attempt. If there's any way that this script could be optimized/improved upon, feel free to let me know. In any case, I hope it helps you. gh:catboy-catfish.


















































