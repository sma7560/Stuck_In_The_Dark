using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieCrumbs : MonoBehaviour {
    public GameObject crumb;
    public Transform player;
    public float timeBetween;
	// Use this for initialization
	void Start () {
        StartCoroutine("dropCrumb");
	}

    private IEnumerator dropCrumb()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetween); // wait 5 seconds
            Instantiate(crumb, player.transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
