# Godot: Distance Material Loader

This is a C# script for Godot that switches between different materials based on the distance between the camera and the material's node, all the while allocating/freeing the materials to/from memory depending on whether they are needed or not. This simulates texture streaming to an extent, a feature which (as of May 5th 2024) hasn't been implemented into Godot yet.



## Features:

- Switches between ~13 different materials, intended for materials with textures ranging from 2x2 to 8192x8192 in resolution, but it's possible to go above that.

- Configurable upper and lower boundaries placed upon the materials that should be selected.

- Automatically allocates memory for the materials and their respective textures depending on whether or not they are needed.

- Garbage collection to actually free the materials from memory once we are done with them.

- Scene included to demonstrate the effectiveness and purpose of this script.
  
  

## Limitations:

- The materials aren't directly configurable as an export fields; instead the paths to them are exported as strings. Pasting all of them repeatedly is tedious, and it doesn't sync with name-changes/moves/deletions. Storing the materials in export fields would keep all of them loaded in memory at all times, defeating the purpose of this script.

- All of the respective mipmaps must be split into their own image files, and their own materials. Managing them one by one is tedious if, for example, all of them have been created beforehand.

- The 'quality' parameters are counter-intuitive. You would think that using a higher number would mean higher quality, but in this case the lower the number, the higher the quality. Think of it as "the distance between the camera and the node".

- Doesn't support setting the material via the Geometry tab yet, only the Surface Material Override.

- Only one material can be set per DistanceMaterialLoader node.

- The script doesn't have any tooltips/documentation right now.
  
  

This is a very hacky way to implement texture streaming, and it doesn't help that this is my first attempt. If there's any way that this script could be optimized/improved upon, feel free to let me know. In any case, I hope it helps you. gh:catboy-catfish.




















































