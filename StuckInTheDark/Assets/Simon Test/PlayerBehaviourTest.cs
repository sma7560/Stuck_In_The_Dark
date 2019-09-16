using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourTest : MonoBehaviour {

	private GameObject[] keyspawnlocations;
	private GameObject[] batteryspawnlocations;

	public GameObject key;
	Color[] keyColours = {Color.yellow, Color.blue, Color.green};

	public GameObject battery;

	public int keysRequired = 3;
	public int keysCollected;
	public bool exitReady;

	public AudioClip batteryPickupFX, keyPickupFX;
	AudioSource aud;

	public int batteries = 2;

	public int power = 100;

	public bool batteryInRange = false, keyInRange = false;

	Collider selectedKey, selectedBattery;

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource> ();
		keysCollected = 0;
		exitReady = false;
		Invoke ("spawnStuff", 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.E)) {
			if (batteryInRange) {
				addBattery ();
				selectedBattery.GetComponent<BatteryBehaviour> ().deleteThis ();
				selectedBattery = null;
				batteryInRange = false;
			}
			if (keyInRange) {
				addKey ();
				selectedKey.GetComponent<KeyBehaviour> ().deleteThis ();
				selectedKey = null;
				keyInRange = false;
			}
		}
	}

	void spawnStuff(){
		keyspawnlocations = GameObject.FindGameObjectsWithTag ("keySpawn");			//populating list of spawnable key locations
		batteryspawnlocations = GameObject.FindGameObjectsWithTag("batterySpawn");	//list of battery locations

		for (int t = 0; t < keyspawnlocations.Length; t++ ){		//shuffle list key of locations (Knuth shuffle algorithm)
			GameObject tmp = keyspawnlocations[t];
			int r = Random.Range(t, keyspawnlocations.Length);
			keyspawnlocations[t] = keyspawnlocations[r];
			keyspawnlocations[r] = tmp;

			if (t < keyColours.Length) {						//shuffle colour as well
				Color tmpC = keyColours[t];
				int rC = Random.Range(t, keyColours.Length);
				keyColours[t] = keyColours[rC];
				keyColours[rC] = tmpC;
			}
		}

		for (int t = 0; t < batteryspawnlocations.Length; t++) {		//shuffle battery spawn locations
			GameObject tmp = batteryspawnlocations[t];
			int r = Random.Range(t, batteryspawnlocations.Length);
			batteryspawnlocations[t] = batteryspawnlocations[r];
			batteryspawnlocations[r] = tmp;
		}


		for (int i = 0; i < keysRequired; i++) {						//spawn the keys
			GameObject k = Instantiate (key, keyspawnlocations [i].transform.position, Quaternion.identity);
			k.GetComponent<MeshRenderer> ().material.color = keyColours[i];
			k.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", keyColours [i]);
		}

		for (int i = 0; i < batteries; i++) {							//spawn the batteries
			GameObject b = Instantiate (battery, batteryspawnlocations [i].transform.position, Quaternion.identity);
		}
	}

	int getKeysCollected(){
		return keysCollected;
	}

	public void addKey(){
		aud.PlayOneShot (keyPickupFX);
		keysCollected++;
		if (keysCollected >= keysRequired)
			exitReady = true;
		keyInRange = false;
	}

	public void addBattery(){
		aud.PlayOneShot (batteryPickupFX);
		power += 20;
		batteryInRange = false;
	}

	void OnTriggerEnter(Collider col){

		switch(col.tag){
		case "Battery":
			batteryInRange = true;
			selectedBattery = col;
			break;
		case "key":
			keyInRange = true;
			selectedKey = col;
			break;
		}
	}

	void OnTriggerExit(Collider col){
		switch(col.tag){
		case "Battery":
			batteryInRange = false;
			selectedBattery = null;
			break;
		case "key":
			keyInRange = false;
			selectedKey = null;
			break;
		}
	}
}
