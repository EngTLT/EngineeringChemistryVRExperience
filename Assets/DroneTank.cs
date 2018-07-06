using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTank : MonoBehaviour {
	bool held;
	SteamVR_TrackedController holder;
	int count = 0;

	Rigidbody tankRB;
	void Start() {
		held = false;
		tankRB = GetComponent<Rigidbody>();
	}

	void OnTriggerStay(Collider collider) {
		if (holder == null)
			holder = collider.gameObject.GetComponent<SteamVR_TrackedController>();

		if (!held && holder.triggerPressed) {
			transform.parent = collider.transform;
			held = true;			
		}else if(held && !holder.triggerPressed) {
			count++;
			if (count > 15) {
				transform.parent = null;
				holder = null;
				GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}
}
