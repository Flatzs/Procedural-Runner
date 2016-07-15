using UnityEngine;
using System.Collections;

public class waitSpeedUpTile : MonoBehaviour {

    public Renderer light1;
    public Renderer light2;
    public Renderer light3;
    public float SecDelay = 1;

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("player on speedup");
        light1.material.color = Color.green;

        StartCoroutine(CountDown());

    }

    void OnTriggerExit(Collider col)
    {
        Debug.Log("trigger exit");

        StopAllCoroutines();
    }

    IEnumerator CountDown()
    {
        light1.material.color = Color.green;
        yield return new WaitForSeconds(SecDelay / 2);

        light2.material.color = Color.green;
        yield return new WaitForSeconds(SecDelay / 2);

        light3.material.color = Color.green;

        GameObject.Find("PlayerUnit").GetComponent<unitMove>().OnSpeedUp();
    }

}
