using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver1to2 : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] float maxTime = 5f;
    [SerializeField] float timeLeft;


    // Start is called befores the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
        timeLeft = maxTime;
    }

    // Update is called once per frame
    public void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;

          
            SceneManager.LoadScene("Stage2");
            Time.timeScale = 1f;
        }
        }

    }
