using UnityEngine;
using System.Collections;
using System.Collections.Generic;




#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using System;
using System.IO;

namespace XboxCtrlrInput.Editor {
	static class InputManagerReplacer {
		[MenuItem("Window/XboxCtrlrInput/Replace InputManager.asset...")]
		static void ReplaceInputManagerAsset() {

			if (!EditorUtility.DisplayDialog("XboxCtrlrInput",
					"This will replace ProjectSettings/InputManager.asset (a backup file will be created)", "Continue", "Cancel")) {
				return;
			}

			DirectoryInfo assetsDirectory = new DirectoryInfo("Assets");
			if (!assetsDirectory.Exists) {
				Debug.LogError("Can't resolve 'Assets' directory");
				return;
			}

			string projectSettingsPath = Path.Combine(assetsDirectory.Parent.FullName, "ProjectSettings");
			if (!Directory.Exists(projectSettingsPath)) {
				Debug.LogError("Can't resolve 'ProjectSettings' directory");
				return;
			}

			string settingsFilename = EditorSettings.serializationMode == SerializationMode.ForceText ? 
				"InputManagerText": "InputManagerBinary";
			string settingsFile = Path.Combine("Assets/Editor/XboxCtrlrInput/InputManager Copies", settingsFilename);

			if (!File.Exists(settingsFile)) {
				Debug.LogError("Can't resolve '" + settingsFile + "' file");
				return;
			}

			string originalSettingsFile = Path.Combine(projectSettingsPath, "InputManager.asset");
			string backupSettingsFile = originalSettingsFile + ".bak";
			File.Copy(originalSettingsFile, backupSettingsFile);

			File.Copy(settingsFile, originalSettingsFile, true);
			Debug.Log("Backup file: " + backupSettingsFile);
		}
	}
}
#endif

namespace XboxCtrlrInput {
	public static class XCIextention {
			public static Dictionary<XboxController, float> vibrating = new Dictionary<XboxController, float>();
			public static void SetVibration(this XboxController controller, float time, float intensity, float rightintensity = -1f) {
				XInputDotNetPure.GamePad.SetVibration((XInputDotNetPure.PlayerIndex)(controller - 1), intensity, rightintensity == - 1 ? intensity : rightintensity);
				if (intensity == 0)
					return;
				if (time != 0) {
					var _mb = GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<MonoBehaviour>();
					if (_mb != null) {
						_mb.StartCoroutine(coRoutineStop(controller, time));
					}
				} else if (intensity != 0) {
					vibrating[controller] = intensity;
				}
			}

			public static IEnumerator coRoutineStop(this XboxController controller, float time) {
				yield return new WaitForSeconds(time);				
				if (!vibrating.ContainsKey(controller))
					StopVibration(controller);
				else if (vibrating[controller] == 0f)
					StopVibration(controller);
				else {
					SetVibration(controller, vibrating[controller], time);
				}
			}

			public static void StopVibration(this XboxController controller) {
				vibrating[controller] = 0f;
				XInputDotNetPure.GamePad.SetVibration((XInputDotNetPure.PlayerIndex)(controller - 1), 0, 0);
			}
	}

}