using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metolachlor : MonoBehaviour {
	bool rotating;

	private Vector3 position;
	private static Quaternion rotation;
	private static int positionIndex = 0; //index used so that each molecule doesn't end up in the same position

	Vector3[] positions = new[] { new Vector3(21.76f, 8.78f, 18f), new Vector3(19.2f, 8.78f, 27.34f) };

	private int count;
	// Use this for initialization
	void Start () {
		StartCoroutine(EnterScene());
		rotating = false;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent != null && rotating == true) {
			rotating = false;
			position = transform.position;
			rotation = transform.rotation;
		}
		else if(!rotating && count > 15){
			count = 0;
			StartCoroutine(Return());
		}
		else if(!rotating && transform.parent == null) { count++; }

		if (rotating) {
			transform.Rotate(Vector3.up, 0.1f);
		}
	}

	IEnumerator Return() {
		Vector3 newPosition = transform.position;
		Quaternion newRotation = transform.rotation;
		for(float t = 0; t < 1; t += 0.01f) {
			transform.position = Vector3.Lerp(newPosition, position, t);
			transform.rotation = Quaternion.Lerp(newRotation, rotation, t);
			yield return new WaitForEndOfFrame();
			if (transform.parent != null)
				yield break;
		}
		rotating = true;
	}

	IEnumerator EnterScene() {
		Vector3 StartPos = transform.position;
		Vector3 EndPos = positions[positionIndex++];
		yield return new WaitForSeconds(6); //wait before bringing out chemicals

		for(float t = 0; t <= 1; t += 0.005f) {
			transform.position = Vector3.Slerp(StartPos, EndPos, t);
			transform.localScale = new Vector3(t / 2, t / 2, t / 2);
			count = 0;
			yield return new WaitForEndOfFrame();
		}
		rotating = true;
	}
}
