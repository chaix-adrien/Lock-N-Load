using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Script_TileExplosionBase : Script_TileHandler {

	public int range = 1;
	public float chance = 0.5f;
	public Tile toPutOnExplode = null;
	public Tile toPutElse = null;
	public Texture particle;
	public bool onlyOnFloor = false;
	public int damageOnExplode = 40;
	public Color onHitColor = Color.white;
	public bool onWalk = false;
	public bool onShoot = true;
	public AudioClip explosionSound;
	private Tilemap tilemap;
	protected Vector2Int pos;	
	protected override void Start() {
		base.Start();
		Vector2Int posInt = GetComponent<Script_Tile_Collider>().pos;
		pos = new Vector2Int(posInt.x, posInt.y);
		tilemap = GameObject.FindGameObjectWithTag("Map").GetComponent<Tilemap>();
	}

	public override void getShot(GameObject player, string from, string fromDetails) {
		if (onShoot) {
			base.getShot(player, from, fromDetails);
			launchExplosion(player);
		}
	}

	protected override void walkedOnEnter(Collider2D entity) {
		if (onWalk) {
			GetComponent<SpriteRenderer>().enabled = false;
			StartCoroutine(launcheExplosionDelayed(entity.gameObject, 0.1f));
			//launchExplosion(entity.gameObject);
		}		
	}

	IEnumerator launcheExplosionDelayed(GameObject madeExplosion, float delay) {
		yield return new WaitForSeconds(delay);
		launchExplosion(madeExplosion);
	}

	protected virtual void launchExplosion(GameObject madeExplosion) {
		GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<Script_AudioPlayer>().play(explosionSound);
		explode(new Vector2Int(pos.x, pos.y), madeExplosion);
	}

	protected bool explode(Vector2Int check, GameObject madeExplosion) {
		if (damageOnExplode > 0) {
			Collider2D[] hitColliders = new Collider2D[50];
			ContactFilter2D contactFilter = new ContactFilter2D();
			var ret = Physics2D.OverlapBox(new Vector3(check.x + 0.5f, check.y + 0.5f, 0), new Vector2(1f, 1f), 0f, contactFilter, hitColliders);
			for (int i = 0; i < ret; i++) {
				Collider2D inArea = hitColliders[i];
				Script_Entity entity = inArea.gameObject.GetComponent<Script_Entity>();
				if (entity) {
					entity.hit(damageOnExplode, onHitColor, "environement", "explosion blast", madeExplosion);
				}
			}
		}
		ScriptedTile tile = tilemap.GetTile(new Vector3Int(check.x, check.y, 0)) as ScriptedTile;
		var emitParams = new ParticleSystem.EmitParams();
		emitParams.applyShapeToPosition = true;
		emitParams.position = new Vector3(check.x + 0.5f, check.y + 0.5f, 0);
		if (!(check.x == pos.x && check.y == pos.y) && tile) {
			if (!onlyOnFloor || tile.floor) {
				if (!tile || tile.canBeExplosed) {
					if (Random.Range(0, 1.0f) <= chance) {
						tilemap.SetTile(new Vector3Int(check.x, check.y, 0), toPutOnExplode);
					} else if (toPutElse) {
						tilemap.SetTile(new Vector3Int(check.x, check.y, 0), toPutElse);
					}
					if (particle)
						GameObject.FindGameObjectWithTag("ImpactParticle").GetComponent<Scirpt_ParticleSystem>().Emit(emitParams, particle, 10, Color.white);
					return true;
				} else
					return false;
			} else
				return false;
		} else {
			Invoke("destroySelf", 0.02f);
			return true;
		}
	}
	
	private void destroySelf() {
		tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), toPutOnExplode);
	}
}
