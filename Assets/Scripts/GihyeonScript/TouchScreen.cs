using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    // UI 게임 오브젝트
    public GameObject vrUi;
    // UI가 활성화 돼있는지 check 하는 변수
    bool isUi;

    // test prefab이 생성될 위치
    public GameObject location;
    // VR UI test용 prefab
    public GameObject cube;

    public GameObject fingerTip;

    // 20210915_KDH
    // udp client socket
    //public Transform ServerManager;
    private UdpSocket udpSoc;

    // 20210915_KDH
    // magic casting 확인용 bool 변수
    public bool isReadytoCast_Magic = false;


    // 손가락, UI
    bool isPointing;
    bool isTouching;
    Vector3 fingerTipForward;
    float touchDistance;

    void Start()
    { 
        fingerTipForward = fingerTip.transform.TransformDirection(Vector3.forward);
        touchDistance = 0.01f;
        isPointing = false;
        isTouching = false;

        isUi = false;

        // 20210916_KDH udpsocket 컴포넌트 가져옴
        udpSoc = GetComponent<UdpSocket>();
    }

    void Update()
    {
        // 프레임마다 체크 
        CheckIsPointing();

        if (Physics.Raycast(fingerTip.transform.position, fingerTipForward, out RaycastHit ray, touchDistance))
        {
            Collider rayCollider = ray.collider;
            if ((rayCollider.gameObject.tag=="Button") && isPointing && !isTouching)
            {
                isTouching = true;
                // 버튼이 작동하는지 test용 prefab 생성
                Instantiate(cube, location.transform.position, new Quaternion(), gameObject.transform);

            }
            else 
            {
                isTouching = false;

            }
        }

            UiOnOff();


    } 

    void CheckIsPointing()
    {
        if (!OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger))
        {
            isPointing = true;
        }
        else
        {
            isPointing = false;
        }
    }

    void UiOnOff()
    {
        // 20210916 KDH 손 동작시 마법 발사 구현
        if (udpSoc.isreceivedData)
        {
            if (udpSoc.curMagicStr == "UICall" && isUi == false)
            {
                vrUi.SetActive(true);
                isUi = true;
                //Debug.Log("활성화");
            }
            else if (udpSoc.curMagicStr == "UICall" && isUi == true)
            {
                vrUi.SetActive(true);
                isUi = true;
                //Debug.Log("활성화");
            }
        }

        // space바 누를 시 UI ON/OFF
        if (Input.GetKeyDown(KeyCode.Space) && isUi == false)
        {
            vrUi.SetActive(true);
            isUi = true;
            //Debug.Log("활성화");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isUi == true)
        {
            vrUi.SetActive(false);
            isUi = false;
            //Debug.Log("비활성화");

        }
    }
}
