using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAudio : MonoBehaviour {
	public AudioSource seagulls, waves;

	// Use this for initialization
	void Start () {
		StartCoroutine("SeagullCall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator SeagullCall() {
		yield return new WaitForSeconds(35);
		seagulls.Play();
	}
}
