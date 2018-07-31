using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spectrometer : MonoBehaviour {
	public GameObject leftLid, rightLid, controlPanel, dial;
	TextMesh outputText;
	bool opened;

	// Use this for initialization
	void Start () {
		opened = false;
		outputText = GetComponentInChildren<TextMesh>();

	}

	// Update is called once per frame
	void Update () {
		outputText.text = dial.transform.localRotation.eulerAngles.z.ToString();
	}

	public void Open() {
		if(!opened)
			StartCoroutine(OpenBox());
	}

	IEnumerator OpenBox() {
		opened = true;
		Vector3 xaxis = new Vector3 ( 1, 0, 0 );

		for(float i = 0; i < 205; i += 1f) {
			leftLid.transform.Rotate(xaxis, -1f);
			rightLid.transform.Rotate(xaxis, 1f);

			yield return new WaitForEndOfFrame();
		}

		for (float i = 0; i < 0.244; i += 0.01f) {
			controlPanel.transform.Translate(transform.up / 100, Space.World);

			yield return new WaitForEndOfFrame();

			dial.GetComponent<DialSpectrometer>().EnableDial(); //dial can now be used
		}

	}
}
