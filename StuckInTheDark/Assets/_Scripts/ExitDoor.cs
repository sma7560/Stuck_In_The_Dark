using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

	private Animation opening;

	// Use this for initialization
	void Start () {
		opening = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void open(){
		opening.Play ();

	}
}
