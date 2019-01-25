using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public Transform center;
	public float rotationSpeed = 40.0f;
	float radius;

	public float FOVChange = 5;

	Camera cam;


	void Start() {
		cam = GetComponent<Camera>();

		radius = (transform.position - center.position).sqrMagnitude;
	}

	void Update() {
		float wheelAxis = Input.GetAxis("Mouse ScrollWheel");
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		ChangeFOV(wheelAxis);
		RotateCameraOrbit(h, v);
	}

	void ChangeFOV(float wheelAxis) {
		if (wheelAxis > 0) // forward
			cam.fieldOfView -= FOVChange;
		else if (wheelAxis < 0) // backwards
			cam.fieldOfView += FOVChange;
	}

	void RotateCameraOrbit(float h, float v) {
		if (h != 0 || v != 0) {
			h = -h;
			transform.RotateAround(
				center.position,
				transform.TransformPoint(new Vector3(v, 0, 0)) - transform.position + new Vector3(0, h, 0),
				rotationSpeed * Time.deltaTime
			);
		}
	}
}
