//https://github.com/godotengine/godot/issues/82112
//https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_basics.html
//https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_differences.html
//https://docs.godotengine.org/en/stable/classes/class_resourceloader.html#class-resourceloader-method-has-cached
//https://learn.microsoft.com/en-us/dotnet/api/system.gc.collect?view=net-8.0
//https://learn.microsoft.com/en-us/dotnet/api/system.gccollectionmode?view=net-8.0
//https://learn.microsoft.com/en-us/dotnet/api/system.gc.waitforpendingfinalizers?view=net-8.0
//https://www.reddit.com/r/godot/comments/1888ige/scene_unloading_not_really_unloads_anything_ram/
//https://www.youtube.com/watch?v=S3QPvd2Eq4w

using Godot;
using System;

/*
    Distance Material Loader
    created by gh:catboy-catfish, mit license
    thanks for the help: xill47, spookyspice
*/

[GlobalClass]
public partial class DistanceMaterialLoader : Node3D
{
	enum LOD{
        lod_8192,
        lod_4096,
        lod_2048,
        lod_1024,
        lod_0512,
        lod_0256,
        lod_0128,
        lod_0064,
        lod_0032,
        lod_0016,
        lod_0008,
        lod_0004,
        lod_0002,
        lod_0001
    }
    
    [ExportGroup("setup")]
    [Export] bool enabled = true;
    [Export] Camera3D camera;
    [Export] MeshInstance3D meshInstance;
    [Export] int materialSlot;

    [ExportGroup("parameters")]
    [ExportSubgroup("distance")]
    [Export] float distanceSensitivity = 1;
    [ExportSubgroup("quality")]
    [Export] LOD maximumLOD = LOD.lod_4096;
    [Export] LOD minimumLOD = LOD.lod_0001;
    [ExportSubgroup("update")]
    [Export] float updateInterval = 0.1f;

    [ExportGroup("material paths")]
    [Export] String path_0001;
    [Export] String path_0002;
    [Export] String path_0004;
    [Export] String path_0008;
    [Export] String path_0016;
    [Export] String path_0032;
    [Export] String path_0064;
    [Export] String path_0128;
    [Export] String path_0256;
    [Export] String path_0512;
    [Export] String path_1024;
    [Export] String path_2048;
    [Export] String path_4096;
    [Export] String path_8192;

    double timeRemaining;
    Material currentMaterial;
    bool canUpdate;
    float cameraMeshDistance;
    int lodToLoad;

    public override void _Ready(){
        timeRemaining = updateInterval;
    }

    void calculations(){
        int iMaxLOD = (int)maximumLOD;
        int iMinLOD = (int)minimumLOD;
        
        cameraMeshDistance = (meshInstance.GlobalPosition - camera.GlobalPosition).Length() * distanceSensitivity;
        lodToLoad = (int)Mathf.Clamp(cameraMeshDistance, iMaxLOD, iMinLOD);
    }

    void collectGarbage()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
    }

    public override void _PhysicsProcess(double delta){
        
        if(enabled){
            if(updateInterval >= delta){
                timeRemaining -= delta;

                if(timeRemaining <= 0){
                    timeRemaining += Mathf.Abs(updateInterval);
                    update();
                }
            } else {
                update();
            }
        }
    }

    String targetMaterial(){
        switch(lodToLoad){
            case 0:
                return path_8192;
            case 1:
                return path_4096;
            case 2:
                return path_2048;
            case 3:
                return path_1024;
            case 4:
                return path_0512;
            case 5:
                return path_0256;
            case 6:
                return path_0128;
            case 7:
                return path_0064;
            case 8:
                return path_0032;
            case 9:
                return path_0016;
            case 10:
                return path_0008;
            case 11:
                return path_0004;
            case 12:
                return path_0002;
            default:
                return path_0001;
        }
    }

    void update(){
        if(Visible){
            currentMaterial = null;
            calculations();
            collectGarbage(); //We're doing this manually because Godot doesn't want to do it for us.
            currentMaterial = ResourceLoader.Load<Material>(targetMaterial());
            meshInstance.SetSurfaceOverrideMaterial(materialSlot, currentMaterial);
        }
    }
}
