using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_StartAnimRandom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Animator anim = GetComponent<Animator>();
		anim.Play(0, -1, Random.Range(0.0f, 1.0f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
