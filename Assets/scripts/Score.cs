using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]float score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    void Start()
    {
        
    }

    public void ScoreDisPlay(float points)
    {
        score = +points;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
