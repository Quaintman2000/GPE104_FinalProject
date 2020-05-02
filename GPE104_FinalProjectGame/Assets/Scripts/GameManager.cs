using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //spawn point 
    public GameObject spawnPoint;

    //player stuff
    public GameObject playerPrefab;
    public GameObject player;
    public PlatformerController playerController;
    public Transform playerTransform;
    public int playerLives;

    //User interface
    public GameObject[] lives;

    public static GameManager instance;

    public Transform cameraTransform;

    public int currentLevel;
    public enum gameState { GameLose, GameWin, StartScreen, GameRunning };
    public gameState currentState;
    public GameObject winLoseManager;
    public Text titleText;
    public Button buttonOne;
    public Text buttonOneText;
    public Button quitButton;
    public Image backgroundImage;
    public Image ammoPreview;
    public Sprite bullet;
    public Sprite shotgun;
    public Sprite bigBullet;
    public Button startButton;
    public Sprite lvl1Background;

    public AudioClip lvl1Music;
    public AudioClip lvl2Music;
    public AudioClip StartMusic;


    private void Awake()
    {
        if (instance == null)
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
    private void Start()
    {

        //grab the player stats
        playerController = player.GetComponent<PlatformerController>();
        playerTransform = player.transform;

        // spawnPoint = GameObject.Find("SpawnPoint");
        currentState = gameState.StartScreen;
        titleText = GameObject.Find("ScreenText").GetComponent<Text>();

        RestartGame();

    }
    // Update is called once per frame
    void Update()
    {


        if (playerLives == 0)
        {
            //game over

            winLoseManager.SetActive(true);
        }

        //if the gamestate = lose
        if (currentState == gameState.GameLose)
        {
            //have a red game over title
            titleText.text = "Game Over";
            titleText.color = Color.red;

            //disable the start button
            startButton.gameObject.SetActive(false);
            buttonOne.gameObject.SetActive(true);

            //turn music off
        }
        //if the gamestate = win
        else if (currentState == gameState.GameWin)
        {
            //have a green you win title
            titleText.text = "You Win!";
            titleText.color = Color.green;

            //disable the start button
            startButton.gameObject.SetActive(false);
            buttonOne.gameObject.SetActive(true);

            //turn music off
        }
        //if the game state = startscreen
        else if (currentState == gameState.StartScreen)
        {
            //turn on ui
            winLoseManager.SetActive(true);
            titleText.text = "Space Shooters";
            //have the start button enabled
            startButton.gameObject.SetActive(true);
            buttonOne.gameObject.SetActive(false);

        }
        //if the game is running
        else if (currentState == gameState.GameRunning)
        {
            //close the ui
            winLoseManager.SetActive(false);
            //if there is no player on the scene
            if (player == null && playerLives != 0)
            {
                Respawn();
            }
            cameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, cameraTransform.position.z);


        }
    }

    /// <summary>
    /// Load a specific level by number
    /// </summary>
    /// <param name="sceneNum">the number of the desired scene to load</param>
    public void LoadLevel(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
        if (currentLevel == 1)
        {
            audioManager.audioManagerInstance.PlaySound(lvl1Music);
        }
        else
        {
            audioManager.audioManagerInstance.StopMusic();
        }
    }
    /// <summary>
    /// load a specific level by name
    /// </summary>
    /// <param name="sceneName">name of the desired scene to load</param>
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        audioManager.audioManagerInstance.StopMusic();
    }
    /// <summary>
    /// load the next scene
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene((currentLevel += 1));
    }
    /// <summary>
    /// Reloads the previous level they were on
    /// </summary>
    public void Retry()
    {
        //reload the level they were on
        LoadLevel(currentLevel);

    }
    public void Quit()
    {
        Application.Quit();
    }
    /// <summary>
    /// Goes back to start screen and resets everything
    /// </summary>
    public void RestartGame()
    {
        //reset lives
        playerLives = 3;
        //set game state to start screen
        currentState = gameState.StartScreen;
        //set the level = 1
        currentLevel = 1;
        audioManager.audioManagerInstance.PlaySound(StartMusic);
    }
    /// <summary>
    /// starts the game
    /// </summary>
    public void StartGame()
    {
        //set game state to null
        currentState = gameState.GameRunning;
        //set current level to 1
        currentLevel = 1;
        //load level 1 scene
        LoadLevel(currentLevel);
        //reset lives
        playerLives = 3;
        for (int i = 0; i < 3; i++)
        {
            lives[i].gameObject.SetActive(true);
        }
        Respawn();
    }
    /// <summary>
    /// respawns the player the spawnpoint or checkpoint
    /// </summary>
    public void Respawn()
    {
        //spawn the player at the spawnpoint position and direction
        Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

    }
}
