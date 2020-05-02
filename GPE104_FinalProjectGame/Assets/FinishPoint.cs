using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public AudioClip winJingle;
    private float timer;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //set the game state to win
            GameManager.instance.currentState = GameManager.gameState.GameWin;
            //turn on the UI
            GameManager.instance.winLoseManager.SetActive(true);
            //load the win screen
            GameManager.instance.LoadLevel("Win Screen");
        }
    }
}
