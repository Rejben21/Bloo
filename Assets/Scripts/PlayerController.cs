using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed, jumpForce;
    public Rigidbody2D RGB;

    public bool isGrounded;
    public Transform groundPoint;
    public LayerMask whatIsGround;
    public GameObject landParticle;
    public GameObject glueLandParticle;
    public GameObject bubblesParticle;

    public GameObject appearNormal, appearRock, appearAir, appearMetal, appearGlue;

    private bool hasDisplayed = false;

    private SpriteRenderer SR;
    public Animator anim;

    public float knockBackLenght, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    public bool isMetal, isRock, isAir, isGlue, isNormal;

    private bool hasAppeared = true;

    public int whatIsPlayer = 1;
    public int whatIsPlayerMax;
    public bool changePlayer;

    public bool isInWater;
    public bool canSwim;
    private bool hasBubbled;

    public bool action = false;

    //normal properties:
    private float normalMass = 2f, normalGravity = 5f, normalSpeed = 8f, normalJump = 15f;

    //rock properties:
    private float rockMass = 3f, rockGravity = 7f, rockSpeed = 6.5f, rockJump = 15f;

    //metal properties:
    private float metalMass = 2f, metalGravity = 6f, metalSpeed = 7f, metalJump = 15f;

    //glue properties:
    private float glueMass = 2f, glueGravity = 5f, glueSpeed = 6f, glueJump = 15f;

    //air properties:
    private float airMass = 1f, airGravity = 1f, airSpeed = 5f, airJump = 8f;

    //in water properties:
    //normal properties:
    private float normalWaterSpeed = 4f, normalWaterJump = 9f;

    //rock properties:
    private float rockWaterSpeed = 5.5f, rockWaterJump = 15f;

    //metal properties:
    private float metalWaterSpeed = 4.5f, metalWaterJump = 15f;

    //glue properties:
    private float glueWaterSpeed = 4f, glueWaterJump = 9f;

    //air properties:
    private float airWaterSpeed = 5f, airWaterJump = 9f;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SR = GetComponent<SpriteRenderer>();
        hasBubbled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (isNormal)
            {
                IsNormal();
            }
            else if(isRock)
            {
                IsRock();
            }
            else if(isMetal)
            {
                IsMetal();
            }
            else if(isAir)
            {
                IsAir();
            }
            else if(isGlue)
            {
                IsGlue();
            }
            
            anim.SetBool("isRock", isRock);
            anim.SetBool("isNormal", isNormal);
            anim.SetBool("isMetal", isMetal);
            anim.SetBool("isAir", isAir);
            anim.SetBool("isGlue", isGlue);
            anim.SetBool("isInWater", isInWater);

            ChangePlayer();
        }

        DisplayWaterParticle();

        UpdatePlayer();
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLenght;
        RGB.velocity = new Vector2(0, knockBackForce);

        anim.SetTrigger("isHurt");
    }
    public void Bounce()
    {
        RGB.velocity = new Vector2(RGB.velocity.x, bounceForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }

    public void ChangePlayer()
    {
        if (changePlayer)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                whatIsPlayer++;
                hasAppeared = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                whatIsPlayer--;
                hasAppeared = false;
            }

            if (whatIsPlayer <= 0)
            {
                whatIsPlayer = whatIsPlayerMax;
            }
            if (whatIsPlayer > whatIsPlayerMax)
            {
                whatIsPlayer = 1;
            }
        }
    }

    public void IsNormal()
    {
        if(isInWater)
        {
            moveSpeed = normalWaterSpeed;
            jumpForce = normalWaterJump;
        }
        else
        {
            moveSpeed = normalSpeed;
            jumpForce = normalJump;
        }

        RGB.mass = normalMass;
        RGB.gravityScale = normalGravity;

        if (!hasAppeared)
        {
            Instantiate(appearNormal, transform.position, Quaternion.identity);
            hasAppeared = true;
        }

        if (knockBackCounter <= 0)
        {

            RGB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), RGB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                RGB.velocity = new Vector2(RGB.velocity.x, jumpForce);
            }

            if (RGB.velocity.x < 0)
            {
                SR.flipX = true;
            }
            else if (RGB.velocity.x > 0)
            {
                SR.flipX = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!SR.flipX)
            {
                RGB.velocity = new Vector2(-knockBackForce, RGB.velocity.y);
            }
            else
            {
                RGB.velocity = new Vector2(knockBackForce, RGB.velocity.y);
            }
        }

        anim.SetFloat("speed", Mathf.Abs(RGB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            if (hasDisplayed == false && !isInWater)
            {
                Instantiate(landParticle, groundPoint.position, Quaternion.identity);
                hasDisplayed = true;
            }
        }
        else
        {
            hasDisplayed = false;
        }

        if (transform.position == CheckpointController.instance.spawnPoint)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void IsRock()
    {
        if (isInWater)
        {
            moveSpeed = rockWaterSpeed;
            jumpForce = rockWaterJump;
        }
        else
        {
            moveSpeed = rockSpeed;
            jumpForce = rockJump;
        }

        RGB.mass = rockMass;
        RGB.gravityScale = rockGravity;

        if (!hasAppeared)
        {
            Instantiate(appearRock, transform.position, Quaternion.identity);
            hasAppeared = true;
        }

        if (knockBackCounter <= 0)
        {

            RGB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), RGB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                RGB.velocity = new Vector2(RGB.velocity.x, jumpForce);
            }

            if (RGB.velocity.x < 0)
            {
                SR.flipX = true;
            }
            else if (RGB.velocity.x > 0)
            {
                SR.flipX = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!SR.flipX)
            {
                RGB.velocity = new Vector2(-knockBackForce, RGB.velocity.y);
            }
            else
            {
                RGB.velocity = new Vector2(knockBackForce, RGB.velocity.y);
            }
        }

        anim.SetFloat("speed", Mathf.Abs(RGB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        /*if (isGrounded)
        {
            if (hasDisplayed == false)
            {
                Instantiate(landParticle, groundPoint.position, Quaternion.identity);
                hasDisplayed = true;
            }
        }
        else
        {
            hasDisplayed = false;
        }*/

        if (transform.position == CheckpointController.instance.spawnPoint)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void IsMetal()
    {
        if (isInWater)
        {
            moveSpeed = metalWaterSpeed;
            jumpForce = metalWaterJump;
        }
        else
        {
            moveSpeed = metalSpeed;
            jumpForce = metalJump;
        }

        RGB.mass = metalMass;
        RGB.gravityScale = metalGravity;

        if (!hasAppeared)
        {
            Instantiate(appearMetal, transform.position, Quaternion.identity);
            hasAppeared = true;
        }

        if (knockBackCounter <= 0)
        {

            RGB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), RGB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                RGB.velocity = new Vector2(RGB.velocity.x, jumpForce);
            }

            if (RGB.velocity.x < 0)
            {
                SR.flipX = true;
            }
            else if (RGB.velocity.x > 0)
            {
                SR.flipX = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!SR.flipX)
            {
                RGB.velocity = new Vector2(-knockBackForce, RGB.velocity.y);
            }
            else
            {
                RGB.velocity = new Vector2(knockBackForce, RGB.velocity.y);
            }
        }

        anim.SetFloat("speed", Mathf.Abs(RGB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        /*if (isGrounded)
        {
            if (hasDisplayed == false)
            {
                Instantiate(landParticle, groundPoint.position, Quaternion.identity);
                hasDisplayed = true;
            }
        }
        else
        {
            hasDisplayed = false;
        }*/

        if (transform.position == CheckpointController.instance.spawnPoint)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void IsGlue()
    {
        if (isInWater)
        {
            moveSpeed = glueWaterSpeed;
            jumpForce = glueWaterJump;
        }
        else
        {
            moveSpeed = glueSpeed;
            jumpForce = glueJump;
        }

        RGB.mass = glueMass;
        RGB.gravityScale = glueGravity;

        if (!hasAppeared)
        {
            Instantiate(appearGlue, transform.position, Quaternion.identity);
            hasAppeared = true;
        }

        if (knockBackCounter <= 0)
        {

            RGB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), RGB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                RGB.velocity = new Vector2(RGB.velocity.x, jumpForce);
            }

            if (RGB.velocity.x < 0)
            {
                SR.flipX = true;
            }
            else if (RGB.velocity.x > 0)
            {
                SR.flipX = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!SR.flipX)
            {
                RGB.velocity = new Vector2(-knockBackForce, RGB.velocity.y);
            }
            else
            {
                RGB.velocity = new Vector2(knockBackForce, RGB.velocity.y);
            }
        }

        anim.SetFloat("speed", Mathf.Abs(RGB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        if (isGrounded)
        {
            if (hasDisplayed == false && !isInWater)
            {
                Instantiate(glueLandParticle, groundPoint.position, Quaternion.identity);
                hasDisplayed = true;
            }
        }
        else
        {
            hasDisplayed = false;
        }

        if (transform.position == CheckpointController.instance.spawnPoint)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void IsAir()
    {
        if (isInWater)
        {
            moveSpeed = airWaterSpeed;
            jumpForce = airWaterJump;
        }
        else
        {
            moveSpeed = airSpeed;
            jumpForce = airJump;
        }

        RGB.mass = airMass;
        RGB.gravityScale = airGravity;

        if (!hasAppeared)
        {
            Instantiate(appearAir, transform.position, Quaternion.identity);
            hasAppeared = true;
        }

        if (knockBackCounter <= 0)
        {

            RGB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), RGB.velocity.y);

            isGrounded = Physics2D.OverlapCircle(groundPoint.position, 0.2f, whatIsGround);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                RGB.velocity = new Vector2(RGB.velocity.x, jumpForce);
            }

            if (RGB.velocity.x < 0)
            {
                SR.flipX = true;
            }
            else if (RGB.velocity.x > 0)
            {
                SR.flipX = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!SR.flipX)
            {
                RGB.velocity = new Vector2(-knockBackForce, RGB.velocity.y);
            }
            else
            {
                RGB.velocity = new Vector2(knockBackForce, RGB.velocity.y);
            }
        }

        anim.SetFloat("speed", Mathf.Abs(RGB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);

        /*if (isGrounded)
        {
            if (hasDisplayed == false)
            {
                Instantiate(landParticle, groundPoint.position, Quaternion.identity);
                hasDisplayed = true;
            }
        }
        else
        {
            hasDisplayed = false;
        }*/

        if (transform.position == CheckpointController.instance.spawnPoint)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void UpdatePlayer()
    {
        switch(whatIsPlayer)
        {
            case 5:
                isNormal = false;
                isRock = false;
                isAir = false;
                isMetal = false;
                isGlue = true;

                break;

            case 4:
                isNormal = false;
                isRock = false;
                isAir = false;
                isGlue = false;
                isMetal = true;

                break;

            case 3:
                isNormal = false;
                isRock = false;
                isMetal = false;
                isGlue = false;
                isAir = true;

                break;

            case 2:
                isNormal = false;
                isAir = false;
                isMetal = false;
                isGlue = false;
                isRock = true;

                break;

            case 1:
                isRock = false;
                isAir = false;
                isMetal = false;
                isGlue = false;
                isNormal = true;

                break;
        }
    }

    public void DisplayWaterParticle()
    {
        if(isInWater)
        {
            if(!hasBubbled)
            {
                StartCoroutine(BubblesCo());
            }
        }
    }

    public IEnumerator BubblesCo()
    {
        Instantiate(bubblesParticle, transform.position, transform.rotation);
        hasBubbled = true;
        yield return new WaitForSeconds(0.05f);
        hasBubbled = false;
    }
}
