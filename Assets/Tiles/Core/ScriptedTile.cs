using UnityEditor;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections;

public class ScriptedTile : Tile 
{
    public Sprite[] m_Sprites;
    public Sprite m_Preview;
    // This refreshes itself and other RoadTiles that are orthogonally and diagonally adjacent
 
#if UNITY_EDITOR
// The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/ScriptedTile")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Road Tile", "New Road Tile", "Asset", "Save Road Tile", "Assets");
        if (path == "")
            return;
    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ScriptedTile>(), path);
    }
#endif
}