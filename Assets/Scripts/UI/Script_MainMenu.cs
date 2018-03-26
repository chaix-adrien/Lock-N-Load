using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_MainMenu : MonoBehaviour  {
	public GameObject option;
	public GameObject warningPannel;
	
	public void quickPlay() {
#if UNITY_EDITOR
		SceneManager.LoadScene("Game");
#else
		if (names.Length >= 2) {
			SceneManager.LoadScene("Game");
		} else {
			warningPannel.SetActive(true);
			warningPannel.transform.GetChild(0).gameObject.SetActive(true);
		}
#endif
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
