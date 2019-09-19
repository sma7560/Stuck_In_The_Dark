using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour {

	private Canvas options;

	// Use this for initialization
	void Start () {
		options = GetComponent<Canvas> ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			options.enabled = false;
		} else {
			options.enabled = true;
		}
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
