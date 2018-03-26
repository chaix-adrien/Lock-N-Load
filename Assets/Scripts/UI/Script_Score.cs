using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Score : MonoBehaviour {
	public int playerNumber;
	
	private Script_Player player;
	private Text killText;
	private Text ScoreText;
	void Start () {
		List<GameObject> playerObjs = GameObject.FindGameObjectWithTag("Map").GetComponent<Script_MapGenerator>().playersToSpawn;
		if (playerNumber >= playerObjs.Count) {
			gameObject.SetActive(false);
			return;
		}
		player = playerObjs[playerNumber].GetComponent<Script_Player>();
		transform.Find("Score").GetComponent<Image>().color = player.entityColor;
		transform.Find("Kill").GetComponent<Image>().color = player.entityColor;
		ScoreText = transform.Find("Score").Find("ScoreText").GetComponent<Text>();
		killText = transform.Find("Kill").Find("KillText").GetComponent<Text>();
	}
	
	void Update () {
		killText.text = player.getKill() + "";
		ScoreText.text = player.getScore() + "";
	}
}


//get score au joueur restant, pas au kill
//check si explosion de tnt = > de qui
