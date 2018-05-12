using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;
public class Script_Score : MonoBehaviour {
	public int playerNumber;
	public GameObject lifebar;
	
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
		player.lifeBar = lifebar;
		transform.Find("Score").GetComponent<Image>().color = player.entityColor;
		transform.Find("Kill").GetComponent<Image>().color = player.entityColor;
		lifebar.GetComponent<LineRenderer>().startColor = player.entityColor;
		lifebar.GetComponent<LineRenderer>().endColor = player.entityColor;
		ScoreText = transform.Find("Score").Find("ScoreText").GetComponent<Text>();
		killText = transform.Find("Kill").Find("KillText").GetComponent<Text>();
		wasDesconected = true;
	}

	bool wasDesconected = false;
	void Update () {
		if (player) {
			killText.text = player.getKill() + "";
			ScoreText.text = player.getScore() + "";
			if (!XCI.IsPluggedIn(player.GetComponent<Script_Player>().gamepad)) {
				ShowPannel("Controller Disconected");
				Time.timeScale = 0.0f;
				wasDesconected = true;
			} else if (wasDesconected) {
				HidePannel();
				Time.timeScale = 1f;
				wasDesconected = false;
			}
		} else if (Static_Datas.scoreToWin <= 0 && XCI.IsPluggedIn((XboxController)(playerNumber + 1))) {
			ShowPannel("Press Start to join !");
			CheckToJoin();
			
		} else {
			HideAll(true);
		}
	}

	private void CheckToJoin() {
		if (Static_Datas.scoreToWin > 0)
			return;
		if (XCI.GetButton(XboxButton.Start, (XboxController)(playerNumber + 1))) {
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

//check si explosion de tnt = > de quiù
