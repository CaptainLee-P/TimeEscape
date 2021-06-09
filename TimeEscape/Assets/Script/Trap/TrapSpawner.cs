using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public float spawnDelayTime;
    public GameObject GameObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void Spawn()
    {
        GameObject bullet = Instantiate(GameObject, gameObject.transform.position, GameObject.transform.rotation);
        bullet.transform.parent = gameObject.transform;
        StartCoroutine(spawnDelay());
    }
    
    IEnumerator spawnDelay()
    {
        yield return new WaitForSeconds(spawnDelayTime);
        Spawn();
    }
}
