using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using Presset = System.Collections.Generic.List<ToSaveTileData>;
using DictionaryPersonal;
namespace DictionaryPersonal
{
    public static class DictionaryExtensions
    {
        public static List<T> ToList<V, T>(this Dictionary<V, T> dico)
        {
			List<T> outValue = new List<T>();
            foreach (var kvp in dico)
            {
                outValue.Add(kvp.Value);
            }
			return outValue;
        }
    }
}


public class Script_GameManager_PVP : MonoBehaviour {

	public GameObject player = null;
	public GameObject map = null;
	public AudioClip onWinSound;
	public Color[] playerColors;
	public GameObject playerWinUI;
	private  bool gameIsOver = false;
	private int scoreToWin = 0;
	private List<GameObject> alive;
	private Dictionary<int, GameObject> players;
	void Start () {
		Debug.Assert(map && player);
		players = new Dictionary<int, GameObject>();
		restart(true);
	}
	
	public void restart(bool resetScore) {
		Debug.Assert(playerColors.Length == 4);
		playerWinUI.SetActive(false);
		GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<Script_PowerUpSpawner>().spawnRate = Static_Datas.poerUpSpawnTime;
		GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<Script_PowerUpSpawner>().timeBeforeFirst = Static_Datas.poerUpSpawnTime;
		GameObject.FindGameObjectWithTag("Map").GetComponent<Script_MapGenerator>().setPresset(Static_Datas.presset);
		if (resetScore) {
			resetPlayers();
		}
		getPlayers();
		int i = 0;
		map.GetComponent<Script_MapGenerator>().playersToSpawn = players.ToList();
		foreach (var player in players) {
			gameIsOver = false;
			player.Value.transform.localScale = new Vector3(1, 1, 1);
			player.Value.GetComponent<Script_Entity>().entityColor = playerColors[i];
			player.Value.GetComponent<Script_Entity>().entityName = "Player " + (i + 1);
			player.Value.GetComponent<Script_Player>().controllMode = Script_Player.moveMode.CONTROLLER;
			player.Value.GetComponent<Script_Player>().gamepad = (GamePad.Index)(i + 1);
			player.Value.GetComponent<Script_Entity>().respawn();
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
			Debug.Log("Add Score before " + alive[0].GetComponent<Script_Player>().getScore());
			alive[0].GetComponent<Script_Player>().addScore();
			Debug.Log("Add Score after " + alive[0].GetComponent<Script_Player>().getScore());
			Debug.Log("Score to win : " + Static_Datas.scoreToWin);
			if (Static_Datas.scoreToWin > 0 && alive[0].GetComponent<Script_Player>().getScore() >= Static_Datas.scoreToWin) {
				Debug.Log("player Win");
				playerWin(alive[0]);
			} else {
				Debug.Log("gameOver");
				gameOver();
			}	
		}
	}

	private void playerWin(GameObject player) {
		GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onWinSound);
		gameIsOver = true;
		foreach (var playerAlive in alive) {
			playerAlive.SetActive(false);
		}
		playerWinUI.GetComponent<Script_PlayerWin_PVP>().playerWin(player.GetComponent<Script_Player>());
	}

	private void checkForRestart() {
		if (Input.GetKey("return")) {
			restart(false);
		}
	}

	private void resetPlayers() {
		foreach (var player in players) {
			player.Value.GetComponent<Script_Player>().Reset();
		}
	}

	private void removePlayers() {
		if (players.Count >= 1)
			foreach (var player in players) {
				Destroy(player.Value);
			}
		players = new Dictionary<int, GameObject>();
	}

	private void getPlayers() {
		string [] names = Input.GetJoystickNames();
		for (int i = 0; i < names.Length; i++) {
			if (names[i] != "") {
				if (!players.ContainsKey(i)) {
					Debug.Log("Dont Contain Key " + i);
					players.Add(i, Instantiate(player, Vector3.zero, Quaternion.identity));
				}
			}
		}
	}

	private void getAlivePlayers() {
		alive = new List<GameObject>();
		foreach (var player in players) {
			if (player.Value.GetComponent<Script_Player>().isAlive())
				alive.Add(player.Value);
		}
	}

	private void gameOver() {
		GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onWinSound);
		gameIsOver = true;
		alive[0].transform.localScale = new Vector3(2, 2, 1);
	}
}


/*
	custome Gamee: start / b
	score to win: min = unlimited
	quand gameOVer: afficher panel "next round"
 */