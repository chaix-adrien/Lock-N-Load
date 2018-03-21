using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_camera : MonoBehaviour {

	public GameObject followPlayer = null;
	public bool everyPlayerInField = true;
	private float z;

	private Vector3 middlePoint;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;
	// Use this for initialization
	void Start () {
		z = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (followPlayer) {
			transform.eulerAngles = new Vector3(0, 0, 0);
			if (followPlayer) {
				Vector3 playerPos = followPlayer.transform.position;
				transform.position = new Vector3(playerPos.x, playerPos.y, z);
			}
		}
	}
}
