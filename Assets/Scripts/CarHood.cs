using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This script controls the behaviour of the car hood on the electric vehicle the user interacts with (i.e. lift it and throw it away)
public class CarHood : MonoBehaviour {
	public Valve.VR.InteractionSystem.Hand rHand, lHand; //place the hand component in here to allow interaction with controllers
	public GameObject rController, lController; //these will become parents of the car hood when player picks it up
	public GameObject player;

	bool isMoved, triggerDown; //bool to see if player is holding the object
	int count; //this will keep track of how long the trigger has been let go, for more accurate button press reads

	float scalingFactor = 0.99f; //what rate the player shrinks at

	// Use this for initialization
	void Start () {
		isMoved = false;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider collider) {
		triggerDown = false;
		if (collider.tag == "rController") {
			if (rHand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
				transform.parent = rController.transform;
				isMoved = true;
				count = 0;
			}
		} else if (collider.tag == "lController") {
			if (lHand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger)) {
				transform.parent = lController.transform;
				isMoved = true;
				count = 0;
			}
		} else if (isMoved && count>10) {
			transform.parent = null;
			Rigidbody rigid = GetComponent<Rigidbody>(); 
			rigid.isKinematic = false;

		} else {
			count++;
		}
	}

	void OnTriggerExit(Collider collider) {
		if (isMoved) {
			GetComponent<MeshCollider>().isTrigger = false;
			StartCoroutine("Shrink");
		}

	}

	private IEnumerator Shrink() {
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Fuel Cell");
		asyncLoad.allowSceneActivation = false;

		Vector3 finalPosition = new Vector3(803.79f, 47.6f, 1108.12f);
		float t = 0;
		while (player.transform.lossyScale.x > 0.15) {
			Vector3 currentScale = player.transform.localScale;

			player.transform.localScale = new Vector3(currentScale.x * scalingFactor, currentScale.y * scalingFactor, currentScale.z * scalingFactor);

			player.transform.position = Vector3.Lerp(player.transform.position, finalPosition, t);
			t += 0.000435f;
			yield return new WaitForEndOfFrame();
		}
		player.transform.position = Vector3.Lerp(player.transform.position, finalPosition, 1f);


		Manager.manager.saveTransform(player); //save players position for when level is reloaded
		asyncLoad.allowSceneActivation = true;
	}
	
}
