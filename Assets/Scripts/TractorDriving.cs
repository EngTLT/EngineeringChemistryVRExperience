using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorDriving : MonoBehaviour {
	Rigidbody rb;
	Vector3 forward;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		forward = rb.transform.forward;

		//when returning to wolfe island, won't do start sequence
		Manager.manager.firstLoad = false;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = forward * 3f;
		//transform.Translate(transform.forward.normalized * 2f * Time.deltaTime, Space.World);
	}
}
