using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int scoreGive = 30;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.addScore(scoreGive);
            Player.obj.addLive();
            AudioManager.obj.playCoin();


            UIManager.obj.updateScore();
            UIManager.obj.updateLives();

            gameObject.SetActive(false);
        }
    }
}
