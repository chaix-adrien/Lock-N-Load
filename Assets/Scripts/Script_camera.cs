using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_camera : MonoBehaviour {
	public GameObject playerToFollow;

	private float z;
	// Use this for initialization
	void Start () {
		z =transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0, 0, 0);
		if (playerToFollow) {
			Vector3 playerPos = playerToFollow.transform.position;
			transform.position = new Vector3(playerPos.x, playerPos.y, z);
		}
	}
}
