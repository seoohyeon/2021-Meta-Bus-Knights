using System.Collections;
using UnityEngine;

public class WeaponManager1 : MonoBehaviour
{
    public float switchDelay = 1f;
    public GameObject[] weapon;  //무기의 게임 오브젝트 배열

    private int index = 0;  // 무기의 인덱스
    private bool isSwitching = false;  // 딜레이를 확인하기 위함.

    // Use this for initialization
    private void Start()
    {
        InitializeWeapon();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !isSwitching) // 마우스 휠이 내려가고 딜레이가 아니면 인덱스 올리고 스위칭
        {
            index++;
            if (index >= weapon.Length)
                index = 0;
            StartCoroutine(SwitchDelay(index)); //IEnumerator를 실행시키기 위해서 사용하는 함수
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && !isSwitching)
        {
            index--;
            if (index < 0)
                index = weapon.Length - 1;
            StartCoroutine(SwitchDelay(index));
        }

        // weapon switching by alphanum
        for (int i = 49; i < 52; i++)  //1부터 3까지의 키를 입력받아 해당하는 인덱스로 지정 스위칭
        {
            if (Input.GetKeyDown((KeyCode)i) && !isSwitching && weapon.Length > i - 49 && index != i - 49)
            {
                index = i - 49;
                StartCoroutine(SwitchDelay(index));
            }
        }
    }

    private void InitializeWeapon() //게임이 시작될때 초기화하는 부분.
        //0번 인덱스의 무기만 Active하고 나머지는 Active를 false로 함.

    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(false);
        }
        weapon[0].SetActive(true);
        index = 0;
    }

    private IEnumerator SwitchDelay(int newIndex)
//함수가 실행될때 대기시간을 넣으려면 IEnumerator를 사용해야하는데
//yield 형으로 WaitForSeconds(초)의 리턴값을 리턴해주면 
//해당 초만큼 대기를 한 후 다음줄부터 실행됨.
//따라서 isSwitching을 true로 만들고 switchDelay만큼의 초를 기다린 뒤 다시 false가 되는식.
    {
        isSwitching = true;
        SwitchWeapons(newIndex);
        yield return new WaitForSeconds(switchDelay);
        isSwitching = false;
    }

    private void SwitchWeapons(int newIndex) // 입력받은 인덱스의 오브젝트를 활성화하고 나머지는 비활성화함.
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(false);
        }
        weapon[newIndex].SetActive(true);
    }
}