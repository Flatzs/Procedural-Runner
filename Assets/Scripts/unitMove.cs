using UnityEngine;
using System.Collections;

	

public class unitMove : MonoBehaviour {

	
	Animator anim; 

	int currentTile;

	CameraScript camScript;
	public TileMap tileMap;

    public TextMesh scoreText;
    BoxCollider boxCollider;

    Vector3 childPos;



    // 乁( ⁰͡  Ĺ̯ ⁰͡ ) ㄏ  乁( ⁰͡  Ĺ̯ ⁰͡ ) ㄏ  乁( ⁰͡  Ĺ̯ ⁰͡ ) ㄏ  乁( ⁰͡  Ĺ̯ ⁰͡ ) ㄏ  乁( ⁰͡  Ĺ̯ ⁰͡ ) ㄏ



    // Use this for initialization
    void Start () {

        // INITIALIZE THIS SHIT
        childPos = new Vector3(0f, 0f, 0f);
		currentTile = 0;
		camScript = Camera.main.GetComponent<CameraScript> ();
		anim = GetComponentInChildren<Animator> ();
        boxCollider = GetComponent<BoxCollider>();
	}




	public void moveToNextTile(){

        try // not every tile has a child
        {
            // get the moving platforms position
            Vector3 pos = tileMap.GetTileGO(currentTile + 1).GetComponentInChildren<movingTileScript>().GetPosition();

            //  check valid distance from player 
            float dist = pos.z - transform.position.z;
            
            // If the player missed the jump
            if (dist > 3.0f)
            {
                // game over
                tileMap.ResetGame();
            }
               
        }
        catch
        {
            //just dont break for now please
        }

        // Position of where player is going next
        Vector3 tilePos = tileMap.GetTilePos (currentTile + 1);

		// move player unit to next tile
		transform.position = new Vector3 (tilePos.x, tilePos.y + 1f, tilePos.z);

        // move animation (jump for now)
        // TODO: lerp position and rotate box
        anim.SetTrigger("MoveZ");

        // Jump camera to player
        camScript.MoveToTile (transform.position);

        // we moved a tile!
		currentTile++;

        // Spawn 1 tile everytime the player moves
		tileMap.SpawnTile (1);
	}


    // For speed power ups (moves without collision)
    IEnumerator moveNumTiles(int num)
    {
        // turn collider off before move
        boxCollider.enabled = false;

        // move designated number of tiles
        for (int i = 0; i < num; i++)
        {
            moveToNextTile();

            // delay between each tile move
            yield return new WaitForSeconds(0.05f);
        }

        // turn collider back on ( player back in danger)
        boxCollider.enabled = true;


    }



    public void moveToTile( int num ){

		Vector3 tilePos = tileMap.GetTilePos (num);

        // move player unit to next tile
        transform.position = new Vector3 (tilePos.x, tilePos.y + 1f, tilePos.z);

        // move animation
        anim.SetTrigger("MoveZ");

        // Jump camera to player
        // TODO: lerp pos to unit (smoothing)
        camScript.MoveToTile (transform.position);

		currentTile = num;

	}

 

    /// Update runs EVERY SINGLE FRAME
	void Update(){

        // When user clicks the  left mouse or 'X' on a controller
		if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")){

			moveToNextTile ();
            scoreText.text = currentTile.ToString();
		}
        else // update tasks todo when player isnt moving to next tile
        {
            if (tileMap.isMovingtile(currentTile))
            {
                
                Vector3 pos = tileMap.GetTileGO(currentTile).GetComponentInChildren<movingTileScript>().GetPosition();
                transform.position = new Vector3(pos.x, pos.y + 1, pos.z);
            }
        }
    }

    
    public int GetCurrentTile(){

		return currentTile;
	}

	public void UnitPosToTileList( int n ){

		currentTile = n;
	}
		

	void OnTriggerEnter( Collider col){

        // layer 9 = hazard tile
        if (col.gameObject.layer == 9)
            
		    tileMap.ResetGame ();
	}

    public void OnSpeedUp()
    {

        // called from speed up tiles
        StartCoroutine(moveNumTiles(13));

        // prevent player from landing on hazard after
        // speed up
        while (tileMap.GetTileGO(currentTile).layer == 9)
        {
            moveToNextTile();
        }
    }

    

	

}
