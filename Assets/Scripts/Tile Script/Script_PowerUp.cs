using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PowerUp : Script_TileHandler {

	// Use this for initialization
	private Script_PowerUpSpawner spawner;
	void Start () {
		GameObject spawnerObj = GameObject.FindGameObjectWithTag("PowerUpSpawner");
		spawner = spawnerObj.GetComponent<Script_PowerUpSpawner>();
		spawner.addToSpawned(transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDestroy() {
		spawner.removeFromSpawned(transform);
	}
}
