using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    // 플레이어 입력을 알려주는 컴포넌트
    private PlayerInput playerInput;

    //마법 프리팹이 생성되는 장소
    public GameObject magicPrefabPos;

    //마법 프리팹
    public GameObject magicPrefab;

    //마법이 발사될 위치
    public Transform magicPosition;

    void Start()
    {
        //사용할 컴포넌트의 참조 가져오기
        playerInput = GetComponent<PlayerInput>();
    }

    //물리 주기에 맞춰 실행됨
    void Update()
    {
        if (playerInput.fireMagic == true)
        {
            fireMagic();
        }
    }

    private void fireMagic()
    {
        GameObject magic = Instantiate(magicPrefab, magicPosition.position, magicPosition.rotation); // magic을 magicPrefabPos에 생성한다.
        //magic.transform.LookAt(magicPosition.forward);
    }

}
