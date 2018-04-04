using UnityEngine.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
    using System.Collections;
    using System.IO;
#endif

public class ScriptedTile : Tile 
{
    private bool init = false;
    public string tileName;
    public Sprite InGameSprite;
    public bool floor = false;
    public bool inMapGeneration = true;
    public bool allowPowerUp = false;
    public bool canBeExplosed = true;
    public float defaultRate = 0.5f;

    private Sprite savedSprite;


    void OnEnable() {
        init = false;
        flags = TileFlags.InstantiateGameObjectRuntimeOnly;
        colliderType = ColliderType.None;
        savedSprite = sprite;
    }

    public bool isInit() {
        return init;
    }

    public Sprite getDisplaySprite() {
        return savedSprite;
    }

    public void setInGameSprite() {
        if (!init) {
            init = true;
            sprite = InGameSprite;
        }
    }
    public void resetSprite() {
        if (init) {
            init = false;
            sprite = savedSprite;
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
        prefab.GetComponent<Script_Tile_Collider>().tile = tile;
        if (texture != "") {
            texture = "Assets" + texture.Replace(Application.dataPath, "");
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(texture);
            prefab.GetComponent<SpriteRenderer>().sprite = sprite;
            tile.sprite = sprite;
            tile.tileName = texture;
            tile.InGameSprite = sprite;
        }
    }
#endif
}
