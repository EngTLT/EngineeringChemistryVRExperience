using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPanelLink : MonoBehaviour {
	public VRLever Lever;
	private GameObject[] panels;
	private GameObject refPanel;
	private GameObject light;
	float emission = 0;

	public Material battery;

	void Start() {
		Lever.Value = 0.5f;
		panels = GameObject.FindGameObjectsWithTag("Solar Panel");
		refPanel = panels[0]; //to read the rotation of all panels
		battery.SetColor("_EmissionColor", Color.black);
		light = GameObject.Find("Directional Light");
		emission = 0.001f;
	}

	void Update() {
		if (Mathf.Abs(Lever.Value - 0.5f) > 0.2f) {
			foreach (GameObject panel in panels)
				panel.transform.Rotate(transform.up, Lever.Value - 0.5f);
		}

		float lightPanelAngle = Mathf.Abs(light.transform.rotation.eulerAngles.y - refPanel.transform.eulerAngles.y - 180);

		if (lightPanelAngle < 30 && emission < 1) {
			emission += lightPanelAngle * emission / 1500;
			Debug.Log(emission);
			Color baseColor = new Color( 0, 3, 22); //Replace this with whatever you want for your base color at emission level '1'

			Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

			battery.SetColor("_EmissionColor", finalColor);
		}

		
	}


}
