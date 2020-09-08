using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
public class GameOverScript : MonoBehaviour
{
public Text txtScore;

    public void Start() {
        int score = PlayerPrefs.GetInt("score");
        txtScore.text = "Game over!\n <i>Score: " + score +"</i>";
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("Scene");
    }
}
