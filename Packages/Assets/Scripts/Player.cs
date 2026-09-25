using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 3;

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isInmune = false;

    public float speed = 5f;
    public float jumpForce = 3f;
    public float movHor;

    public float inuneTimeCnt = 0f;
    public float InmuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    private void Awake()
    {
        obj = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        if (Game.obj.gamePaused)
        {
           movHor = 0f;
           return;
        }
        movHor = Input.GetAxisRaw("Horizontal");
        isMoving = (movHor != 0f);
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

            if (isInmune)
        {   
            spr.enabled = !spr.enabled;

            inuneTimeCnt -= Time.deltaTime;

            if (inuneTimeCnt <= 0)
            {
                isInmune = false;
                spr.enabled = true;
            }
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);

        flip(movHor);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movHor * speed, rb.linearVelocity.y);
    }


    private void goingInmune()
    {
        isInmune = true;
        inuneTimeCnt = InmuneTime;
    }

    public void jump()
    {
        if (!isGrounded) return;
            AudioManager.obj.playJump();

        
        rb.linearVelocity = Vector2.up * jumpForce;
    }

    private void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue < 0)
        {
            theScale.x = Mathf.Abs(theScale.x) * -1;
        }
        else if (_xValue > 0)
        {
            theScale.x = Mathf.Abs(theScale.x);
        }
        transform.localScale = theScale;
    }

    public void getDamage()
    {
        lives--;

        AudioManager.obj.playHit();
        UIManager.obj.updateLives();

        goingInmune();

        if (lives <= 0)
        {
            FXManager.obj.showPop(transform.position);
            AudioManager.obj.playEnemyHit();
            Game.obj.gameOver();
        }
    }

    public void addLive()
    {
        lives++;

        if(lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }

    void OnDestroy()
    {
        obj = null;
    }
}