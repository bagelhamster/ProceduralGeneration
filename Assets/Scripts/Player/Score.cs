using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreText2;
    public GameObject player;
    public static int scoreValue = 0;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreValue = 0;
        scoreText2.gameObject.SetActive(false);
        player.GetComponent<FPSController>().enabled = true;


    }
    void Update()
    {
        scoreText.text = "Eggs found " + scoreValue+"/5";


        if (scoreValue >= 5)
         {
            scoreText2.gameObject.SetActive(true);
            player.GetComponent<FPSController>().enabled = false;
}
    }
}
