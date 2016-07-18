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
    public GameObject tilePrefab_press;
    public GameObject tilePrefab_waterfall;
    public GameObject tilePrefab_WaitSpeedUp;
    public GameObject tilePrefab_rainbowRoad;
    public GameObject tilePrefab_movingZ;

    int lastMovingTileNum;


	CameraScript camScript;
	Vector3 lastTilePos;

    

	int dir; //straight = 0, turned = 1
    int nxtDir; // next direction
    int lstDir; // last direction

	// Use this for initialization
	void Start () {
	
		// initialize var
		camScript = Camera.main.GetComponent<CameraScript>();
		dir = 0; // default direction is straight
        nxtDir = 0;
        lastMovingTileNum = 0;

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

        for (int i = 0; i < num; i++)
        {


            lstDir = dir;
            dir = nxtDir;

            // change direction next loop?
            if (Random.Range(0, 5) == 1)
            {

                if (dir == 0)
                    nxtDir = 1; // turn!
                else
                    nxtDir = 0; // straight!
            }




            int rand = Random.Range(0, 12);
            GameObject tile = tilePrefab_normal;
            Vector3 rot = new Vector3(0f, 0f, 0f);

            // random road
            ////////////////////////////////////////////////////////
            if (Random.Range(0, 100) == 1) { 
                SpawnRainbowRoad(5);
                break;
               }

            // Type of tile
            switch (rand) {
                case 1:
                    tile = tilePrefab_spikes;
                    break;
                case 2:
                    if (nxtDir != 1 && lstDir != 1 && dir != 1)
                        tile = tilePrefab_saw;
                    break;
                case 3:
                    tile = tilePrefab_press;
                    break;
                case 4:
                    if (nxtDir != 1 && lstDir != 1 && dir != 1)
                        tile = tilePrefab_waterfall;
                    rot = new Vector3(0f, 180f, 0f);
                    break;
                case 5:
                    //tile = tilePrefab_WaitSpeedUp;

                    //if (nxtDir != 1 && lstDir != 1 && dir != 1)
                    //    rot = new Vector3(0f, 180f, 0f);
                    //else if (dir == 1)
                    //    rot = new Vector3(0f, -90f, 0f);
                    break;
                case 6:
                    if (nxtDir != 1 && lstDir != 1 && dir != 1 && (tiles.Count - lastMovingTileNum) > 1) { 
                        tile = tilePrefab_movingZ;
                        lastTilePos.z += 2f;
                        lastMovingTileNum = tiles.Count + 1;
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
			GameObject go = (GameObject)Instantiate (tile, SpawnPos, Quaternion.Euler(rot));

			// add to parent go
			go.transform.parent = this.transform;

			// log position
			lastTilePos = go.transform.position;

			// record into tile array
			tiles.Add( go );
        }
	}



    void SpawnRainbowRoad(int num)
    {

        GameObject tile = tilePrefab_rainbowRoad;
        Vector3 rot = new Vector3(0f, 0f, 0f);

        for (int i = 0; i < num; i++)
        {

            lstDir = dir;
            dir = nxtDir;

            // change direction next loop?
            if (Random.Range(0, 5) == 1)
            {

                if (dir == 0)
                    nxtDir = 1; // turn!
                else
                    nxtDir = 0; // straight!
            }

            // Calculate next coord
            Vector3 SpawnPos = lastTilePos;
            if (dir == 0)
            {   // straight
                SpawnPos = new Vector3(lastTilePos.x, lastTilePos.y + .5f, lastTilePos.z + 1.5f);
            }
            else if (dir == 1)
            { // turn!
                SpawnPos = new Vector3(lastTilePos.x - 1.5f, lastTilePos.y + .5f, lastTilePos.z);
            }

            // Instantiate 
            GameObject go = (GameObject)Instantiate(tile, SpawnPos, Quaternion.Euler(rot));


            // add to parent go
            go.transform.parent = this.transform;

            // log position
            lastTilePos = go.transform.position;

            // record into tile array
            tiles.Add(go);

        }
    }


	public Vector3 GetTilePos( int num ){

		//Debug.Log ("num: " + num);

		return tiles [num].transform.position;
	}

    public GameObject GetTileGO( int num)
    {
        return tiles[num];
    }

    public bool isMovingtile(int num){

        if (tiles[num].layer == 11){
            return true;
        }

        return false;
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

        Debug.Log("Player died");

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
