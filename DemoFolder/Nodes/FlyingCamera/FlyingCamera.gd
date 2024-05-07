extends CharacterBody3D

const SPEED = 4
const SENSITIVITY = 0.1

func _ready():
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED

func _input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		rotation_degrees.y -= event.relative.x * SENSITIVITY
		rotation_degrees.x -= event.relative.y * SENSITIVITY
		rotation_degrees.x = clamp(rotation_degrees.x, -90, 90)

func _process(_delta: float) -> void:
	var inputDir: Vector2 = Input.get_vector("A", "D", "W", "S")
	var direction: Vector3 = ((transform.basis.x * inputDir.x)+(transform.basis.z * inputDir.y)).normalized()*SPEED
	
	velocity = direction
	
	move_and_slide()
