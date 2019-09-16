using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour {
    public Animator anim;
    public bool isWalking;
    private int walkHash = Animator.StringToHash("isWalking");
    private float x, z;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        x = transform.position.x;
        z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 vOld = new Vector2(x, z);
        float dist = Vector2.Distance(vOld, new Vector2(transform.position.x, transform.position.z));
        //Debug.Log(dist);
        if (dist > 0) anim.SetBool(walkHash, true);
        else anim.SetBool(walkHash, false);
        x = transform.position.x;
        z = transform.position.z;

    }
}
