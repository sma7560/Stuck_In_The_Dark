using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumb : MonoBehaviour {
    public float expiry;
    private float count;
	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        if (count > expiry) Destroy(this.gameObject);
		
	}
    void OnTriggerEnter(Collider other)
    {
       //Debug.Log("collision name = " + other.gameObject.name);
       if (other.tag == "Monster") {
           Destroy(this.gameObject);  
       }
    }
}
