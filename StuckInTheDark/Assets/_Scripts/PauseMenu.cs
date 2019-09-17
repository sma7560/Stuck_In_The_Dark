using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	private GameObject[] elements;

	// Use this for initialization
	void Start () {
		elements = GameObject.FindGameObjectsWithTag ("pause");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			foreach (GameObject a in elements)
				a.SetActive (false);
		} else {
			foreach (GameObject a in elements)
				a.SetActive (true);
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
