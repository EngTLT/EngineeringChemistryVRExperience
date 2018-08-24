using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPanelLink : MonoBehaviour {
	public GameObject gaugePointer; //pointer on the power gauge
	Vector3 pointerStart; //start angle of gaugePointer

	public VRLever Lever; //lever that player manipulates to rotate panels
	ParticleSystem[] Bubbles; //bubble effect in hydrogen tanks
	private GameObject[] panels; //solar panels themselves
	private GameObject refPanel, light; //refPanel is just a panel that is used to get the angles of all the panels
	GameObject[] liquid; //liquid in hydrogen tanks that recedes when they are filled

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

		//save pointers start position on gauge for power level
		pointerStart = gaugePointer.transform.localEulerAngles;

	}

	void Update() {
		sunPanelAngle = Mathf.Abs(refPanel.transform.rotation.eulerAngles.y + 180f - light.transform.rotation.eulerAngles.y); //angle between sun and panels

		if (Mathf.Abs(Lever.Value - 0.5f) > 0.2f) { //if lever is pulled past a certain threshold
			foreach (GameObject panel in panels) //update the rotation of all the panels
				panel.transform.Rotate(transform.up, Lever.Value - 0.5f);
		}

		if(sunPanelAngle < 45) {
			//show power level on gauge by rotating the pointer
			gaugePointer.transform.localEulerAngles = new Vector3(pointerStart.x, pointerStart.y, -120 + ((45f - sunPanelAngle) / 45 * 283));

			//update rate of bubble emission based on angle between the panel and sun
			for (int i = 0; i < Bubbles.Length; i++) {
				var emission = Bubbles[i].emission;
				emission.rateOverTime = Mathf.Abs(sunPanelAngle - 45) / 5; // divide by 45 to normalize, multiply by 5 to get proper emmision rate

				liquid[i].transform.Translate(0, (sunPanelAngle - 45) / 100000, 0);
			}
		}
				
	}


}
