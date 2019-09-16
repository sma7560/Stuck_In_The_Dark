using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySpawn : MonoBehaviour {

	GameObject[] batteryspawnlocations;

	public GameObject battery;
	public int maxBatteries;

	// Use this for initialization
	void Start () {
		maxBatteries = 5;

		batteryspawnlocations = GameObject.FindGameObjectsWithTag ("batterySpawn");			//populating list of spawnable key locations

		for (int t = 0; t < batteryspawnlocations.Length; t++) {		//shuffle battery spawn locations
			GameObject tmp = batteryspawnlocations[t];
			int r = Random.Range(t, batteryspawnlocations.Length);
			batteryspawnlocations[t] = batteryspawnlocations[r];
			batteryspawnlocations[r] = tmp;
		}
			
		for (int i = 0; i < maxBatteries; i++) {							//spawn the batteries
			GameObject b = Instantiate (battery, batteryspawnlocations [i].transform.position, Quaternion.identity);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
