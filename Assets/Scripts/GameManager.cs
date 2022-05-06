using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private float spawnDelay = 1;
    [SerializeField]
    private float spawnInterval = 2f;
    private int odometerValue;
    private PlayerController playerControllerScript;
    private bool isHighScore;
   
    public Text odometerText;
    public Text bestDistanceText;
    public GameObject[] objectPrefabs;
    public GameObject gameOverScreen;
    public GameObject newHighScoreScreen;
    public InputField mainInputField;
    


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObject", spawnDelay, spawnInterval);
        StartCoroutine("Odometer");
        
        //Adds a listener that invokes the "LockInput" method when the player finishes editing the main input field.
        //Passes the main input field into the method when "LockInput" is invoked
        mainInputField.onEndEdit.AddListener(delegate { LockInput(mainInputField); });

        bestDistanceText.text = "Best distance: " + ScoreManager.Instance.playerName + " " + ScoreManager.Instance.playerScore;
        odometerText.text = "Distance: " + odometerValue;

    }

    private void Update()
    {
        if (playerControllerScript.gameOver)
        {
            GameOver();
        }

    }
    void SpawnObject()
    {
        Vector3 spawnLocation = new Vector3(30, Random.Range(-5, 15), 0);
        int index = Random.Range(0, objectPrefabs.Length);
        
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(objectPrefabs[index], spawnLocation, objectPrefabs[index].transform.rotation);
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveAndRestartGame()
    {
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene(1);
    }

    public void SaveAndBackToMenu()
    {
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene(0);
    }
    IEnumerator Odometer()
    {
        while (!playerControllerScript.gameOver)
        {
            odometerValue += 1;
            odometerText.text = "Distance: " + odometerValue;
            yield return new WaitForSeconds(1f);
        }
    }

    // Checks if there is anything entered into the input field.
    void LockInput(InputField input)
    {
        if (input.text.Length > 0)
        {
            Debug.Log("Text has been entered");
            ScoreManager.Instance.playerName = input.text;
        }
        else if (input.text.Length == 0)
        {
            Debug.Log("Main Input Empty");
        }
    }

    public void GameOver()
    {

        if (odometerValue > ScoreManager.Instance.playerScore)
        {
            isHighScore = true;
        }
        switch (isHighScore)
        {
            case true:
                newHighScoreScreen.SetActive(true);
                ScoreManager.Instance.playerScore = odometerValue;
                ScoreManager.Instance.SaveScore();
                break;
            case false:
                gameOverScreen.SetActive(true);
                break;
        }

    }


}
