using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	public static Manager manager;
	public bool firstLoad;
	
	void Start() {
		firstLoad = true;
        StartCoroutine(NarrationHandler.instance.PlayLineDelayed(0, 4));
    }

	void Awake() {
		if(manager == null) {
			manager = this;
		}else if(manager != null) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		firstLoad = true; //scene has not been loaded yet
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
        yield return new WaitForSeconds(4);
        NarrationHandler.instance.PlayLine(0); //play introduction line
	}
}
