using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBladesOnTurbine : MonoBehaviour {
	float rotationSpeed;

	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range(0.9f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward, rotationSpeed);
	}
}
