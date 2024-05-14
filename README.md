# Godot: Distance Material Loader

This is a C# script for Godot that switches between different materials based on the distance between the camera and the material's node, all the while allocating/freeing the materials to/from memory depending on whether they are needed or not. This simulates texture streaming to an extent, a feature which (as of May 5th 2024) hasn't been implemented into Godot yet.



## Features:

- Switches between ~13 different materials, intended for materials with textures ranging from 2x2 to 4k in resolution.

- Configurable upper and lower boundaries placed upon the materials that should be selected.

- Automatically allocates memory for the materials and their respective textures depending on whether or not they are needed.

- Garbage collection to free the materials from memory once we are done with them.

- Everything takes place within a slower (custom) update loop as to not stress the computer too much.

- Scene included to demonstrate the effectiveness and purpose of this script.
  
  

## Limitations:

- Doesn't seem to be able to deallocate 8K .dds textures from VRAM, resulting in a large memory leak in Performance.RENDER_VIDEO_MEM_USED. I've fixed this before but I have no clue how, now it's broken again. Not sure if this is an issue with the script or with Godot 4.3.dev6. To get around this I've changed the default MaximumLOD to 4K instead of 8K.

- The materials aren't directly configurable as an export fields; instead the paths to them are exported as strings. Pasting all of them repeatedly is tedious, and it doesn't sync with name-changes/moves/deletions. Storing the materials in export fields would keep all of them loaded in memory at all times, defeating the purpose of this script.

- All of the respective mipmaps must be split into their own image files, and their own materials. Managing them one by one is tedious if, for example, all of them have been created beforehand.

- Doesn't support setting the material via the Geometry tab yet, only the Surface Material Override.

- Only one material can be set per DistanceMaterialLoader node.

- No support for built-in materials; all of the materials must be on the disk.

- Cannot fetch the camera from PackedScenes, it must be in the scene tree.
  
  

## Parameters:

- `setup/enabled`: Whether or not DistanceMaterialLoader should even run.

- 

- `setup/camera`: The camera that DistanceMaterialLoader will use to calculate the distance between the mesh and the camera.

- `setup/meshInstance`: The MeshInstance3D that DistanceMaterialLoader will use to calculate the distance between the camera and the mesh.

- `setup/materialSlot`: The Surface Material Override slot that DistanceMaterialLoader will replace. Any value lower than 0 is planned to set the Geometry material override instead, but this hasn't been implemented yet.

- `parameters/distance/distanceSensitivity`: How much DistanceMaterialLoader reacts to different distances. Higher values mean that higher LODs only appear when the camera is closer to the mesh. Lower values mean that higher LODs are visible from farther away.

- `parameters/quality/maximumLOD`: The maximum material LOD that DistanceMaterialLoader will load. Ranges from _8192 (value: 0) to _1 (value: 13).

- `parameters/quality/minimumQuality`: The minimum material LOD that DistanceMaterialLoader will load. Ranges from 0 (8192x8192) to 13 (1 pixel).

- `parameters/update/updateInterval`: The interval, in seconds, before every time DistanceMaterialLoader updates the materials, calculations and other variables.

- `material paths/mp0001` to `material paths/mp8192`: The paths to the materials. Increases in resolution by powers of 2. Only the paths to the materials are held in exported strings, not the materials themselves. If they were then all of the materials would be loaded at all times, making this script useless. The materials are instead loaded manually.
  
  

## Notes:

- The material paths are designated `mp0001` to `mp8192` because they are designed to be used with textures 1x1 to 8192x8192 in size, but it isn't strictly necessary to use this convention. It *is* possible to go beyond these limitations, such as using textures bigger than 8192x8192, or different aspect ratios.

## Goals:

- The distance between the camera and mesh is strictly linear. Add options to customize the behaviour, like to offset it or to make it more asymptotic and/or exponential.

- Add a way to further customize when the materials are updated (like in _Process() or _PhysicsProcess()) instead of just the custom update loop.

- Perhaps reduce the amount of type-casting in the script, if that is necessary and/or doable.
  
  

This is a very hacky way to implement texture streaming, and it doesn't help that this is my first attempt. If there's any way that this script could be optimized/improved upon, feel free to let me know. In any case, I hope it helps you. gh:catboy-catfish.
