using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_MainMenu : Script_ButtonMenu_Button {
	public GameObject option;
	public void quickPlay() {
		string[] names = Input.GetJoystickNames();
#if UNITY_EDITOR
#else
	if (names.Length >= 2)
#endif
		SceneManager.LoadScene("Game");
	}

	void Update() {
			GetComponent<Collider2D>().enabled = !option.activeSelf;
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
}
