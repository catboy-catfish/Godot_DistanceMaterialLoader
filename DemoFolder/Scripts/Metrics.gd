#https://docs.godotengine.org/en/latest/classes/class_performance.html

extends Node3D
@onready var fps: Label = $Control/FPS
@onready var textureMem: Label = $Control/TextureMem

func _process(delta: float) -> void:
	fps.text = "Frames/sec: " + str(Performance.get_monitor(Performance.TIME_FPS))
	textureMem.text = "Texture memory: " + str(Performance.get_monitor(Performance.RENDER_TEXTURE_MEM_USED)) + " bytes"
