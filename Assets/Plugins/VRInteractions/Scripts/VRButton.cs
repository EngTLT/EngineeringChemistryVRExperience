﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// VR button event. Used for registering callbacks in the editor
/// </summary>
[System.Serializable]
public class VRButtonEvent : UnityEvent<VRButton> {}

/// <summary>
/// VR Button. Behaves like a UI button, but exists as a physical button for you to push in VR
/// </summary>
public class VRButton : VRInteractable {

	/// <summary>
	/// Callbacks for button pressed event
	/// </summary>
	public VRButtonEvent ButtonListeners;

	/// <summary>
	/// Controllers currently interacting with the button
	/// </summary>
	List<VRGripper> ActiveControllers = new List<VRGripper> ();

	void OnTriggerEnter(Collider _collider)
	{	
		if (Interactable == true && _collider.name == "Switch") { // If the button hit's the contact switch it has been pressed
			TriggerButton ();
		}
	}

	void OnCollisionEnter(Collision _collision)
	{
		
		if (Interactable == true && _collision.collider.name == "Switch") {
			TriggerButton (); // If the button hit's the contact switch it has been pressed
		} else if (_collision.rigidbody == null)
			return;

		// In this case we are dealingwith another physics object that has collided with the button

		var gripper = _collision.rigidbody.GetComponent<VRGripper> ();


		if (gripper != null) // If we find a gripper add it to our interacting list
			ActiveControllers.Add (gripper);
	}

	void OnCollisionExit(Collision _collision)
	{
		if (_collision.rigidbody == null)
			return;

		var gripper = _collision.rigidbody.GetComponent<VRGripper> ();

		if (gripper != null)
			ActiveControllers.Remove (gripper);
			
	}

	public float TriggerHapticStrength = 0.5f;

	void TriggerButton ()
	{
		if (Interactable == false)
			return;

		//THIS IS MAX'S PERSONAL ADDITION TO LOAD WOLFE ISLAND HUB AS AN EXIT
		StartCoroutine(LoadNewScene());
		
		if (ButtonListeners != null) { // Trigger our callbacks
			ButtonListeners.Invoke (this);
		}

		foreach (VRGripper gripper in ActiveControllers) { // Trigger a response on any active controllers
			gripper.HapticVibration(0.112f, TriggerHapticStrength);
		}
	}

	IEnumerator LoadNewScene() {
		yield return new WaitForSeconds(1.5f);
		if (gameObject.name == "Button to Wolfe")
			SceneManager.LoadScene("WolfeIslandLanding");
		else if (gameObject.name == "Button to Electrolyzer")
			SceneManager.LoadScene("Electrolyzer");
	}
}
