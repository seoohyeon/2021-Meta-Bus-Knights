using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour {

    public static Tower Instance;

    public Slider hpSlider;

    public int MAX_HP = 10;
    int hp = 0;
    bool gameOver = false;

    public GameObject die;

    void Awake()
    {

        if (Instance == null)
            Instance = this;
    }

    void Start()
    {

        hp = MAX_HP;
    }

        private void Update()
        {

        if (gameOver == true && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
        public void Damage()
        {
            hp--;

            hpSlider.value = hp;

            gameOver = true;

            if (hp <= 0)
            {
                if (die)
                {
                    die.SetActive(true);
                }
            }
        }
}