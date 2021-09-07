using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    // test prefab이 생성될 위치
    public GameObject location;
    // VR UI test용 prefab
    public GameObject cube;

    public GameObject fingerTip;


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
}
