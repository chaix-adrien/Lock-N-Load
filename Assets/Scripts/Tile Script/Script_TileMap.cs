using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class Script_TileMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
  		Tilemap tilemap = FindObjectOfType(typeof(Tilemap)) as Tilemap;
        tilemap.RefreshAllTiles();
	}
}
