using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {
	public float speed = 6.0f;
	public float gravity = 1.0f;
	public float jumpHeight = 20.0f;
	public int jumpStep = 3;
	private Queue<float> jumpSteps;
	private CharacterController _characterController;
	// Use this for initialization
	void Start () {
		_characterController = GetComponent<CharacterController>();
		jumpSteps = new Queue<float>();
	}

	Queue<float> createJumpCurve(int steps,float height){
		Queue<float> q = new Queue<float>();
		for (int i = 0; i < steps; i++)
		{
			q.Enqueue(height/steps);
		}
		return q;

	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX,0,deltaZ);
		movement = Vector3.ClampMagnitude(movement,speed);
		movement.y = gravity;
		movement *= Time.deltaTime;
		if (Input.GetKey(KeyCode.Space) && _characterController.isGrounded && jumpSteps.Count  == 0) {
			jumpSteps = createJumpCurve(jumpStep,jumpHeight);
        }
		if(jumpSteps.Count  > 0){
			movement.y = jumpSteps.Dequeue();
		}
		movement = transform.TransformDirection(movement);
		_characterController.Move(movement);
	}
}
