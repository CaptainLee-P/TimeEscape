using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public GameObject targetPosition;
    public bool isAttack = false;
    

    //public bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition.transform.position.x > gameObject.transform.position.x)
        {
            transform.localPosition = new Vector2(1, 0f); ;
        }
        else
        {
            transform.localPosition = new Vector2(-1, 0f);
        }
    }

   /*private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {

            isAttack = false;
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {

            Debug.Log("HITBOX");
            StartCoroutine(Attack(collision));
            //isAttack = twrue;
            /*
            PlayerController playerController = collision.GetComponent<PlayerController>();
            
            if(playerController!=null)
            {
                playerController.OnDie();
            }*/
        }
    }

    IEnumerator Attack(Collider2D collision)
    {

        isAttack = true;
        yield return new WaitForSeconds(1f);
        //collision.gameObject.GetComponent<PlayerController>().OnDamage(Damage);
        Debug.Log("플레이어에게 피해를 줌");
        yield return new WaitForSeconds(2f);
        isAttack = false;

    }
}
