using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour {

	GameObject[] keyspawnlocations;
	Color[] keyColours = {Color.black, Color.black, Color.black };

	public GameObject key;

	int maxKeys;

	// Use this for initialization
	void Start () {
		maxKeys = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Flashlight> ().keysRequired;

		keyspawnlocations = GameObject.FindGameObjectsWithTag ("keySpawn");			//populating list of spawnable key locations

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

		for (int i = 0; i < maxKeys; i++) {						//spawn the keys
			GameObject k = Instantiate (key, keyspawnlocations [i].transform.position, Quaternion.identity);
			k.GetComponent<MeshRenderer> ().material.color = keyColours[i];
			k.GetComponent<MeshRenderer> ().material.SetColor ("_EmissionColor", keyColours [i]);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
