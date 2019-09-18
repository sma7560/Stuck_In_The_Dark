using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BaseAi : MonoBehaviour {
    public float distanceFrom;
    public Transform target;
    private Transform crumb;
    public Transform me;
    private NavMeshAgent navComponent;
    public AudioSource aud;

	public AudioClip attack;
	public AudioClip playerdeathfx;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navComponent = this.gameObject.GetComponent<NavMeshAgent>();
        aud = GetComponent<AudioSource>();
        aud.Play();
    }
	
	// Update is called once per frame
	void Update () {
        float dist1 = Vector3.Distance(target.position, transform.position);
        //float dist2 = Vector3.Distance(crumb.position, transform.position);
        if (dist1 < distanceFrom)
        {
            navComponent.SetDestination(target.position);
        }
        else {
            try {
                crumb = GameObject.FindGameObjectWithTag("crumb").transform;
                if (crumb)
                {
                    navComponent.SetDestination(crumb.position);
                }
            }
            catch (NullReferenceException e) {
            }
        }
    }
    void OnTriggerEnter(Collider col){
		if (col.transform.tag == "MainCamera") {
			aud.PlayOneShot (attack);
			GetComponent<MonsterMove> ().attack ();
			StartCoroutine (PlayerDeath());
		}

    }

	IEnumerator PlayerDeath(){
		yield return new WaitForSeconds(0.5f); // wait time
		aud.PlayOneShot (playerdeathfx);
		yield return new WaitForSeconds(0.5f); // wait time

		Scene scene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (scene.name);
	}
}
