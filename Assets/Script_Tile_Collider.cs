using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
	using UnityEditor;
#endif
public class Script_Tile_Collider : MonoBehaviour {

	// Use this for initialization
	Collider2D col;
	void Start () {
		col = GetComponent<BoxCollider2D>();
		col.offset = new Vector2(0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#if UNITY_EDITOR
// The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem( "Assets/Create/Tile_Collider" ) ]
    public static void CreateTile_Colider()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Tile Collider", "Collider_", "prefab", "Save Tile Collider", "Prefab");
		if (path != "") {
			GameObject corePrefab = Resources.Load("Collider") as GameObject;
			Debug.Log(GameObject.FindGameObjectWithTag("TileCollider"));
			corePrefab.hideFlags = HideFlags.None;
			PrefabUtility.CreatePrefab(path, corePrefab);
		}    
    }
#endif
}
