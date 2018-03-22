using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager_PVP : MonoBehaviour {

	public GameObject[] players = null;
	public GameObject map = null;
	public AudioClip onWinSound;

	private  bool gameIsOver = false;
	private List<GameObject> alive;
	void Start () {
		Debug.Assert(map && players.Length >= 2);
		restart();
	}
	
	public void restart() {
		foreach (var player in players) {
			gameIsOver = false;
			player.transform.localScale = new Vector3(1, 1, 1);
			player.GetComponent<Script_Entity>().respawn();
			map.GetComponent<Script_MapGenerator>().playersToSpawn = players;
			map.GetComponent<Script_MapGenerator>().generateMap();
		}
	}

	void Update () {
		checkForRestart();
		if (gameIsOver)
			return;
		getAlivePlayers();
		if (alive.Count <= 1)
			gameOver();
		
	}

	private void checkForRestart() {
		if (Input.GetKey("return")) {
			restart();
		}
			
	}

	private void getAlivePlayers() {
		alive = new List<GameObject>();
		foreach (var player in players) {
			if (player.GetComponent<Script_Player>().isAlive())
				alive.Add(player);
		}
	}

	private void gameOver() {
		GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onWinSound);
		gameIsOver = true;
		alive[0].transform.localScale = new Vector3(2, 2, 1);
	}
}
