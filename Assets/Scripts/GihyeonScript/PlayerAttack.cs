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

    // 20210903_KDH
    // magic casting 확인용 bool 변수
    public bool isReadytoCast_Magic = false;


    //마법 프리팹이 생성되는 장소
    //public GameObject magicPrefabPos;

    //마법 프리팹
    public GameObject[] magicPrefabs;

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
        if (udpSoc.isreceivedData)
        {

            if(udpSoc.curMagicStr == "magicCasting")
            {
                Debug.Log("== Ready To Cast Magic ==");
                isReadytoCast_Magic = true;
            }

            if (udpSoc.curMagicStr == "fireball" && isReadytoCast_Magic)
            {
                isReadytoCast_Magic = false;
                Debug.Log("==== fireball ====");
                playerInput.setChangeCharacterState(1);
                fireMagic();
            }   
            else if(udpSoc.curMagicStr == "thunderStorm" && isReadytoCast_Magic)
            {
                isReadytoCast_Magic = false;
                Debug.Log("!!! thunderstorm !!!");
                playerInput.setChangeCharacterState(2);
                fireMagic();
            }
            else if(udpSoc.curMagicStr == "ignition" && isReadytoCast_Magic)
            {
                isReadytoCast_Magic = false;
                Debug.Log("*** ignition ***");
                playerInput.setChangeCharacterState(3);
                fireMagic();
            }
            
        }

        udpSoc.isreceivedData = false;

        // 마우스 input으로 fireMagic 값이 true가 되면 fireMagic 함수 실행 
        if (playerInput.fireMagic == true)
        {
            fireMagic();
        }
    }

    // 마법 발사 함수
    private void fireMagic()
    {
        if (playerInput.changeCharacterState == 0)
        { } //do nothing
        else if (playerInput.changeCharacterState == 1)
        {
            // magic - fireball
            GameObject magic = Instantiate(magicPrefabs[0], magicPosition.position, magicPosition.rotation);
                                                                                                             //magic.transform.LookAt(magicPosition.forward);
        }
        else if (playerInput.changeCharacterState == 2)
        {
            // magic - thunderstorm
            GameObject magic = Instantiate(magicPrefabs[1], magicPosition.position, magicPosition.rotation); 
                                                                                                             //magic.transform.LookAt(magicPosition.forward);
        }
        else if (playerInput.changeCharacterState == 3)
        {
            // magic - ignition
            GameObject magic = Instantiate(magicPrefabs[2], magicPosition.position, magicPosition.rotation);
                                                                                                             //magic.transform.LookAt(magicPosition.forward);
        }

    }
}
