using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2Behaviour : MonoBehaviour {

	public GameObject electron;
	bool electronNotSpawned;
	
	// Use this for initialization
	void Start () {
		electronNotSpawned = true;
		StartCoroutine(MoveH2());
	}

	// Update is called once per frame
	void Update() {
		
	}

	IEnumerator MoveH2() {
		while (transform.position.x > 1.65f) {
			transform.position = transform.position + new Vector3(-0.04f, 0, 0);
			yield return new WaitForEndOfFrame();
		}

		Instantiate(electron, transform.position, Quaternion.identity);

		Vector3 startCell = transform.position;
		Vector3 endCell = new Vector3(-1.65f, transform.position.y, transform.position.z);

		for (float t = 0; t < 3f; t += 0.004f/3f) {
			transform.position = Vector3.Lerp(startCell, endCell, t);
			yield return new WaitForEndOfFrame();
		}

		//tag = "H2";
	}
}
