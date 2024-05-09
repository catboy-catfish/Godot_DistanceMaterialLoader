//https://docs.godotengine.org/en/stable/tutorials/scripting/c_sharp/c_sharp_basics.html

using Godot;
using System;

/*
    Distance Material Loader
    created by gh:catboy-catfish, mit license
*/

[GlobalClass]
public partial class DistanceMaterialLoader : Node3D
{
	enum TextureQuality
    {
        _8192,
        _4096,
        _2048,
        _1024,
        _0512,
        _0256,
        _0128,
        _0064,
        _0032,
        _0016,
        _0008,
        _0004,
        _0002
    }
    
    [ExportGroup("setup")]
    [Export] Camera3D camera;
    [Export] MeshInstance3D meshInstance;
    [Export] int materialSlot;
    [Export] bool useGarbageCollection = true;

    [ExportGroup("parameters")]
    [ExportSubgroup("update")]
    [Export] float updateRate = 10f;
    [ExportSubgroup("quality")]
    [Export] TextureQuality maximumQuality = TextureQuality._2048;
    [Export] TextureQuality minimumQuality = TextureQuality._0256;
    [Export] float distanceMultiplier = 1;

    [ExportGroup("material paths")]
    [Export] string mp_0001;
    [Export] string mp_0002;
    [Export] string mp_0004;
    [Export] string mp_0008;
    [Export] string mp_0016;
    [Export] string mp_0032;
    [Export] string mp_0064;
    [Export] string mp_0128;
    [Export] string mp_0256;
    [Export] string mp_0512;
    [Export] string mp_1024;
    [Export] string mp_2048;
    [Export] string mp_4096;
    [Export] string mp_8192;

    float cameraMeshDistance;
    int qualityToLoad;

    public override void _Ready()
    {   
        loop();
    }

    void calculations()
    {
        cameraMeshDistance = (meshInstance.GlobalPosition - camera.GlobalPosition).Length() * distanceMultiplier;

        //Find a way to not use as much type casting?
        qualityToLoad = (int)Mathf.Min( Mathf.Max(cameraMeshDistance, (int)maximumQuality), (int)minimumQuality );
    }

    void collectGarbage()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
        GC.WaitForPendingFinalizers();
    }

    async void loop()
    {
        if(useGarbageCollection) collectGarbage();

        if(materialSlot > -1)
        {
            meshInstance.SetSurfaceOverrideMaterial( materialSlot, GD.Load<Material>( targetMaterial() ) );
        } else {
            //TODO: Set geometry material override, somehow
            //meshInstance.SetActiveMaterial( GD.Load<Material>( targetMaterial() ) );
        }

        await ToSignal( GetTree().CreateTimer( 1/updateRate ) , "timeout");

        calculations();
        
        loop();
    }

    String targetMaterial()
    {
        switch(qualityToLoad)
        {
            case 0:
                return mp_8192;
            case 1:
                return mp_4096;
            case 2:
                return mp_2048;
            case 3:
                return mp_1024;
            case 4:
                return mp_0512;
            case 5:
                return mp_0256;
            case 6:
                return mp_0128;
            case 7:
                return mp_0064;
            case 8:
                return mp_0032;
            case 9:
                return mp_0016;
            case 10:
                return mp_0008;
            case 11:
                return mp_0004;
            case 12:
                return mp_0002;
            default:
                return mp_0001;
        }
    }

    void updateMaterial()
    {

    }
}
