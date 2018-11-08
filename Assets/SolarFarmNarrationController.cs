using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFarmNarrationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(0, 3));
        
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(1, 30));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
