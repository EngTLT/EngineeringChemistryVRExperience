using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Moves the ferry to the dock with the player on board
public class MoveFerry : MonoBehaviour {
	public GameObject camRig;
	private AudioSource engineSound;

	private float deltaSpeed = 0.00032f;
	private float deltaRot = -0.07f;

	// Use this for initialization
	void Start () {
		engineSound = GetComponent<AudioSource>();
		StartCoroutine("moveFerry");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator moveFerry() {
		float speed = 0.6f;
		while(speed > 0) {
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
}
