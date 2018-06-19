using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronBehaviour : MonoBehaviour {
	public GameObject electricity;
	static bool electricityOn = false; //has the electricity animation started?
	Vector3 finalPosition;

	Vector3 wire1Start = new Vector3(1.618f, -0.75f, 2.632f); //this is where the copperwire begins, electron must move here first
	Vector3 wire1End = new Vector3(1.618f, -0.75f, 6.61f);
	Vector3 wire2Start = new Vector3(-1.65f, -0.75f, 2.632f);
	// Use this for initialization
	void Start () {
		StartCoroutine(MoveElectron());
		finalPosition = new Vector3(-1.65f, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator MoveElectron() {
		Vector3 startPos = transform.position;
		for(float t=0; t<1f; t+= 0.004f) {
			transform.position = Vector3.Lerp(startPos, wire1Start, t);
			yield return new WaitForEndOfFrame();
		}

		startPos = transform.position;
		for (float t = 0; t < 1f; t += 0.004f) {
			transform.position = Vector3.Lerp(startPos, wire1End, t);
			yield return new WaitForEndOfFrame();
		}

		if (!electricityOn) {
			Instantiate(electricity, new Vector3(0.284f, -0.68f, 5.23f), Quaternion.identity);
			electricityOn = true;
		}

		startPos = new Vector3(-1.65f, -0.75f, 6.61f);
		for (float t = 0; t < 1f; t += 0.004f) {
			transform.position = Vector3.Lerp(startPos, wire2Start, t);
			yield return new WaitForEndOfFrame();
		}

		for (float t = 0; t < 1f; t += 0.004f) {
			transform.position = Vector3.Lerp(wire2Start, finalPosition, t);
			yield return new WaitForEndOfFrame();
		}

		Destroy(gameObject);
	}
}
