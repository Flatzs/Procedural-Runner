using UnityEngine;
using System.Collections;
using UnityEngine.WSA;

public class CameraScript : MonoBehaviour {

	public void MoveToTile( Vector3 tilePos){

		transform.position = new Vector3 (	tilePos.x + 9f,
											10,
											tilePos.z - 6f
										 );


	}
}
