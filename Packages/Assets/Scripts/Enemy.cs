using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movHor = 0f;
    public float speed = 3f;

    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer;
    public float frontGrandRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGive = 50;

    private RaycastHit2D hit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Game.obj.gamePaused)
        {
           
           return;
        }

        isGroundFloor = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
        new Vector3(movHor, 0, 0), frontGrandRayDist, groundLayer);

        if (isGroundFloor)
            movHor = movHor * -1;

        //Choque con paredes
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
            movHor = movHor * -1;

        //Choque con otro enemigo
        hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z),
        new Vector3(movHor, 0, 0), frontDist
        );

        if (hit.collider != null)
            if (hit.transform != null)
                if (hit.transform.CompareTag("Enemy")) 
                    movHor = movHor * -1;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movHor * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Revisar si el jugador viene desde arriba
            if (collision.transform.position.y > transform.position.y + 0.2f)
            {
                getKilled();
                AudioManager.obj.playEnemyHit();
                // Rebote del jugador
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * 5f;
            }
            else
            {
                Player.obj.getDamage();
            }
        }
    }

    private void getKilled()
    {
        FXManager.obj.showPop(transform.position);
        gameObject.SetActive(false);
    }
}