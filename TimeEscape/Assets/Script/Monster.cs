using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;
    //private float reversetime = 0f;
    //private int dir = 1;
    //private BoxCollider2D MonsterCollider;
    public Rigidbody2D MonsterRigidbody;
    public GameObject targetPosition;
    public SpriteRenderer spriteRenderer;
    private float monsterscale;
    //private bool isCamera = false;

    // Start is called before the first frame update
    void Start()
    {
        //MonsterCollider = GetComponent<BoxCollider2D>();
        MonsterRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //targetPosition = FindObjectOfType<PlayerController>();

    }

    public void Move(bool isCamera)
    {
        targetPosition = GameObject.Find("Player");
        if (isCamera)
        {
            
            Vector2 playervector = new Vector2(targetPosition.transform.position.x, gameObject.transform.position.y);
            transform.position = Vector2.MoveTowards(gameObject.transform.position, playervector, 0.005f * speed);

            if (targetPosition.transform.position.x > gameObject.transform.position.x)
            {

                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        /*else
        {
            MonsterRigidbody.velocity = new Vector2(speed, 0);
        }*/
    }
    public void Jump(bool isGround)
    {
        if (isGround)
        {
            MonsterRigidbody.AddForce(new Vector2(0, speed * 20));

        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }


}

