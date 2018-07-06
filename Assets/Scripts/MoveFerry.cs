using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves the ferry to the dock with the player on board
public class MoveFerry : MonoBehaviour {
	public GameObject camRig;
	private AudioSource engineSound;

	private float deltaSpeed = 0.00032f;
	private float deltaRot = -0.07f;

	private float scalingFactor = 0.99f;

	// Use this for initialization
	void Start() {
		if (Manager.manager.firstLoad) {
			engineSound = GetComponent<AudioSource>();
			StartCoroutine(moveFerry());
			Manager.manager.firstLoad = false;
		}
		else {
			camRig.transform.parent = null;
			Manager.manager.loadTransform(camRig);
			Destroy(GameObject.Find("removableHood"));
			StartCoroutine(Grow());
		}
	}

	private IEnumerator moveFerry() {
		float speed = 0.6f;
		while (speed > 0) {
			transform.Translate(new Vector3(speed, 0, 0));
			speed -= deltaSpeed;
			if (transform.rotation.eulerAngles.y < 45f || transform.rotation.eulerAngles.y > 225f) //this will stop the ferry rotating once it is perpendicular to the dock
				transform.Rotate(Vector3.up, deltaRot);
			else
				engineSound.volume -= 0.0002f;

			if (camRig.transform.position.z > 1000 && camRig.transform.position.x > 660) //if player lands on dock before ferry stops, it will stop following the ferry
				camRig.transform.parent = null;

			yield return new WaitForEndOfFrame();
		}
		engineSound.Stop();
		camRig.transform.parent = null;
	}

	private IEnumerator Grow() {
		Vector3 finalPosition = new Vector3(802.39f, 45.5f, 1110.04f);
		float t = 0;

		yield return new WaitForSeconds(2f);
		while (camRig.transform.lossyScale.x < 2.6) {
			Vector3 currentScale = camRig.transform.localScale;

			camRig.transform.localScale = new Vector3(currentScale.x / scalingFactor, currentScale.y / scalingFactor, currentScale.z / scalingFactor);

			camRig.transform.position = Vector3.Lerp(Manager.manager.playerPosition, finalPosition, t) + new Vector3(0, 2 * (1f - Mathf.Pow(t - 1f, 2)), 0);
			t += 0.002f;
			yield return new WaitForEndOfFrame();
		}

		Manager.manager.playerPosition = camRig.transform.position;
		t = 0;
		while (t < 1) {//finish moving player
			camRig.transform.position = Vector3.Lerp(Manager.manager.playerPosition, finalPosition, t);
			t += 0.004f;
			yield return new WaitForEndOfFrame();
		}
		Destroy(this.gameObject); //TODO: instead of destroy, set at final position?
	}
}
