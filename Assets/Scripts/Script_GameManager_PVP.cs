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
	public enum GameState {
		PLAYING,
		ENDROUND,
		ENDGAME
	}

	public GameState state;
	public GameObject playerPrefab = null;
	public GameObject map = null;
	public AudioClip onWinSound;
	public Color[] playerColors;
	public GameObject playerWinUI;
	public GameObject NextRoundUI;
	private  bool gameIsOver = false;
	private int scoreToWin = 0;
	private List<GameObject> alive;
	private Dictionary<int, GameObject> players;
	void Start () {
		Debug.Assert(map && playerPrefab);
		players = new Dictionary<int, GameObject>();
		restart(true);
	}
	
	public void restart(bool resetScore) {
		Debug.Assert(playerColors.Length == 4);
		NextRoundUI.SetActive(false);
		playerWinUI.SetActive(false);
		GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<Script_PowerUpSpawner>().spawnRate = Static_Datas.poerUpSpawnTime;
		GameObject.FindGameObjectWithTag("PowerUpSpawner").GetComponent<Script_PowerUpSpawner>().timeBeforeFirst = Static_Datas.poerUpSpawnTime;
		GameObject.FindGameObjectWithTag("Map").GetComponent<Script_MapGenerator>().setPresset(Static_Datas.presset);
		map.GetComponent<Script_MapGenerator>().generateMap();
		resetPlayers(resetScore);
		getPlayers();
		gameIsOver = false;
		state = GameState.PLAYING;
		map.GetComponent<Script_MapGenerator>().spawnPlayers();
	}

	void Update () {
		checkForRestart();
		if (gameIsOver)
			return;
		getAlivePlayers();
		if (alive.Count <= 1 && players.Count > 1) {
			alive[0].GetComponent<Script_Player>().addScore();
			if (Static_Datas.scoreToWin > 0 && alive[0].GetComponent<Script_Player>().getScore() >= Static_Datas.scoreToWin) {
				playerWin(alive[0]);
			} else {
				gameOver();
			}
		}
	}

	private void playerWin(GameObject player) {
		state = GameState.ENDGAME;
		GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(onWinSound);
		gameIsOver = true;
		foreach (var playerAlive in alive) {
			playerAlive.SetActive(false);
		}
		playerWinUI.GetComponent<Script_PlayerWin_PVP>().playerWin(player.GetComponent<Script_Player>());
	}

	private void checkForRestart() {
		if (state == GameState.ENDROUND && GamePad.GetButton(GamePad.Button.A, alive[0].GetComponent<Script_Player>().gamepad)) {
			restart(false);
		}
	}

	private void resetPlayers(bool resetScore) {
		foreach (var player in players) {
			player.Value.GetComponent<Script_Player>().Reset(resetScore);
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
			if (GamePad.IsConnected((GamePad.Index)i)) {
				if (!players.ContainsKey(i)) {
					CreatePlayer(i);
				}
			}
		}
	}

	public GameObject CreatePlayer(int idx) {
		GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		player.transform.localScale = new Vector3(1, 1, 1);
		player.GetComponent<Script_Entity>().entityColor = playerColors[idx];
		player.GetComponent<Script_Entity>().entityName = "Player " + (idx + 1);
		player.GetComponent<Script_Player>().controllMode = Script_Player.moveMode.CONTROLLER;
		player.GetComponent<Script_Player>().gamepad = (GamePad.Index)(idx + 1);
		player.GetComponent<Script_Entity>().respawn();
		players.Add(idx, player);
		map.GetComponent<Script_MapGenerator>().playersToSpawn = players.ToList();
		map.GetComponent<Script_MapGenerator>().spawnPlayer(player);
		return player;
	}

	private void getAlivePlayers() {
		alive = new List<GameObject>();
		foreach (var player in players) {
			if (player.Value.GetComponent<Script_Player>().isAlive())
				alive.Add(player.Value);
		}
	}

	private void gameOver() {
		state = GameState.ENDROUND;
		NextRoundUI.SetActive(true);
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