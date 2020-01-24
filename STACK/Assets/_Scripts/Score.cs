using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;

    public TextMeshProUGUI scoreText;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    
}
