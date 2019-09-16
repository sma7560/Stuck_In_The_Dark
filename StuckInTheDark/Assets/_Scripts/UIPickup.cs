using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickup : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent<Flashlight> ().batteryInRange || player.GetComponent<Flashlight> ().keyInRange) {
			GetComponent<Text> ().enabled = true;
		} else
			GetComponent<Text> ().enabled = false;
	}
}
