using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    // 플레이어 입력을 알려주는 컴포넌트
    private PlayerInput playerInput;


    // 20210812_KDH
    // udp client socket
    //public Transform ServerManager;
    private UdpSocket udpSoc;


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

        // 20210812_KDH udpsocket 컴포넌트 가져옴
        udpSoc = GetComponent<UdpSocket>();
    }

    //물리 주기에 맞춰 실행됨
    void Update()
    {
        // 20210812 KDH 손 동작시 마법 발사 구현
        if ( udpSoc.curMagicStr == "fireball" && udpSoc.isreceivedData)
        {
            fireMagic();
        }

        udpSoc.isreceivedData = false;

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
