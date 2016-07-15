using UnityEngine;
using System.Collections;

public class randColorOnTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

}
