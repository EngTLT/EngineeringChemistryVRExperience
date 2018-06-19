using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spawns the traffic in the scene at random intervals
public class CarSpawner : MonoBehaviour {
	public GameObject[] cars;
	public GameObject spawner;
	

	// Use this for initialization
	void Start () {
		StartCoroutine("SpawnVehicle");
	}
	
	IEnumerator SpawnVehicle() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(20, 30)); //wait random time before spawning
			Instantiate(cars[(int)Random.Range(0f, 2f)], spawner.transform.position, Quaternion.identity); //spawn a random vehicle
		}
	}
}
