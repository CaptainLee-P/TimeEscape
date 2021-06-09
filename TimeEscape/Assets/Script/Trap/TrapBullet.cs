using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D CapsuleCollider2D;
    //public GameObject Player;
    public float bulletSpeed;
    public int bulletDamage;
    private float dir;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        target = FindObjectOfType<PlayerController>().transform;
        if (target.position.x - gameObject.transform.parent.transform.position.x > 0)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

    }
    void Move()
    {
        Vector2 vector2 = new Vector2(bulletSpeed * dir, rigid.velocity.y);
        rigid.velocity = vector2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            //collision.gameObject.GetComponent<PlayerController>().OnDamage(bulletDamage);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            DataManager.instance.AttackToPlayer(bulletDamage);
            //collision.gameObject.GetComponent<PlayerController>().isHurt = true;

            gameObject.SetActive(false);
            Destroy(gameObject,2);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
           
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            DataManager.instance.AttackToPlayer(bulletDamage);
         

            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }
    }
}
