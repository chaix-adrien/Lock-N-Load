using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;
public class Script_Score : MonoBehaviour {
	public int playerNumber;
	
	private Script_Player player = null;
	private Text killText;
	private Text ScoreText;
	public GameObject joinPannel;
	public Text joinText;
	void Start () {
		List<GameObject> playerObjs = GameObject.FindGameObjectWithTag("Map").GetComponent<Script_MapGenerator>().playersToSpawn;
		if (playerNumber >= playerObjs.Count) {
			HideAll(true);
			return;
		}
		init();
	}
	
	private void init() {
		List<GameObject> playerObjs = GameObject.FindGameObjectWithTag("Map").GetComponent<Script_MapGenerator>().playersToSpawn;
		player = playerObjs[playerNumber].GetComponent<Script_Player>();
		transform.Find("Score").GetComponent<Image>().color = player.entityColor;
		transform.Find("Kill").GetComponent<Image>().color = player.entityColor;
		ScoreText = transform.Find("Score").Find("ScoreText").GetComponent<Text>();
		killText = transform.Find("Kill").Find("KillText").GetComponent<Text>();
	}

	void Update () {
		string [] names = Input.GetJoystickNames();
		if (player) {
			killText.text = player.getKill() + "";
			ScoreText.text = player.getScore() + "";
			if (!GamePad.IsConnected((GamePad.Index)playerNumber)) {
				ShowPannel("Controller Disconected");
			} else {
				HidePannel();
			}
		} else if (Static_Datas.scoreToWin <= 0 && GamePad.IsConnected((GamePad.Index)playerNumber)) {
			ShowPannel("Press Start to join !");
			CheckToJoin();
		} else {
			HideAll(true);
		}
	}

	private void CheckToJoin() {
		if (Static_Datas.scoreToWin > 0)
			return;
		if (GamePad.GetButton(GamePad.Button.Start, (GamePad.Index)(playerNumber + 1))) {
			GameObject.FindGameObjectWithTag("GameManager").GetComponent<Script_GameManager_PVP>().CreatePlayer(playerNumber);
			init();
		}
	}

	private void ShowPannel(string text) {
		HideAll(true);
		joinText.text = text;
		joinPannel.SetActive(true);
	}

	private void HideAll(bool hide) {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(!hide);
		}
	}

	

	private void HidePannel() {
		HideAll(false);
		joinPannel.SetActive(false);
	}
}


//get score au joueur restant, pas au kill
//check si explosion de tnt = > de quiù
