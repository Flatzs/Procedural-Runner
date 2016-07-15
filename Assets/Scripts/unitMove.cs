using UnityEngine;
using System.Collections;

	

public class unitMove : MonoBehaviour {

	float initXRot;
	Animator anim; 

	int currentTile;

	CameraScript camScript;
	public TileMap tileMap;

	// Use this for initialization
	void Start () {
		
		currentTile = 0;
		camScript = Camera.main.GetComponent<CameraScript> ();
		anim = GetComponentInChildren<Animator> ();
	}

	public void moveToNextTile(){

		Vector3 tilePos = tileMap.GetTilePos (currentTile + 1);

		//x rotation
		anim.SetTrigger("MoveZ");

		// move player unit to next tile
		transform.position = new Vector3 (tilePos.x, tilePos.y + 1f, tilePos.z);


		// Jump camera to player
		// TODO: lerp pos to unit (smoothing)
		camScript.MoveToTile (transform.position);

		currentTile++;

		tileMap.SpawnTile (1);

//		if (currentTile + 5 > tileMap.GetTileCount ()) {
//
//			tileMap.SpawnNormalTile (10);
//
//			//tileMap.CleanupTiles (currentTile - 5);
//
//		}

	}

	public void moveToTile( int num ){

		Vector3 tilePos = tileMap.GetTilePos (num);

		// move player unit to next tile
		transform.position = new Vector3 (tilePos.x, tilePos.y + 1f, tilePos.z);


		// Jump camera to player
		// TODO: lerp pos to unit (smoothing)
		camScript.MoveToTile (transform.position);

		currentTile = num;

	}

	void Update(){

		if (Input.GetMouseButtonDown(0)){

			moveToNextTile ();

		}
	}

	public int GetCurrentTile(){

		return currentTile;
	}

	public void UnitPosToTileList( int n ){

		currentTile = n;
	}
		

	void OnTriggerEnter( Collider col){

		Debug.Log ("collision with player!!!");

		tileMap.ResetGame ();
	}

	

}
