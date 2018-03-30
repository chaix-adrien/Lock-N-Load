using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Presset = System.Collections.Generic.List<ToSaveTileData>;

public class Script_DropDownPresset : MonoBehaviour {
	public string saveFile;
	public Script_GenerateTileRatePannel ratePannel;
	public bool saveOnlyInEditor = false;
	public InputFieldSubmitOnly textFieldModal;
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

	public void endInputName(string name) {
		if (name != "") {
			saveCurrentPresset(name);
		}
		textFieldModal.gameObject.SetActive(false);
	}

	public void doApplyPresset() {
		if (dropdown.value == 0)
			return;
		if (dropdown.value == dropdown.options.Count - 1) {
			textFieldModal.gameObject.SetActive(true);
			textFieldModal.Select();
			return;
		}
		ratePannel.applyPresset(pressets[dropdown.value - 1]);
	}

	private void loadPresset() {
		loadPressetNames();
		pressets = new List<Presset>();
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
		Dropdown.OptionData firstOption = dropdown.options[0];
		Dropdown.OptionData newOption = dropdown.options[dropdown.options.Count - 1];
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
#if UNITY_EDITOR
		options.Add(newOption);
#else
		if (!saveOnlyInEditor) {
			options.Add(newOption);
		}
#endif
		dropdown.AddOptions(options);
		save[keyPressetNames] = pressetNames;
		save.Save(savePath);
	}

	public void doRemoveCurrentPresset() {
		if (dropdown.value == 0 || dropdown.value == dropdown.options.Count - 1)
			return;
		removePresset(save, pressets[dropdown.value - 1], pressetNames[dropdown.value - 1]);
	}
	
	public void doSaveCurentPresset() {
		if (dropdown.value == 0 || dropdown.value == dropdown.options.Count - 1)
			return;
		saveCurrentPresset(pressetNames[dropdown.value - 1]);
	}

	public void saveCurrentPresset(string pressetName) {
		savePresset(save, ratePannel.getCurentPresset(), pressetName);
	}

	private void savePresset(SaveData saveData, Presset presset, string pressetName) {
		Debug.Log("Save in " + saveData + " the presset " + pressetName);
		if (!pressetNames.Contains(pressetName))
			pressetNames.Add(pressetName);
		saveData[keyPressetNames] = pressetNames;
		saveData[pressetName] = presset;
		save.Save();
		loadPresset();
	}

	private void removePresset(SaveData saveData, Presset presset, string pressetName) {
		Debug.Log("remove presset " + pressetName);
		pressetNames.Remove(pressetName);
		pressets.Remove(presset);
		save[keyPressetNames] = pressetNames;
		save[pressetName] = new List<Presset>();
		save.Save();
		dropdown.value = 0;
		loadPresset();
	}

}
