using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves the ferry to the dock with the player on board
public class MoveFerry : MonoBehaviour
{
	public GameObject camRig;
	private AudioSource engineSound;

	public float deltaSpeed = 0.00032f;
	public float deltaRot = -0.07f;

    public float speed = 0.6f;

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
			camRig.transform.position = new Vector3(755f, 40.2f , 1100f);
			camRig.transform.rotation = Quaternion.Euler(0, 46f, 0);
		}
	}

	private IEnumerator moveFerry() {
		while (speed > 0) {
			transform.Translate(new Vector3(speed, 0, 0));
			speed -= deltaSpeed;
			if (transform.rotation.eulerAngles.y < 45f || transform.rotation.eulerAngles.y > 225f) //this will stop the ferry rotating once it is perpendicular to the dock
				transform.Rotate(Vector3.up, deltaRot);
			else
				engineSound.volume -= 0.0002f;

			if (camRig.transform.position.z > 1050 && camRig.transform.position.x > 700) //if player lands on dock before ferry stops, it will stop following the ferry
				camRig.transform.parent = null;

			yield return new WaitForEndOfFrame();
		}
		engineSound.Stop();
		camRig.transform.parent = null;
	}
}