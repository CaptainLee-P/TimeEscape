using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidMan_AI : Monster
{
    //float rot=0f;
    //private bool isAttack=false;
    private bool isCamera = false;
    private Animator animator;
    public HitBox hitbox;
    public GameObject Player;
    int Damage = 20;
    int damage = 50;
    int HP = 2;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hitbox.isAttack)
        {
            Move(isCamera);
        }
        else
        {/*
            rot += Time.deltaTime;
            transform.Rotate(new Vector3(rot, 0, 0));*/
            animator.SetBool("IsAttack", true);
                       
        } 
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Attack"))
        {

            StartCoroutine(GetDamage());
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {     

        if (collision.tag == "MainCamera")
        {
            Debug.Log("Camera");
            isCamera = true;
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
    public void AttackStart()
    {
        if (hitbox.isAttack)
        {
            DataManager.instance.AttackToPlayer(damage);
            Debug.Log("플레이어에게 피해를 줌");
        }

    }
    public void AttackEnd()
    {
        hitbox.isAttack = false;
        animator.SetBool("IsAttack", false);
    }
    IEnumerator GetDamage()
    {

        yield return new WaitForSeconds(2f);
        HP--;
        if (HP <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2);
        }
    }
}
