using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	public static Manager manager;
	public bool firstLoad; //has the main scene been loaded yet?

	public Vector3 playerPosition, playerScale;
	public Quaternion playerQuaternion;

	void Awake() {
		if(manager == null) {
			manager = this;
		}else if(manager != null) {
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		firstLoad = true; //scene has not been loaded yet
	}

	public void saveTransform(GameObject player) {
		playerPosition = player.transform.position;
		playerScale = player.transform.localScale;
		playerQuaternion = player.transform.rotation;
	}

	public void loadTransform(GameObject player) {
		player.transform.position = playerPosition;
		player.transform.localScale = playerScale; ;
		player.transform.rotation = playerQuaternion;
	}

	public void LoadFerryAsync() {
		StartCoroutine(LoadFerryAsyncCoroutine());
	}

	private IEnumerator LoadFerryAsyncCoroutine() {
		int count = 0;
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("WolfeIslandLanding");
		asyncLoad.allowSceneActivation = false;
		while (!asyncLoad.isDone && count++ < 900) { Debug.Log("Loading"); yield return null; } //load scene fully before transitioning
		asyncLoad.allowSceneActivation = true;
	}
}
