using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_MainMenu : MonoBehaviour  {
	public GameObject option;

	private List<GraphicRaycaster> raycast;
	private List<Script_ButtonMenuNavigation> menus;

	void Start() {
		raycast = new List<GraphicRaycaster>();
		menus = new List<Script_ButtonMenuNavigation>();
		GetComponentsInChildren<GraphicRaycaster>(raycast);
		GetComponentsInChildren<Script_ButtonMenuNavigation>(menus);
	}

	void Update() {
		foreach (var ray in raycast) {
			ray.enabled = !option.activeSelf;
		}
		foreach (var menu in menus) {
			menu.enabled = !option.activeSelf;
		}
	}

	public void quickPlay() {
		string[] names = Input.GetJoystickNames();
#if UNITY_EDITOR
#else
	if (names.Length >= 2)
#endif
		SceneManager.LoadScene("Game");
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
