using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISonar : MonoBehaviour {

	private Text text;
	private Flashlight player;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		player = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Flashlight>();
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			text.text = "";
		else if (Time.timeScale == 1) {

			try {
				float ttl = player.ttl;
				float minEchoTime = player.minEchoTime;

				if(ttl > minEchoTime){
					text.color = Color.green;
					text.text = "SONAR: READY";
				}else{
					text.color = Color.red;
					text.text = "SONAR: " + Mathf.RoundToInt (minEchoTime - ttl + 1) + "s";
				}

			} catch (System.NullReferenceException) {		//player is dead
				gameObject.SetActive (false);
			}
		}
	}
}
