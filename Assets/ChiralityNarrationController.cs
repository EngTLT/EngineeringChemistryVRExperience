using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiralityNarrationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(0, 3));
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(1, 26));
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(2, 90));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
