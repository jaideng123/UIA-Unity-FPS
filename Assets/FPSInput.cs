using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {
    public float speed = 6.0f;
    public float gravity = 1.0f;
    public float jumpHeight = 20.0f;
	public float MaxjumpHeight = 30.0f;
    public int jumpStep = 3;
    private float jumpLeft;
    private CharacterController _characterController;
    // Use this for initialization
    void Start() {
        _characterController = GetComponent<CharacterController>();
        jumpLeft = 0;
    }

    Queue<float> createJumpCurve(int steps, float height) {
        Queue<float> q = new Queue<float>();
        for (int i = 0; i < steps; i++) {
            q.Enqueue(height / steps);
        }
        return q;

    }

    // Update is called once per frame
    void Update() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && _characterController.isGrounded && jumpLeft == 0) {
            jumpLeft = jumpHeight;
            StartCoroutine(SpreadJump());
        }
        movement = transform.TransformDirection(movement);
        _characterController.Move(movement);
    }

    private IEnumerator SpreadJump() {
        while (jumpLeft > 0) {
            float accelValue = Mathf.Min(jumpLeft, jumpStep * Time.deltaTime);
            jumpLeft -= accelValue;
            Vector3 movement = new Vector3(0, 0, 0);
            movement.y = accelValue;
            movement = transform.TransformDirection(movement);
            _characterController.Move(movement);
            yield return null;
        }
    }
}
