using UnityEngine;
using System.Collections;

public class PlayerShipMovement : MonoBehaviour 
{

	public float GetVelocityMagnitude { get { return _rigidbody.velocity.magnitude; }}

	Vector2 _stickInput;
	Vector3 _moveVector = Vector3.zero;

	private const float MOVE_SPEED = 200.0f;
	private const float VELOCITY_ROTATE_SPEED = 2.0f;
	private const float ROTATION_SPEED = 8.0f;

	private const float JOYSTICK_DEADZONE = 0.1f;

	private Vector3 YOFFSET_RAYCASTVEC = new Vector3(0.15f, 1.0f, 0.0f);
	private const float YOFFSET_DISTANCE = 1.0f;
	private const float YOFFSET_SPEED = 1.0f;

	private Rigidbody _rigidbody;
	public Rigidbody PlayerRB
	{
		get { return _rigidbody; }
	}

	void Awake()
	{
		_stickInput = Vector2.zero;
		_rigidbody = GetComponent<Rigidbody>();

	}

	void Start()
	{
	}

	void Update()
	{

		_stickInput = Camera.main.transform.TransformDirection(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
		HandleMovement(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));

		HandleRotation(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));


		// Brings the Tot Home if they fall off ; )
		if(transform.position.y <  -25.0f)
		{
			transform.position = new Vector3(5.0f, 25.0f, 5.0f);
			_rigidbody.velocity = Vector3.zero;
		}
			
	}

	void HandleMovement(Vector2 input)
	{
		if (input.sqrMagnitude < JOYSTICK_DEADZONE)
		{
			return;
		}
		else
		{
			_moveVector = new Vector3(input.x, 0.0f, input.y);
			_moveVector.Normalize();
			
			_moveVector = Camera.main.transform.TransformDirection(_moveVector);

			_moveVector *= MOVE_SPEED;
			_moveVector.y = _rigidbody.velocity.y;

			//_rigidbody.velocity.Normalize();
			_rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, _moveVector, Time.deltaTime * VELOCITY_ROTATE_SPEED);
		}

	}
	
	void HandleRotation(Vector2 input)
	{
		if (input.sqrMagnitude < JOYSTICK_DEADZONE)
		{
			return;
		}
		
		Vector3 inputVec = new Vector3(input.x, 0f, input.y);
		_moveVector.Normalize();
		inputVec = Camera.main.transform.TransformDirection(inputVec);
		
		var angle = Mathf.Atan2(-inputVec.z, inputVec.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angle, 0), ROTATION_SPEED * Time.deltaTime);
	}

	public void HandleBlowerMovement(Vector2 blowerDirection)
	{
		HandleMovement(blowerDirection);
	}
		
		
	private void DisplacePlayerY()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, YOFFSET_RAYCASTVEC.normalized, out hit, YOFFSET_DISTANCE))
		{
			Vector3 newPos = new Vector3(transform.position.x, hit.point.y + YOFFSET_DISTANCE, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, newPos, YOFFSET_SPEED * Time.deltaTime);
		}
	}
}
 