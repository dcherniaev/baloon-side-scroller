using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
    public Text bestDistanceText;

    void Awake()
    {
        ScoreManager.Instance.LoadScore();
        bestDistanceText.text = "Best distance: " + ScoreManager.Instance.playerName + " " + ScoreManager.Instance.playerScore;
    }

    public void ResetScore()
    {
        ScoreManager.Instance.playerName = " ";
        ScoreManager.Instance.playerScore = 0;
        ScoreManager.Instance.SaveScore();
        SceneManager.LoadScene(0);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

#else

        Application.Quit(); // original code to quit Unity player
#endif
    }

}