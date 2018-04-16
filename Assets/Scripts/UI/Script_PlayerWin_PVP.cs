using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_PlayerWin_PVP : MonoBehaviour {
	public Image playerSprite;
	public Text playerText;

	public void playerWin(Script_Player player) {
		gameObject.SetActive(true);
		playerSprite.color = player.entityColor;
		playerText.color = player.entityColor;
		playerText.text = player.entityName + " WIN !";
	}
}
