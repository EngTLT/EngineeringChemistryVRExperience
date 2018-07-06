using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour {
	public GameObject[] rotorblades;
	bool hover;
	public GameObject dronetank, tip1;
	public Material highlight;

	// Use this for initialization
	void Start () {
		StartCoroutine(ApproachPlayer());
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject rotor in rotorblades) {
			rotor.transform.Rotate(rotor.transform.forward, 67f, Space.World);
		}
	}

	IEnumerator ApproachPlayer() {
		Vector3 start = transform.position;
		Vector3 end = new Vector3(23f, 6f, 25f);
		float t;
		float n = 0.005f;

		for (t = 0; t < 1; t += n) {
			transform.position = Vector3.Slerp(start, end, t);
			n = 0.005f *(2.2f-2*t);
			yield return new WaitForEndOfFrame();
		}
		hover = true;
		StartCoroutine(Hover());

		MeshRenderer droneTankMR = dronetank.GetComponent<MeshRenderer>();
		droneTankMR.materials = new Material[] { droneTankMR.material, highlight } ;

		tip1.SetActive(true);
	}

	IEnumerator Hover() {
		Vector3 rotateAxis;
		float t = 0.1f;
		while (hover) {
			rotateAxis = new Vector3(Random.Range(0, 360f), 0f, Random.Range(0, 360f));

			while (t > -0.1f){
				transform.Rotate(rotateAxis, t*2f, Space.World);
				transform.Translate(new Vector3(0, t*0.025f, 0), Space.World);
				t -= 0.002f;
				yield return new WaitForEndOfFrame();
			}

			while (t < 0.1f) {
				transform.Rotate(rotateAxis, t*2f, Space.World);
				transform.Translate(new Vector3(0, t * 0.025f, 0), Space.World);
				t += 0.002f;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
