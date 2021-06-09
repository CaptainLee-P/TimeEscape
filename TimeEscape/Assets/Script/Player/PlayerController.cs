using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float playerJumpSpeed;
    public float playerMoveSpeed;
    public bool onJump;
    public bool onWalk;
    public bool onAttack;
    public bool onSit;
    public bool isHurt;
    public bool isHurtknockback;

    private float sitBoxColliderSizeX = 2f;
    private float sitBoxColliderSizeY = 2.3f;
    private float IdleCapsuleColliderSizeX;
    private float IdleCapsuleColliderSizeY;
    public float knockBackPower;
    public int HP = 100;
    public int MaxHP = 100;
    //데미지 텍스트
    public GameObject hudDamageText;
    public Transform hudPosition;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip attackSound;

    Rigidbody2D rid;
    private SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    // Start is called before the first frame update
    private void Awake()
    {
        
        rid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        //앉기 박스콜라이더 변경을 위한 값
        DataManager.instance.Save();
        DataManager.instance.stageDataUpdate();
        IdleCapsuleColliderSizeX = transform.GetComponent<CapsuleCollider2D>().size.x;
        IdleCapsuleColliderSizeY = transform.GetComponent<CapsuleCollider2D>().size.y;

    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && !onSit && !onJump) {
            audioSource.PlayOneShot(jumpSound);
            Jump(); }
        Sit();
        Attack();
        SitUp();
        CheckPlatform(); 

        

        //MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
    private void FixedUpdate()
    {
        Move();
    }
    void Jump()
    {

        //Vector2 vector2 = new Vector2(0, playerJumpSpeed);
        rid.AddForce(Vector2.up * playerJumpSpeed, ForceMode2D.Impulse);
        onJump = true;
        anim.SetBool("isWalk", false);
        anim.SetBool("isJump", true);
    }
    void Move()
    {
        
        //방향 전환 flip
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            //spriteRenderer.flipX = true;
            transform.localScale = (new Vector3(-1, 1, 1));

            onWalk = true;
            
            anim.SetBool("isWalk", true);
            anim.SetFloat("whichDirection", h);
        }
        else if (h == 0)
        {
            onWalk = false;
            anim.SetBool("isWalk", false);
            
        }
        else
        {
            //spriteRenderer.flipX = false;
            transform.localScale = (new Vector3(1, 1, 1));

            onWalk = true;
            anim.SetBool("isWalk", true);
            anim.SetFloat("whichDirection", h);
        }
        //앉기상태X   물리력X    좌우이동 
        if (!onSit)
        {
            Vector2 vector2 = new Vector2(h * playerMoveSpeed, rid.velocity.y);
            rid.velocity = vector2;
            //rid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        }
    }
    void Attack()
    {
        if (Input.GetButton("Fire1") && !onAttack)
        {
            audioSource.PlayOneShot(attackSound);
            onAttack = true;
            StartCoroutine(AttackDelay());
            if (onSit)
            {
                anim.SetBool("isSitAttack", true);
            }
            else if (onJump)
            {
                anim.SetBool("isJumpAttack", true);
            }
            else
            {
                anim.SetBool("isAttack", true);
            }

            //1초동안 attack 오브젝트 생성 -> 추후 수정
        }
    }
    void AttackStart()
    {

            onAttack = true;
        if (onSit)
        {
            anim.SetBool("isSitAttack", true);
            GameObject attack = transform.Find("Attack").gameObject;
            attack.SetActive(true);
        }
        else if (onJump)
        {
            anim.SetBool("isAttack", true);
            GameObject attackJump = transform.Find("AttackJump").gameObject;
            attackJump.SetActive(true);
        }
        else
        {
         
            anim.SetBool("isJumpAttack", true);
            GameObject attackStand = transform.Find("AttackStand").gameObject;
            attackStand.SetActive(true);
        }


            //1초동안 attack 오브젝트 생성 -> 추후 수정
            //StartCoroutine(AttackDelay());
        
    }
    void AttackEnd()
    {
        GameObject attack = transform.Find("Attack").gameObject;
        attack.SetActive(false);
        GameObject attackStand = transform.Find("AttackStand").gameObject;
        attackStand.SetActive(false);
        GameObject attackJump = transform.Find("AttackJump").gameObject;
        attackJump.SetActive(false);
        anim.SetBool("isAttack", false);
        anim.SetBool("isSitAttack", false);
        anim.SetBool("isJumpAttack", false);
    }
    void Sit()
    {
        if (!onJump && Input.GetButton("Vertical"))
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                if (!onSit)
                {
                    //앉기 공중에서 뜨는 좌표 수정
                    Vector2 SitPosition = new Vector2(transform.position.x, transform.position.y-1);
                    transform.position = SitPosition;
                }
                //앉기시 정지
                Vector2 SitStopVector = new Vector2(rid.velocity.normalized.x * 0.1f, rid.velocity.y);
                rid.velocity = SitStopVector;
                onWalk = false;
                onSit = true;

                rid.GetComponent<Animator>().SetBool("isSit", true);
                //capsuleCollider.direction = CapsuleDirection2D.Horizontal;
                //transform.GetComponent<CapsuleCollider2D>().size = vector2;

                //캡슐-> 박스 콜라이더로 전환
                Vector2 colliderSize = new Vector2(sitBoxColliderSizeX,sitBoxColliderSizeY );
                boxCollider.enabled = true;
                boxCollider.size = colliderSize;
                capsuleCollider.enabled = false;
                //transform.position = new Vector2(transform.localPosition.x, transform.localPosition.y);
                GameObject attack = transform.Find("Attack").gameObject;
                attack.transform.localPosition = new Vector3(attack.transform.localPosition.x, -0.3f, attack.transform.localPosition.z);
            }

        }
    }
    void SitUp()
    {
        if (Input.GetAxisRaw("Vertical") >= 0)
        {
            rid.GetComponent<Animator>().SetBool("isSit", false);
            //박스 -> 캡슐 콜라이더 전환
            Vector2 idleCollider = new Vector2(IdleCapsuleColliderSizeX, IdleCapsuleColliderSizeY);
            transform.GetComponent<CapsuleCollider2D>().size = idleCollider;
            boxCollider.enabled = false;
            capsuleCollider.enabled = true;
            //capsuleCollider.direction = CapsuleDirection2D.Vertical;
            // capsuleCollider.enabled;
            if (onSit)
            {
                //idle 전환시 좌표끼임 방지 X좌표 +1
                Vector2 vector2 = new Vector2(transform.localPosition.x, transform.localPosition.y + 1);
                transform.position = vector2;
            }
            GameObject attack = transform.Find("Attack").gameObject;
            attack.transform.localPosition = new Vector3(attack.transform.localPosition.x, 0.5f, attack.transform.localPosition.z);
            onSit = false;
        }
    }
    public void OnDie()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        spriteRenderer.flipY = true;

        capsuleCollider.enabled = false;

    }
    void Revival()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);

        spriteRenderer.flipY = false;

        capsuleCollider.enabled = true;
        HP = MaxHP;
    }
    void Status()
    {
        if (HP <= 0)
        {
            OnDie();
            //GameManager.instance.returnStage();
            if (GameManager.instance.lifePoint >= 0)
            {
                Revival();
            }
        }
    }

    void CheckPlatform()
    {
        if (rid.velocity.y < 0)
        {
            anim.SetBool("isFall", true);
            //player좌표 기준 아래 방향으로 레이
            RaycastHit2D rayHit = Physics2D.Raycast(rid.position, Vector3.down, rid.position.x+1, LayerMask.GetMask("Platform"));
            Debug.DrawRay(rid.position, Vector3.down, Color.red);
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 2.5f)
                {
                    anim.SetBool("isFall", false);
                    onJump = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            //onJump = false;
            //Debug.Log("JUMPFALSE");
        }
        if (collision.gameObject.CompareTag("Monster"))
        {
            //onJump = false;
            if (!isHurt)
            {
                Hurt(collision.transform.position);
            }

            
        }
        if (collision.gameObject.CompareTag("SmallStone"))
        {
            //onJump = false;
            if (!isHurt)
            {
                Hurt(collision.transform.position);
            }
        }
        if (rid.velocity.y < 1)
        {
            onJump = false;
            anim.SetBool("isJump",false);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            if (!isHurt)
            {
                Hurt(collision.transform.position);
            }
        }
        if (collision.gameObject.CompareTag("SmallStone"))
        {
            if (!isHurt)
            {
                Hurt(collision.transform.position);
            }
        }
        if (!onJump)
        {
            onJump = false;
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            //
            //GameManager.instance.stagePoint += 100;

            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "finish")
        {
            //다음 스테이지
        }
        if (collision.CompareTag("Trap"))
        {
            if (!isHurt)
            {
                Hurt(collision.transform.position);
            }
        }
        if (collision.CompareTag("DeathZone"))
        {
            DataManager.instance.PlayerDeathZone();
        }
    }

 
    public void DamageText(int Damage)
    {

        //데미지 텍스트 출력
        GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPosition.transform.position; // 표시될 위치
        hudText.GetComponent<DamageText>().damage = Damage; // 데미지 전달

    }
    void Hurt(Vector2 pos)
    {
        isHurt = true;
        DamageText(DataManager.instance.currentDamage);
        if (HP <= 0)
        {
            Status();

        }
        else
        {
            audioSource.PlayOneShot(damageSound);
            float x = transform.position.x - pos.x;
            if (x < 0)
                x = 1;
            else
                x = -1;

            StartCoroutine(Knockback(x));
            StartCoroutine(Alphablink());
            StartCoroutine(HurtRoutine());
        }
        //넉백시 뒤로 밀림
        float h = Input.GetAxis("Horizontal");
        
        Vector2 vector2 = new Vector2((h) * knockBackPower, knockBackPower);

        rid.AddForce(vector2);
    }

    //void Hurt(float x)
    //{
    //    isHurt = true;

    //    if (HP <= 0)
    //    {
    //        Status();
    //    }
    //    else
    //    {
    //        StartCoroutine(Knockback(x));
    //        StartCoroutine(alphablink());
    //        StartCoroutine(HurtRoutine());
    //    }
    //    float h = Input.GetAxis("Horizontal");

    //    Vector2 vector2 = new Vector2((h) * knockBackPower, knockBackPower);

    //    rid.AddForce(vector2);
    //}
    IEnumerator Knockback(float dir)
    {
        isHurtknockback = true;
        float ctime = 0;
        while (ctime < 0.2f)
        {
            if (transform.rotation.y == 0)
                transform.Translate(Vector2.left * knockBackPower * Time.deltaTime * dir);
            else
                transform.Translate(Vector2.left * knockBackPower * Time.deltaTime * -1f * dir);
            ctime += Time.deltaTime;
            yield return null;
        }
        isHurtknockback = false;
    }
    IEnumerator HurtRoutine()
    { 
        yield return new WaitForSeconds(1f);
        isHurt = false;
        //isDamage = false;
    }
    IEnumerator Alphablink()
    {
        //캐릭터 깜박깜박 
        while (isHurt) {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1); ;
        }
    }
    IEnumerator AttackDelay()
    {
        if (onAttack)
        {
            yield return new WaitForSeconds(1f);
            transform.Find("Attack").gameObject.SetActive(false);
            onAttack = false;
            GameObject attackStand = transform.Find("AttackStand").gameObject;
            attackStand.SetActive(false);
            GameObject attackJump = transform.Find("AttackJump").gameObject;
            attackJump.SetActive(false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isSitAttack", false);
            anim.SetBool("isJumpAttack", false);
            //Debug.Log("attackDelay");
        }
    }
}

//레이캐스트 블록파괴 Fail
//RaycastHit2D rayHit = Physics2D.Raycast(rid.position, Vector3.down, 4, LayerMask.GetMask("Platform"));
//Vector3 MousePosition = rayHit.transform.position;
//Collider2D overCollider2d = Physics2D.OverlapCircle(MousePosition, 0.01f, LayerMask.GetMask("Platform"));
//if (overCollider2d != null)
//{
//    overCollider2d.transform.GetComponent<Bricks>().MakeDot(MousePosition);
//}
//Debug.Log(MousePosition);