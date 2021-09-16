using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    // UI ���� ������Ʈ
    public GameObject vrUi;
    // UI�� Ȱ��ȭ ���ִ��� check �ϴ� ����
    bool isUi;

    // test prefab�� ������ ��ġ
    public GameObject location;
    // VR UI test�� prefab
    public GameObject cube;

    public GameObject fingerTip;

    // 20210915_KDH
    // udp client socket
    //public Transform ServerManager;
    private UdpSocket udpSoc;

    // 20210915_KDH
    // magic casting Ȯ�ο� bool ����
    public bool isReadytoCast_Magic = false;


    // �հ���, UI
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

        // 20210916_KDH udpsocket ������Ʈ ������
        udpSoc = GetComponent<UdpSocket>();
    }

    void Update()
    {
        // �����Ӹ��� üũ 
        CheckIsPointing();

        if (Physics.Raycast(fingerTip.transform.position, fingerTipForward, out RaycastHit ray, touchDistance))
        {
            Collider rayCollider = ray.collider;
            if ((rayCollider.gameObject.tag=="Button") && isPointing && !isTouching)
            {
                isTouching = true;
                // ��ư�� �۵��ϴ��� test�� prefab ����
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
        // 20210916 KDH �� ���۽� ���� �߻� ����
        if (udpSoc.isreceivedData)
        {
            if (udpSoc.curMagicStr == "UICall" && isUi == false)
            {
                vrUi.SetActive(true);
                isUi = true;
                //Debug.Log("Ȱ��ȭ");
            }
            else if (udpSoc.curMagicStr == "UICall" && isUi == true)
            {
                vrUi.SetActive(true);
                isUi = true;
                //Debug.Log("Ȱ��ȭ");
            }
        }

        // space�� ���� �� UI ON/OFF
        if (Input.GetKeyDown(KeyCode.Space) && isUi == false)
        {
            vrUi.SetActive(true);
            isUi = true;
            //Debug.Log("Ȱ��ȭ");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isUi == true)
        {
            vrUi.SetActive(false);
            isUi = false;
            //Debug.Log("��Ȱ��ȭ");

        }
    }
}
