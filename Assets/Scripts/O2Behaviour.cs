using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2Behaviour : MonoBehaviour {

	// Use this for initialization
	void Start() {
		StartCoroutine(MoveO2());
	}

	// Update is called once per frame
	void Update() {

	}

	IEnumerator MoveO2() {
		float spawnZ, spawnY;
		spawnY = transform.position.y;
		spawnZ = transform.position.z;

		Vector3 startPosition = transform.position;
		Vector3 fuelCell = new Vector3(-1.65f, spawnY, spawnZ - 11);

		Vector3 fuelCell1 = fuelCell + new Vector3(0, 0.7f, 0.7f);
		Vector3 fuelCell2 = fuelCell + new Vector3(0, -0.7f, -0.7f);

		for (float t = 0; t < 1f; t += 0.001f) { //move O2 to the fuel cell
			transform.position = Vector3.Lerp(startPosition, fuelCell, t);
			yield return new WaitForEndOfFrame();
		}


		//________________________BEGIN CLUSTERFUCK CODE________________________________________

		//Ok, this is confusing, but I will try explain succinctly
		//each molecule has two child spheres
		Transform[] hydrogenChildren1 = new Transform[2], hydrogenChildren2 = new Transform[2], OxygenChildren = new Transform[2]; //these arrays will contain the transforms of the child spheres

		GameObject hydrogenfinder = GameObject.FindGameObjectWithTag("H2"); //find one H2 molecule (has 2 Hydrogen atoms, for one Oxygen atom)
		while (hydrogenfinder == null) {//if one isn't found keep searching
			hydrogenfinder = GameObject.FindGameObjectWithTag("H2");
			Debug.Log("fail");
		}
		int i = 0;
		foreach (Transform tr in hydrogenfinder.transform) { //this is the only way I could find to get the child transforms, it's not pretty but it works
			hydrogenChildren1[i] = tr;
			i++;
		}

		hydrogenfinder.tag = "H"; //can no longer be found by search function
		hydrogenfinder = null;

		hydrogenfinder = GameObject.FindGameObjectWithTag("H2"); //find another pair of hydrogens		
		while (hydrogenfinder == null)//if one isn't found keep searching
			hydrogenfinder = GameObject.FindGameObjectWithTag("H2");
		i = 0;//have to reset this to zero
		foreach (Transform tr in hydrogenfinder.transform) {
			hydrogenChildren2[i] = tr;
			i++;
		}

		hydrogenfinder.tag = "H"; //can no longer be found by search function
		hydrogenfinder = null;

		i = 0; //reset once again
		foreach (Transform tr in transform) { //finally we set up the oxygens
			OxygenChildren[i] = tr;
			i++;
		}

		//______________________________END OF CLUSTERFUCK CODE____________________________________________

		//free the oxygen pair
		OxygenChildren[0].parent = null;
		OxygenChildren[1].parent = null;

		//now pair 2 hydrogen with each oxygen
		Vector3 OxygenPos1 = OxygenChildren[0].position;
		Vector3 OxygenPos2 = OxygenChildren[1].position;

		Vector3 HydrogenPos1 = hydrogenChildren1[0].position;
		Vector3 HydrogenPos2 = hydrogenChildren1[1].position;
		Vector3 HydrogenPos3 = hydrogenChildren2[0].position;
		Vector3 HydrogenPos4 = hydrogenChildren2[1].position;
		for (float t = 0; t < 1; t += 0.01f) {
			//first oxygen
			hydrogenChildren1[0].position = Vector3.Lerp(HydrogenPos1, OxygenPos1 + new Vector3(0, -0.6f, 0.68f), t);
			hydrogenChildren1[1].position = Vector3.Lerp(HydrogenPos2, OxygenPos1 + new Vector3(0, -0.6f, -0.68f), t);

			//second oxygen
			hydrogenChildren2[0].position = Vector3.Lerp(HydrogenPos3, OxygenPos2 + new Vector3(0, -0.6f, 0.68f), t);
			hydrogenChildren2[1].position = Vector3.Lerp(HydrogenPos4, OxygenPos2 + new Vector3(0, -0.6f, -0.68f), t);
			yield return new WaitForEndOfFrame();
		}

		//set parents for hydrogen atoms
		hydrogenChildren1[0].parent = OxygenChildren[0];
		hydrogenChildren1[1].parent = OxygenChildren[0];
		hydrogenChildren2[0].parent = OxygenChildren[1];
		hydrogenChildren2[1].parent = OxygenChildren[1];

		for (float t = 0; t < 1f; t += 0.004f) {
			OxygenChildren[0].position = Vector3.Lerp(fuelCell1, new Vector3(startPosition.x, startPosition.y +0.7f, -startPosition.z + 0.75f ), t);
			OxygenChildren[1].position = Vector3.Lerp(fuelCell2, new Vector3(startPosition.x, startPosition.y - 0.7f, -startPosition.z - 0.75f), t);
			yield return new WaitForEndOfFrame();
		}
		//Time to destroy things
		Destroy(OxygenChildren[0].gameObject);
		Destroy(OxygenChildren[1].gameObject);
		Destroy(GameObject.FindGameObjectWithTag("H"));
		Destroy(GameObject.FindGameObjectWithTag("H"));
		Destroy(this.gameObject);
	}	
}

