using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawnerSingle : MonoBehaviour
{
    public float spawnDelayTime;
    public GameObject GameObject;
    private bool onShot= true;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(spawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void Spawn()
    {
        GameObject bullet = Instantiate(GameObject, gameObject.transform.position, GameObject.transform.rotation);
        bullet.transform.parent = gameObject.transform;
        onShot = false;
        //StartCoroutine(spawnDelay());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (onShot)
            {
                Spawn();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (onShot)
            {
                Spawn();
            }
        }
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            onShot = true;
        }
    }*/
    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(spawnDelayTime);
        onShot = true;
    }

}
