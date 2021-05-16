using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKey : MonoBehaviour {

	private Text text;

	private int k, km;

	private Flashlight player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Flashlight>();
		text = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			text.text = "";
		else if (Time.timeScale == 1) {
			try {
				if (!player.wonGame) {
					k = player.keysCollected;
					km = player.keysRequired;
					text.text = "KEYS: " + k + "/" + km;

					if (k >= km) {
						text.color = Color.green;
						text.text = "KEYS: " + k + "/" + km + " GET TO THE EXIT!";
					}
				} else {
					text.text = "YOU WIN";
				}
			} catch (System.NullReferenceException) {		//player is dead
				gameObject.SetActive (false);
			}
		}
	}
}
