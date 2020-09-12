using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
public class GameOverScript : MonoBehaviour
{

    public Text txtScore;
    public Text highScore;
    public int score;
    public int highscore;

    public void Start() {
        score = PlayerPrefs.GetInt("score");

        if(score > highscore) {
            PlayerPrefs.SetInt("highscore", score);
        }
        highscore = PlayerPrefs.GetInt("highscore");

        txtScore.text = "<i>Score: " + score +"</i>";
        highScore.text = "<i>Highscore: " + highscore +"</i>";
        
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("Scene");
    }
}
