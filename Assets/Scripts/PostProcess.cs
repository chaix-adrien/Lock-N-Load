// C# example:
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;

public class MyBuildPostprocessor {
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
       var path = "";
	   if (EditorUserBuildSettings.activeBuildTarget.ToString() == "StandaloneOSX") {
			path = pathToBuiltProject;
			path += "/Contents/";
		} else {
			var splited = pathToBuiltProject.Split('/');
			ArrayUtility.RemoveAt(ref splited, splited.Length - 1);
			path = string.Join("/", splited);
			path += "/LockNLoad_Data/";
		}
		FileUtil.CopyFileOrDirectory(Application.dataPath + "/PersistentSave/", path + "PersistentSave/");
    }
}
#endif