using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_camera : MonoBehaviour {

	public GameObject followPlayer = null;

	public GameObject CenterOnMap = null;
	public bool everyPlayerInField = true;
	private float z;

	private Vector3 middlePoint;
	private float distanceFromMiddlePoint;
	private float distanceBetweenPlayers;
	private float cameraDistance;
	private float aspectRatio;
	private float fov;
	private float tanFov;
	private Tilemap tilemap;
	private GameObject[] tiles;
	private bool placed = false;
	// Use this for initialization
	void Start () {
		z = transform.position.z;
		if (CenterOnMap)
			tilemap = CenterOnMap.GetComponent<Tilemap>();
		BoundsInt bounds = tilemap.cellBounds;
		transform.position = new Vector3(
			CenterOnMap.transform.position.x + bounds.position.x + bounds.size.x / 2f,
			CenterOnMap.transform.position.y + bounds.position.y + bounds.size.y / 2f,
			z);
	}
	
	// Update is called once per frame
	void Update () {
		if (followPlayer) {
			transform.eulerAngles = new Vector3(0, 0, 0);
			if (followPlayer) {
				Vector3 playerPos = followPlayer.transform.position;
				transform.position = new Vector3(playerPos.x, playerPos.y, z);
			}
		} else if (CenterOnMap && !placed) {
			bool allGood = true;
			tiles = GameObject.FindGameObjectsWithTag("TileCollider");
			foreach (GameObject tile in tiles) {
				if (!tile.GetComponent<Renderer>().isVisible) {
					allGood = false;
					break;
				}
				if (!allGood)
					break;
			}
			if (!allGood)
				GetComponent<Camera>().orthographicSize += 0.05f;
			else
				placed = true;
			/*BoundsInt bounds = tilemap.cellBounds;
			Debug.Log(bounds);
			if (bounds.size.x < bounds.size.y)
				GetComponent<Camera>().orthographicSize = 0.5f * bounds.size.y;
			else
				GetComponent<Camera>().orthographicSize = 0.5f * bounds.size.x;
			transform.position = new Vector3(
				CenterOnMap.transform.position.x + bounds.position.x + bounds.size.x / 2f,
				CenterOnMap.transform.position.y + bounds.position.y + bounds.size.y / 2f,
				z);*/
		}
	}
}
