using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Script_GameManager_PVP : MonoBehaviour {

	public GameObject player = null;
	public GameObject map = null;
	public AudioClip onWinSound;
	public Color[] playerColors;

	private  bool gameIsOver = false;
	private List<GameObject> alive;
	private List<GameObject> players;
	void Start () {
		Debug.Assert(map && player);
		restart();
	}
	
	public void restart() {
		Debug.Assert(playerColors.Length == 4);
		getPlayers();
		int i = 0;
		foreach (var player in players) {
			gameIsOver = false;
			player.transform.localScale = new Vector3(1, 1, 1);
			player.GetComponent<Script_Entity>().entityColor = playerColors[i];
			player.GetComponent<Script_Entity>().respawn();
			map.GetComponent<Script_MapGenerator>().playersToSpawn = players;
			map.GetComponent<Script_MapGenerator>().generateMap();
			i++;
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

	private void getPlayers() {
		players = new List<GameObject>();
		GamePad.Index[] index = {GamePad.Index.One, GamePad.Index.Two, GamePad.Index.Three, GamePad.Index.Four};
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
