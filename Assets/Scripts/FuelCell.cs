using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCell : MonoBehaviour {
	public GameObject O2, H2, H20;

	int oxygenCounter; //oxygen spawns half as often

	// Use this for initialization
	void Start () {
		StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Spawn() {
		float spawnY, spawnZ;

		while (true) {
			spawnY = Random.Range(-2.8f, 2.8f);
			spawnZ = Random.Range(-2.8f, 2.8f);

			Instantiate(H2, new Vector3(20, spawnY, spawnZ), randomQuaternion());
			if(oxygenCounter%2 == 0)
				Instantiate(O2, new Vector3(-20, spawnY, spawnZ+11), randomQuaternion());

			oxygenCounter++;

			yield return new WaitForSeconds(2);
		}
	}

	Quaternion randomQuaternion() {
		Quaternion quat;

		float x, y, z, w;

		x = Random.Range(0, 1f);
		y = Random.Range(0, 1f);
		z = Random.Range(0, 1f);
		w = Random.Range(0, 1f);

		quat = new Quaternion(x,y,z,w);

		return quat;
	}
}
