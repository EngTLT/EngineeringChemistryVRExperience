using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialSpectrometer : MonoBehaviour {
	private bool enable, manipulated;
	SteamVR_TrackedController controller;

	private float controllerAngle = 0, prevControllerAngle = 0;

	// Use this for initialization
	void Start () {
		enable = false;
		manipulated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (manipulated && controller != null) {
			controllerAngle = controller.gameObject.transform.rotation.eulerAngles.y;
			float diffAngle = controllerAngle - prevControllerAngle;
			transform.Rotate(transform.forward, diffAngle); //for this model, the z axis is actually up
			prevControllerAngle = controllerAngle;
		}
	}

	void OnTriggerEnter(Collider collider) {
		controller = collider.GetComponent<SteamVR_TrackedController>();
	}

	void OnTriggerExit(Collider collider) {
		controller = null;
	}

	void OnTriggerStay(Collider collider) {
		if (enable && controller.triggerPressed) {
			manipulated = true;
		}
		else manipulated = false;
	}

	public void EnableDial() {
		enable = true;
	}
}
