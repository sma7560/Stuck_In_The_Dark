using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	private Canvas pause;
	private Flashlight player;

	// Use this for initialization
	void Start () {
		pause = GetComponent<Canvas> ();
		pause.enabled = false;

		player = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Flashlight>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			pause.enabled = false;
		} else if(!player.wonGame && player.alive) {
			pause.enabled = true;
		}
	}

	public void resumeGame(){
		Time.timeScale = 1;
	}
}
