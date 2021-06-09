using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stone : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    public float stoneSpeed;
    public float stoneRotate;

    private float stoneRayDist_x;
    private float stoneRayDist_y;
    private CircleCollider2D CircleCollider2D;
    //GameObject DestroyPosition;
    public LayerMask whatisPlatform;

    private int Damage = 30;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //DestroyPosition = transform.Find("DestroyPosition").gameObject;
        CircleCollider2D = GetComponent<CircleCollider2D>();

        //destroy 지점 
        stoneRayDist_x =
            transform.position.x + (transform.localScale.x * CircleCollider2D.radius);
        stoneRayDist_y = 
            transform.position.y + (transform.localScale.y * CircleCollider2D.radius);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector2 = new Vector2(stoneSpeed,0);
        Rigidbody2D.position += vector2;
        //돌 회전
        Vector3 vector3 = new Vector3(0, 0, stoneRotate);
        transform.Rotate(vector3);
        //CheckRay();
        if (transform.localScale.x >= 5)
        {
            Destroy();
        }
        
       
    }

    private void Destroy()
    {
        
        GameObject destroyPosition = gameObject.transform.Find("DestroyPosition").gameObject;
        Vector3 vector3 = new Vector3(destroyPosition.transform.localPosition.x, destroyPosition.transform.localPosition.y, destroyPosition.transform.localPosition.z);

        stoneRayDist_x =
            transform.position.x + (transform.localScale.x * CircleCollider2D.radius);
        stoneRayDist_y =
            transform.position.y + (transform.localScale.y * CircleCollider2D.radius);
        float checkDestroy_y = stoneRayDist_y - (transform.localScale.y * CircleCollider2D.radius * 2);
        for (float i = stoneRayDist_y; i > checkDestroy_y; i -= 1f)
        {

            //Debug.Log("x:" + transform.position.x + stoneRayDist_x + "y:" + i);
            // Debug.Log("x:" + GetComponent<CircleCollider2D>().transform.position.x  + "y:" + GetComponent<CircleCollider2D>().transform.position.x);
            Vector2 DestroyPosition = new Vector2(stoneRayDist_x, i);
            Collider2D overCollider2d = Physics2D.OverlapCircle(DestroyPosition, 1f, whatisPlatform);
            if (overCollider2d != null)
            {
                overCollider2d.transform.GetComponent<Bricks>().MakeDot(DestroyPosition);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.localScale.x < 5)
            {
                DataManager.instance.AttackToPlayer(Damage);
            }
            else
            {
                int DeathDamage = DataManager.instance.playerMaxHP;
                DataManager.instance.AttackToPlayer(DeathDamage);
            }
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (transform.localScale.x > 5)
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (transform.localScale.x < 5)
            {
                DataManager.instance.AttackToPlayer(Damage);
            }
            else
            {
                int DeathDamage = DataManager.instance.playerMaxHP;
                DataManager.instance.AttackToPlayer(DeathDamage);
            }
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (transform.localScale.x > 5)
            {
                Destroy(collision.gameObject);
            }
        }
    }


    private void CheckRay()
    {

        LayerMask layerMask = new LayerMask();
        layerMask = LayerMask.GetMask("Platform");
        Vector2 vector2 = new Vector2(transform.position.x + stoneRayDist_x, transform.position.y + stoneRayDist_y);

        //Collider2D overCollider2d = Physics2D.OverlapCircle(MousePosition, 0.01f, whatisPlatform);
        RaycastHit2D [] raycast = Physics2D.RaycastAll(vector2, new Vector2(0, -(transform.position.y*2)), 5f,layerMask.value);
        Debug.DrawRay(vector2, new Vector3(0, -(transform.position.y * 2), 0), Color.red);
        for (int i = 0; i < raycast.Length; i++)
        {
            if (raycast[i].collider != null)
            {

                Debug.Log("x:" + transform.position.x + "y:" + transform.position.y);
            }
        }
        //for (int i = 0; i < raycast.Length; i++)
        //{
        //    RaycastHit2D hit2D = raycast[i];
        //    Vector3 pos = new Vector3(hit2D.transform.localPosition.x, hit2D.transform.localPosition.y, 0);
        //    Debug.Log("x:"+raycast.transform.localPosition.x + "y:"+raycast.transform.localPosition.y);
        //    hit2D.transform.GetComponent<Bricks>().MakeDot(pos);

        //    //hit2D.transform.GetComponent<Tilemap>().SetTile(new Vector3Int(hit2D.transform.position.x, hit2D.transform.position.y, 0), null);
        //}
    }
}
