using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorDriving : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.forward.normalized * 2f * Time.deltaTime, Space.World);
	}
}
