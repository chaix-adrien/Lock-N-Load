using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PersistentLoader : MonoBehaviour {
	public string saveFile;
	private string savePath;
	private SaveData save;

	void Start () {
		savePath = Application.dataPath + "/PersistentSave/" + saveFile + ".uml";
		save = SaveData.Load(savePath);
	}

	public SaveData getSave() {
		Debug.Log(save);
		return save;
	}
}
