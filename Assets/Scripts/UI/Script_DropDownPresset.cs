using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Presset = System.Collections.Generic.List<ToSaveTileData>;

[RequireComponent(typeof(Script_PersistentLoader))]
public class Script_DropDownPresset : MonoBehaviour {
	public Script_GenerateTileRatePannel ratePannel;
	public bool saveOnlyInEditor = false;
	public bool applyDefaultAtStart = false;
	public InputFieldSubmitOnly textFieldModal;
	private Dropdown dropdown;
	private SaveData save;
	private List<Presset> pressets;
	public List<GameObject> buttons;
	public Script_DropDownPresset otherDropDown;
	private string keyPressetNames = "pressetKeys";

	private List<string> pressetNames;
	void Start () {
		dropdown = GetComponent<Dropdown>();
		save = GetComponent<Script_PersistentLoader>().getSave();
		loadPresset();
		if (applyDefaultAtStart) {
			ratePannel.applyPresset(pressets[pressetNames.IndexOf("Default")]);
		}
		showButton(false);
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
			dropdown.value = dropdown.options.Count - 2;
			showButton(true);
			textFieldModal.gameObject.SetActive(false);
		}
	}

	public void setToDefault() {
		dropdown.value = 0;
		dropdown.Hide();
	}
	private void showButton(bool show) {
		if (saveOnlyInEditor)
			return;
		foreach (GameObject obj in buttons) {
			obj.SetActive(show);
		}
		if (show) {
			otherDropDown.setToDefault();
		}
	}

	public void doApplyPresset() {
		dropdown.Hide();
		if (dropdown.value == 0) {
			showButton(false);
			return;
		}
		if (!saveOnlyInEditor &&  dropdown.value == dropdown.options.Count - 1) {
			textFieldModal.gameObject.SetActive(true);
			textFieldModal.Select();
			showButton(false);
			return;
		}
		if (!saveOnlyInEditor)
			showButton(true);
		otherDropDown.setToDefault();
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
		save.Save();
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
		setToDefault();
		loadPresset();
	}

}
