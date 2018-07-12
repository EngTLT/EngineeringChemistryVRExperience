using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrolyzer : MonoBehaviour {
	public GameObject H2O;
	

	// Use this for initialization
	void Start() {
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn() {
		float spawnY, spawnZ;

		while (true) {
			spawnY = Random.Range(-2.8f, 2.8f);
			spawnZ = Random.Range(-2.8f, 2.8f);

			Instantiate(H2O, new Vector3(5, spawnY, spawnZ-15), randomQuaternion());


			yield return new WaitForSeconds(3);
		}
	}

	Quaternion randomQuaternion() { //creates a random quaternion, duh
		Quaternion quat;

		float x, y, z, w;

		x = Random.Range(0, 1f);
		y = Random.Range(0, 1f);
		z = Random.Range(0, 1f);
		w = Random.Range(0, 1f);

		quat = new Quaternion(x, y, z, w);

		return quat;
	}
}
