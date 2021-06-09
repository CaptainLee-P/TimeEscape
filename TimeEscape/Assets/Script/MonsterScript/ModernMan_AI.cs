using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModernMan_AI : Monster
{

    public bool isCamera = false;
    public bool isGround = true;
    private int Damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        isCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move(isCamera);
        if (isCamera && isGround)
        {
            Jump(isGround);
            isGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "MainCamera")
        {
            Debug.Log("Camera");
            isCamera = true;
        }
        if (collision.tag == "Platform")
        {
            isGround = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Wall")
        {

            Debug.Log("trigger");
            isCamera = false;
            speed *= -1;
            if (speed < 0)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {


            DataManager.instance.AttackToPlayer(Damage);
            //collision.gameObject.GetComponent<PlayerController>().isHurt = true;

            //gameObject.SetActive(false);
            //Destroy(gameObject, 2);
        }

    }
}
