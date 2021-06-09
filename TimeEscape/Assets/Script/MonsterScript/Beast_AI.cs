using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast_AI : Monster
{

    private bool isCamera = false;
    private int Damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(isCamera);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Wall")
        {

            Debug.Log("trigger");
            isCamera = false;
            //reversetime = 0f;
            //speed *= -1;
            //dir *= -1;

        }
        if (collision.tag == "MainCamera")
        {
            Debug.Log("Camera");
            isCamera = true;
        }
        if (collision.gameObject.CompareTag("Attack"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DataManager.instance.AttackToPlayer(Damage);
            //collision.gameObject.GetComponent<PlayerController>().isHurt = true;

            //gameObject.SetActive(false);
            //Destroy(gameObject, 2);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            DataManager.instance.AttackToPlayer(Damage);
            //collision.gameObject.GetComponent<PlayerController>().isHurt = true;

            //gameObject.SetActive(false);
            //Destroy(gameObject, 2);
        }
    }
}
