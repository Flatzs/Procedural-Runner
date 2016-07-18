using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform playerUnit;

    void Update()
    {
        //transform.position = new Vector3(playerUnit.position.x + 9f,
        //								 playerUnit.position.y + 10f,
        //								 playerUnit.position.z - 6f
        //								 );

    }

    public void MoveToTile( Vector3 tilePos){

        transform.position = new Vector3(tilePos.x + 9f,
                                            tilePos.y + 10f,
                                            tilePos.z - 6f
                                         );


    }
}
