﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using Presset = System.Collections.Generic.List<ToSaveTileData>;

public class Script_GameManager_PVP : MonoBehaviour {

	public GameObject player = null;
	public GameObject map = null;
	public AudioClip onWinSound;
	public Color[] playerColors;

	private  bool gameIsOver = false;
	private int scoreToWin = 0;
	private List<GameObject> alive;
	private List<GameObject> players;
	void Start () {
		Debug.Assert(map && player);
		players = new List<GameObject>();
		restart();
	}
	
	public void restart() {
		Debug.Assert(playerColors.Length == 4);
		GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<Script_PowerUpSpawner>().spawnRate = Static_Datas.poerUpSpawnTime;
		GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<Script_PowerUpSpawner>().timeBeforeFirst = Static_Datas.poerUpSpawnTime;
		GameObject.FindGameObjectWithTag("Map").GetComponent<Script_MapGenerator>().setPresset(Static_Datas.presset);
		
		scoreToWin = Static_Datas.scoreToWin;
		removePlayers();
		getPlayers();
		int i = 0;
		map.GetComponent<Script_MapGenerator>().playersToSpawn = players;
		foreach (var player in players) {
			gameIsOver = false;
			player.transform.localScale = new Vector3(1, 1, 1);
			player.GetComponent<Script_Entity>().entityColor = playerColors[i];
			player.GetComponent<Script_Entity>().respawn();
			map.GetComponent<Script_MapGenerator>().generateMap();
			i++;
		}
	}

	void Update () {
		checkForRestart();
		if (gameIsOver)
			return;
		getAlivePlayers();
		if (alive.Count <= 1 && players.Count > 1) {
			alive[0].GetComponent<Script_Player>().addScore();
			if (alive[0].GetComponent<Script_Player>().getScore() >= scoreToWin) {
				playerWin(alive[0]);
			}
			gameOver();
		}
	}

	private void playerWin(GameObject player) {
		gameOver();
	}

	private void checkForRestart() {
		if (Input.GetKey("return")) {
			restart();
		}
	}

	private void removePlayers() {
		if (players.Count >= 1)
			foreach (var player in players) {
				Destroy(player);
			}
	}

	private void getPlayers() {
		players = new List<GameObject>();
		string [] names = Input.GetJoystickNames();
		for (int i = 0; i < names.Length; i++) {
			if (names[i] != "") {
				players.Add(Instantiate(player, Vector3.zero, Quaternion.identity));
			}
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
