using UnityEngine;
using System.Collections;


public class tileMouse : MonoBehaviour {



	public void OnMouseUp(){

		TileMap tm = GameObject.Find ("TileMap").GetComponent<TileMap>();



		tm.SpawnTile (1);



	}
}
