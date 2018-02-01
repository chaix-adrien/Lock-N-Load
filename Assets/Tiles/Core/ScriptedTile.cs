using UnityEngine.Tilemaps;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using System.Collections;
    using System.IO;
#endif

public class ScriptedTile : Tile 
{
    private bool init = false;
    
    //Parametre de base de la tile
    void OnEnable() {
        if (!init) {
            flags = TileFlags.InstantiateGameObjectRuntimeOnly;
            colliderType = ColliderType.None;
            init = true;
        }
    }
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Scripted Tile")]
    public static void CreateScriptedTile()
    {
        var path = "";
        var obj = Selection.activeObject;
        if (obj == null) path = "Assets";
        else path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
        if (!Directory.Exists(path)) {
            string[] tab = path.Split('/');
            tab[tab.Length - 1] = "";
            path = string.Join("/", tab);
        } else
            path += "/";
        string[] filter = {"", ""};
        filter[0] = "Image files";
        filter[1] = "png,jpg,jpeg";
        string texture = EditorUtility.OpenFilePanelWithFilters("Select Sprite", "Sprite", filter);
        ScriptedTile tile = ScriptableObject.CreateInstance<ScriptedTile>();
        ProjectWindowUtil.CreateAsset(tile, path + "NewTile.Asset");
        GameObject corePrefab = Resources.Load("Collider") as GameObject;
		corePrefab.hideFlags = HideFlags.None;
	    GameObject prefab = PrefabUtility.CreatePrefab(path + "NewTile.prefab", corePrefab);
        tile.gameObject = prefab;
        if (texture != "") {
            texture = "Assets" + texture.Replace(Application.dataPath, "");
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(texture);
            prefab.GetComponent<SpriteRenderer>().sprite = sprite;
            tile.sprite = sprite;
        }
    }
#endif
}