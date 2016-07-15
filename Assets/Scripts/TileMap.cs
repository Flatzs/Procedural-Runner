using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {

	public unitMove playerUnit;
	public Vector3 initialSpawn;
	public Material NormalTileMaterial;

	public List<GameObject> tiles = new List<GameObject>();


	// tile prefabs
	public GameObject tilePrefab_normal;
	public GameObject tilePrefab_spikes;
	public GameObject tilePrefab_saw;


	CameraScript camScript;
	Vector3 lastTilePos;

	int dir; //straight = 0, turned = 1

	// Use this for initialization
	void Start () {
	
		// initialize var
		camScript = Camera.main.GetComponent<CameraScript>();
		dir = 0; // default direction is straight

		InitTile ();
		SpawnTile (11);

	
	}

	void InitTile(){


		// Spawn initial tile (normal)
		GameObject go = (GameObject)Instantiate (tilePrefab_normal, initialSpawn, Quaternion.identity);

		// add to parent go
		go.transform.parent = this.transform;

		// log position
		lastTilePos = go.transform.position;

		// record into tile array
		tiles.Add( go );


	}

	public void SpawnTile(int num){

		for (int i = 0; i < num; i++) {


			// change direction?
			if (Random.Range (0, 5) == 1) {

				if (dir == 0)
					dir = 1; // turn!
			else
					dir = 0; // straight!
			}

			int rand = Random.Range (0, 10);
			GameObject tile = tilePrefab_normal;



			switch (rand) {
			case 1: 
				tile = tilePrefab_spikes;
				break;
			case 2:
				if (dir != 1) {
					tile = tilePrefab_saw;
				}
				break;
			default:
				tile = tilePrefab_normal;
				break;
			}
				
			// Calculate next coord
			Vector3 SpawnPos = lastTilePos;
			if (dir == 0) { 	// straight
				SpawnPos = new Vector3 (lastTilePos.x, lastTilePos.y, lastTilePos.z + 1.5f);
			} else if (dir == 1) { // turn!
				SpawnPos = new Vector3 (lastTilePos.x - 1.5f, lastTilePos.y, lastTilePos.z);
			}

			// Instantiate 
			GameObject go = (GameObject)Instantiate (tile, SpawnPos, Quaternion.identity);

			// random color for each tile
			//go.GetComponentInChildren<Renderer> ().material.color = new Color (Random.Range (0f, 1f), Random.Range (0f, 1f), Random.Range (0f, 1f));

			// add to parent go
			go.transform.parent = this.transform;

			// log position
			lastTilePos = go.transform.position;

			// move player unit to new tile
			//playerUnit.GetComponent<unitMove> ().moveToTile (lastTilePos);

			// record into tile array
			tiles.Add( go );

		}

	}


	public Vector3 GetTilePos( int num ){

		//Debug.Log ("num: " + num);

		return tiles [num].transform.position;
	}



	// removes and deletes all tile gameObjects
	// below the int provided.
	public void CleanupTiles( int num ){
		
		for (int i = 0; i < num; i++) {

			tiles.RemoveAt (i);



		}

		playerUnit.UnitPosToTileList (0);

	}

	public void ResetGame(){

		for (int i = 0; i < tiles.Count; i++) {
			Destroy (tiles [i].gameObject);
		}
		tiles.RemoveRange (0, tiles.Count);

		// spawn initial tile 
		InitTile ();
		SpawnTile (11);

		playerUnit.UnitPosToTileList (0);
		playerUnit.moveToTile (0);


	}
}
