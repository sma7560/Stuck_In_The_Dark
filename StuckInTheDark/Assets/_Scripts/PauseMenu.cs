using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	private GameObject[] elements;
	private Canvas pause;

	// Use this for initialization
	void Start () {
		pause = GetComponent<Canvas> ();
		pause.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			pause.enabled = false;
		} else {
			pause.enabled = true;
		}
	}

	public void resumeGame(){
		Time.timeScale = 1;
	}

	public void restartGame(){
		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
		Time.timeScale = 1;
	}

	public void exit(){
		Application.Quit ();
	}
}
