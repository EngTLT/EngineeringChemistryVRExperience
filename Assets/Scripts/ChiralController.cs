using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiralController : MonoBehaviour {
	public SteamVR_LaserPointer laser;
	public SteamVR_TrackedController controller;

	private GameObject heldObject;

	// Use this for initialization
	void Start () {
		laser = GetComponent<SteamVR_LaserPointer>();
		controller = GetComponent<SteamVR_TrackedController>();

		controller.TriggerClicked += LaserGrab;
		controller.TriggerUnclicked += LaserRelease;

	}
	
	// Update is called once per frame
	void Update () {
		if (controller.padPressed)
			Rotate();
	}

	void LaserGrab(object sender, ClickedEventArgs e) {
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit , 40f) && hit.collider.tag == "Metolachlor") {
			heldObject = hit.collider.gameObject;
			heldObject.transform.parent = transform;
		}
	}

	void LaserRelease(object sender, ClickedEventArgs e) {
		heldObject.transform.parent = null;
		heldObject = null;
	}

	void Rotate() {
		if (heldObject != null) {
			Vector2 click_dir = SteamVR_Controller.Input((int)controller.controllerIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
			Debug.Log(click_dir.x);
			if (click_dir.x > 0.4)
				heldObject.transform.Rotate(Vector3.up, -0.5f);
			else if (click_dir.x < -0.4)
				heldObject.transform.Rotate(Vector3.up, 0.5f);
		}
	}
}
