using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver2to3 : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] float maxTime = 5f;
    float timeLeft;
    Image timerBar;

    // Start is called befores the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    public void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            gameOverText.SetActive(true);
            Time.timeScale = 0;


            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage3");
            Time.timeScale = 1f;
        }
        
    }
}
