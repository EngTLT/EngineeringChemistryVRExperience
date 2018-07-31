using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPanelLink : MonoBehaviour {
	public VRLever Lever;
	ParticleSystem[] Bubbles;
	private GameObject[] panels;
	private GameObject refPanel, light;
	GameObject[] liquid;

	float sunPanelAngle;

	void Start() {
		//when returning to wolfe island, won't do start sequence
		if(Manager.manager != null) //this is for testing scenes where the manager is not set up
			Manager.manager.firstLoad = false;

		Lever.Value = 0.5f; //Angle of the lever is set to straight up
		panels = GameObject.FindGameObjectsWithTag("Solar Panel");
		refPanel = panels[0]; //to read the rotation of all panels
		light = GameObject.Find("Directional Light");

		liquid = GameObject.FindGameObjectsWithTag("Battery Fluid");
		Bubbles = FindObjectsOfType<ParticleSystem>();
		
		foreach (ParticleSystem Bubble in Bubbles) {
			var emissions = Bubble.emission;
			emissions.rateOverTime = 0;
		}
	}

	void Update() {
		sunPanelAngle = Mathf.Abs(refPanel.transform.rotation.eulerAngles.y + 180f - light.transform.rotation.eulerAngles.y);

		if (Mathf.Abs(Lever.Value - 0.5f) > 0.2f) {
			foreach (GameObject panel in panels)
				panel.transform.Rotate(transform.up, Lever.Value - 0.5f);
		}

		if(sunPanelAngle < 45) {
			for (int i = 0; i < Bubbles.Length; i++) {
				var emission = Bubbles[i].emission;
				emission.rateOverTime = Mathf.Abs(sunPanelAngle - 45) / 5; // divide by 45 to normalize, multiply by 5 to get proper emmision rate

				liquid[i].transform.Translate(0, (sunPanelAngle - 45) / 100000, 0);
			}
		}
				
	}


}
