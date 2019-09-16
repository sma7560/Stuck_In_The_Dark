using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player") {
			if (col.GetComponent<Flashlight> ().exitReady) {
				Debug.Log ("winner is you");
				col.GetComponent<Flashlight> ().win ();
			}
		}
	}
}
