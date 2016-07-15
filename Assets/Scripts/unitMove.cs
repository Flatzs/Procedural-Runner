using UnityEngine;
using System.Collections;

	

public class unitMove : MonoBehaviour {

	float initXRot;
	Animator anim; 

	int currentTile;

	CameraScript camScript;
	public TileMap tileMap;

    public TextMesh scoreText;
    BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		
		currentTile = 0;
		camScript = Camera.main.GetComponent<CameraScript> ();
		anim = GetComponentInChildren<Animator> ();
        boxCollider = GetComponent<BoxCollider>();
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

		if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")){

			moveToNextTile ();
            scoreText.text = currentTile.ToString();


		}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(moveNumTiles(13));
        }

        
	}

	public int GetCurrentTile(){

		return currentTile;
	}

	public void UnitPosToTileList( int n ){

		currentTile = n;
	}
		

	void OnTriggerEnter( Collider col){

        if (col.gameObject.layer == 9)
		    tileMap.ResetGame ();
	}

    public void OnSpeedUp()
    {
        StartCoroutine(moveNumTiles(13));
    }

    IEnumerator moveNumTiles( int num){

        boxCollider.enabled = false;

        for (int i=0; i < num; i++)
        {
            moveToNextTile();

            yield return new WaitForSeconds(0.05f);
        }

        boxCollider.enabled = true;


    }

	

}
