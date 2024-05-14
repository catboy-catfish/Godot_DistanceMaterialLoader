#https://docs.godotengine.org/en/latest/classes/class_performance.html

extends Node3D
@onready var fps: Label = %FPS
@onready var miscVRAM: Label = %MiscVRAM
@onready var vertexMem: Label = %VertexMem
@onready var textureMem: Label = %TextureMem

func _process(delta: float) -> void:
	var framesF := float(Performance.get_monitor(Performance.TIME_FPS))
	var videoF := float(Performance.get_monitor(Performance.RENDER_VIDEO_MEM_USED)) * 0.000001
	var vertF := float(Performance.get_monitor(Performance.RENDER_BUFFER_MEM_USED)) * 0.000001
	var textF := float(Performance.get_monitor(Performance.RENDER_TEXTURE_MEM_USED)) * 0.000001
	
	fps.text = "Frames/sec: " + str(framesF)
	miscVRAM.text = "Misc. VRAM: " + str(videoF - vertF - textF) + " mb"
	vertexMem.text = "Vertex memory: " + str(vertF) + " mb"
	textureMem.text = "Texture memory: " + str(textF) + " mb"
