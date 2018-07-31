using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2OBehaviour : MonoBehaviour {
	public GameObject hydrogen1, hydrogen2, oxygen, electron;
	private Vector3 startPosition;
	// Use this for initialization
	void Start() {
		StartCoroutine(Move());
		
	}

	// Update is called once per frame
	void Update() {

	}

	IEnumerator Move() {
		float spawnZ, spawnY;
		spawnY = transform.position.y;
		spawnZ = transform.position.z;

		startPosition = transform.position;
		Vector3 endPosition = new Vector3(1.65f, spawnY, spawnZ + 15);


		for (float t = 0; t < 1f; t += 0.001f) { //move O2 to the fuel cell
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return new WaitForEndOfFrame();
		}

		oxygen.transform.parent = null;
		oxygen.tag = "oxygen";

		hydrogen1.transform.parent = null;
		//hydrogen1.transform.rotation = Quaternion.identity;
		hydrogen2.transform.parent = hydrogen1.transform;
		//hydrogen2.transform.rotation = Quaternion.identity;

		Instantiate(electron, hydrogen1.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.3f);
		Instantiate(electron, hydrogen2.transform.position, Quaternion.identity);

		StartCoroutine(MoveOxygen());

		Vector3 hydrogen1start = hydrogen1.transform.position, hydrogen2start = hydrogen2.transform.position;
		Vector3 hydrogen1end = hydrogen1.transform.position + new Vector3(-3.3f, 0, 0), hydrogen2end = hydrogen1end + new Vector3(0, 0, -0.7f); //3.3 is distance across the electrolyzer, 0.7 is the offset for second hydrogen atom

		for (float t = 0; t < 1; t += 0.001f) {
			hydrogen1.transform.position = Vector3.Lerp(hydrogen1start, hydrogen1end, t);
			hydrogen2.transform.position = Vector3.Lerp(hydrogen2start, hydrogen2end, t);
			yield return new WaitForEndOfFrame();
		}

		hydrogen1start = hydrogen1.transform.position;
		hydrogen1end = hydrogen1.transform.position + new Vector3(-25f, 0, 0);

		for (float t = 0; t < 1; t += 0.001f) {
			hydrogen1.transform.position = Vector3.Lerp(hydrogen1start, hydrogen1end, t);
			yield return new WaitForEndOfFrame();
		}
		Destroy(hydrogen1);
		Destroy(this.gameObject);

	}

	IEnumerator MoveOxygen() {
		GameObject otherOxygen = GameObject.FindWithTag("oxygen");
		if(otherOxygen == null || otherOxygen == oxygen) {
			yield return null;
		}
		else {
			otherOxygen.tag = "O2";
			oxygen.tag = "O2";
			Vector3 start, end;
			start = otherOxygen.transform.position;
			end = oxygen.transform.position + new Vector3(0, 0, -1.4f);

			for (float t = 0; t < 1; t += 0.004f) {
				otherOxygen.transform.position = Vector3.Lerp(start, end, t);
				yield return new WaitForEndOfFrame();
			}
			otherOxygen.transform.parent = oxygen.transform;

			start = oxygen.transform.position;
			end = new Vector3(startPosition.x, startPosition.y, -startPosition.z);
			for (float t = 0; t < 1; t += 0.001f) {
				oxygen.transform.position = Vector3.Lerp(start, end, t);
				yield return new WaitForEndOfFrame();
			}
			Destroy(oxygen);
		}
	}
}
