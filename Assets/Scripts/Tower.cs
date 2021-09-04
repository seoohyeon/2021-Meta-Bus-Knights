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
    bool gameOver = false; //시작시 게임오버가 아님 

	public GameObject die;

	void Awake()
	{
        
		if(Instance == null)
			Instance = this;
	}

	void Start()
	{
        
		hp = MAX_HP;
	}

    private void Update()
    {
        if (gameOver == true && Input.GetMouseButton(0))
            SceneManager.LoadScene("Stage1");
    }

    public void Damage()
	{
		hp--;


        hpSlider.value = hp;

        gameOver = true;

		if(hp <= 0)
		{
			if(die)  //플레이어 사망시
			{
				die.SetActive(true);  //게임오버 UI 생성
			}
		}
	}
}
