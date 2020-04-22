using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //spawn point 
    public GameObject spawnPoint;
    public Vector3 spawnPointPosition;

    //player stuff
    public GameObject playerPrefab;
    public GameObject player;
    public Transform playerTransform;
    public int playerLives = 3;
    public float playerScore;
    public float playerSingleAmmo;
    public float playerShotgunAmmo;
    public float playerBFGAmmo;

    public static GameManager instance;

    public int currentScene;

    private void Awake()
    {
        if(instance == null)
        {
            //set this to be the gameobject
            instance = this;
            //set this to be DoNotDestroy
            DontDestroyOnLoad(gameObject);
        }
        //if there's already a game manager
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
