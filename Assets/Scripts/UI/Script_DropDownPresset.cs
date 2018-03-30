using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Presset = System.Collections.Generic.List<ToSaveTileData>;

public class Script_DropDownPresset : MonoBehaviour {
	public string saveFile;
	public Script_GenerateTileRatePannel ratePannel;
	private string savePath;
	private Dropdown dropdown;
	private SaveData save;
	private List<Presset> pressets;
	private string keyPressetNames = "pressetKeys";

	private List<string> pressetNames;
	void Start () {
		savePath = Application.dataPath + "/PersistentSave/" + saveFile;
		dropdown = GetComponent<Dropdown>();
		
		save = SaveData.Load(savePath);
		loadPresset();
	}
	
	private void loadPressetNames() {
		pressetNames = new List<string>();
		if (save.HasKey(keyPressetNames)) {
			pressetNames = save.GetValue<List<string>>(keyPressetNames);
		}
	}

	public void doApplyPresset() {
		if (dropdown.value != 0) {
			ratePannel.applyPresset(pressets[dropdown.value - 1]);
		}
	}

	private void loadPresset() {
		loadPressetNames();
		pressets = new List<Presset>();
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
		Dropdown.OptionData firstOption = dropdown.options[0];
		dropdown.options.Clear();
		options.Add(firstOption);
		foreach (string keyPresset in pressetNames) {
			if (save.HasKey(keyPresset)) {
				pressets.Add(save.GetValue<Presset>(keyPresset));
				options.Add(new Dropdown.OptionData(keyPresset));
			} else {
				pressetNames.Remove(keyPresset);
			}
		}
		dropdown.AddOptions(options);
		save[keyPressetNames] = pressetNames;
		save.Save(savePath);
	}

	
	public void doSaveCurentPresset(string pressetName) {
		savePresset(save, ratePannel.getCurentPresset(), pressetName);
	}

	private void savePresset(SaveData saveData, Presset presset, string pressetName) {
		if (!pressetNames.Contains(pressetName))
			pressetNames.Add(pressetName);
		saveData[keyPressetNames] = pressetNames;
		saveData[pressetName] = presset;
		save.Save();
		loadPresset();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
