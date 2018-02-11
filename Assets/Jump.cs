using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    public float jumpHeight = 20.0f;
    private CharacterController _characterController;

	private float distToGround;

    // Use this for initialization
    void Start() {
        _characterController = GetComponent<CharacterController>();
		distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
			_characterController.SimpleMove(new Vector3(0,jumpHeight,0));
        }
    }

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
