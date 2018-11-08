using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveFarmScene : MonoBehaviour {
	public GameObject tip, otherHand;

	SteamVR_TrackedController left, right;
	// Use this for initialization
	void Start () {
		//StartCoroutine(Leave());
		right = GetComponent<SteamVR_TrackedController>();
		left = otherHand.GetComponent<SteamVR_TrackedController>();
	}

	void Update() {
		if (tip.activeInHierarchy) {
			//if (left.triggerPressed && right.triggerPressed)
				//SceneManager.LoadScene("WolfeIslandLanding");
		}
	}

	IEnumerator Leave() {
		yield return new WaitForSeconds(20f);
		tip.SetActive(true);
	}
}
