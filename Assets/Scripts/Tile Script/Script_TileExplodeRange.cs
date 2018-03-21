using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileExplodeRange : Script_TileHandler {
	public int range = 1;
	bool m_Started;
	public float chance = 0.5f;
	public Tile toPutOnExplode = null;
	public Tile toPutElse = null;
	public int damageOnExplode = 40;
	public Color onHitColor = Color.white;
	private Tilemap tilemap;
	private Vector3Int pos;	
	void Start() {
		base.Start();
		Vector2Int posInt = GetComponent<Script_Tile_Collider>().pos;
		pos = new Vector3Int(posInt.x, posInt.y, 0);
	}

	public override void getShot(GameObject player, string from, string fromDetails) {
		tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
		Vector3Int check = Vector3Int.zero;
		Collider2D[] hitColliders = new Collider2D[10];
		ContactFilter2D contactFilter = new ContactFilter2D();
		var ret = Physics2D.OverlapBox(transform.position, new Vector2(1f + range, 1f + range), 0f, contactFilter, hitColliders);
		for (int i = 0; i < ret; i++) {
			Collider2D inArea = hitColliders[i];
			Script_Entity entity = inArea.gameObject.GetComponent<Script_Entity>();
			if (entity)
				entity.hit(damageOnExplode, onHitColor, "environement", "explosion");
		}

		for (check.x = pos.x - range; check.x <= pos.x + range; check.x++) {
			for (check.y = pos.y - range; check.y <= pos.y + range; check.y++) {
				ScriptedTile tile = tilemap.GetTile(new Vector3Int(check.x, check.y, 0)) as ScriptedTile;
				if (!(check.x == pos.x && check.y == pos.y)) {
					if (!tile || tile.canBeExplosed) {
						if (Random.value <= chance)
							tilemap.SetTile(new Vector3Int(check.x, check.y, 0), toPutOnExplode);
						else if (toPutElse)
							tilemap.SetTile(new Vector3Int(check.x, check.y, 0), toPutElse);
					}
				}
			}	
		}
		Invoke("destroySelf", 0.01f);
	}

	private void destroySelf() {
		tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), toPutOnExplode);
	}
}
