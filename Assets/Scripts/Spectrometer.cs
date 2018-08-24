using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spectrometer : MonoBehaviour {
	public GameObject leftLid, rightLid, controlPanel, dial, pointer;
	TextMesh outputText;
	bool opened;

	private float prevRotation, rotation, wavenumber;

	Vector3 pointerStart, pointerEnd;

	// Use this for initialization
	void Start () {
		opened = false;
		outputText = GetComponentInChildren<TextMesh>();
		prevRotation = dial.transform.localRotation.eulerAngles.z;

		pointerStart = pointer.transform.localEulerAngles;
		pointerEnd = new Vector3(pointerStart.x, pointerStart.y, -34f);
	}

	// Update is called once per frame
	void Update () {
		rotation = dial.transform.localRotation.eulerAngles.z;

		float diff = rotation - prevRotation;

		if (Mathf.Abs(diff) < 300) {
			wavenumber += diff;
			wavenumber = Mathf.Clamp(wavenumber, 0f, 4000f);
		}

		float transmittance = Mathf.Abs(1700f - wavenumber);
		if ( transmittance < 100) {
			pointer.transform.localEulerAngles = new Vector3(pointerStart.x, pointerStart.y, -120 + (Mathf.Abs(transmittance-100)/100 * 283)); //I'll be honest, I don't fully understand the math here, took me a lot of trial and error to get this work so trust that it will update the gauge appropriately
		}

		outputText.text = wavenumber.ToString();

		prevRotation = rotation;

	}

	public void Open() {
		if(!opened)
			StartCoroutine(OpenBox());
	}

	IEnumerator OpenBox() {
		opened = true;
		Vector3 xaxis = new Vector3 ( 1, 0, 0 );

		for(float i = 0; i < 205; i += 1f) { //open lids
			leftLid.transform.Rotate(xaxis, -1f);
			rightLid.transform.Rotate(xaxis, 1f);

			yield return new WaitForEndOfFrame();
		}

		for (float i = 0; i < 0.244; i += 0.01f) { //raise control panel to player
			controlPanel.transform.Translate(transform.up / 100, Space.World);

			yield return new WaitForEndOfFrame();
		}

		dial.GetComponent<DialSpectrometer>().EnableDial(); //dial can now be used

	}
}
