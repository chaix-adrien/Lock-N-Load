using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_MainMenu : MonoBehaviour  {
	public GameObject option;
	public GameObject warningPannel;
	public GameObject sizeMapPanel;
	public GameObject customGamePanel;
	private enum MenuState {Main, QuickPlay, customGame};
	private MenuState state;

	void Start() {
		state = MenuState.Main;
	}

	void Update() {
		if (Input.GetAxis("Cancel") > 0) {
			if (state == MenuState.QuickPlay) {
				sizeMapPanel.SetActive(false);
				GetComponent<Script_UIDefaultSelected>().selectDefault();
				state = MenuState.Main;
			}
		}
	}

	public void quickPlay() {
#if UNITY_EDITOR
#else
		if (names.Length <= 2) {
			warningPannel.SetActive(true);
			warningPannel.transform.GetChild(0).gameObject.SetActive(true);
			return;
		}
#endif
		Static_Datas.scoreToWin = 0;
		sizeMapPanel.SetActive(true);
		state = MenuState.QuickPlay;
	}

	public void customGame() {
		Static_Datas.scoreToWin = 5;
		customGamePanel.SetActive(true);
		state = MenuState.customGame;
	}


	public void options() {
		GameObject.FindGameObjectWithTag("Menu").GetComponent<bl_PauseMenu>().DoPause();
	}

	public void quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void launchQuickPlay(int size) {
		Vector2Int dim = Vector2Int.zero;
		if (size == 0)
			dim = new Vector2Int(12, 7);
		else if (size == 1)
			dim = new Vector2Int(19, 10);
		else if (size == 2)
			dim = new Vector2Int(25, 14);
		else
			Debug.Assert(false);
		Static_Datas.sizeMap = dim;
		SceneManager.LoadScene("Game");
	}
}
