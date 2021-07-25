using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//사용자 입력 감지
//감지된 입력값을 다른 컴포넌트가 사용할 수 있게 제공
public class PlayerInput : MonoBehaviour
{
    //카메라 관련
    public float rotateSpeed = 200;
    float mouseAngleX;
    float mouseAngleY;

    //값 할당은 내부에서만 가능하도록 getter/setter 지정
    public bool fireMagic { get; private set; }
    public int changeCharacterState { get; private set; }
    
    // 마법 발사와 캐릭터 상태 변경 함수 초기화
    void Start()
    {
        fireMagic = false;
        changeCharacterState = 0;
    }

   
    // 매 프레임마다 입력 체크
    void Update()
    {
        //카메라 관련
        // 마우스 x,y축의 움직임 감지 , 좌우 : y축 회전, 위아래 : x축 회전
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        // P = P0 + vt
        mouseAngleX += x * rotateSpeed * Time.deltaTime; // 마우스 x각도 누적
        mouseAngleY += y * rotateSpeed * Time.deltaTime; // 마우스 y각도 누적

        /*
        if (mouseAngleY >= 90)
        {
            mouseAngleY = 90;
        }
        else if (mouseAngleY <= -90)
        {
            mouseAngleY = -90;
        }*/
        mouseAngleY = Mathf.Clamp(mouseAngleY, -90, 90); //mouseAngleY의 최소,최댓값 지정

        //픽셀이 부호가 위아래 반대이므로, mouseAngleY에 -1을 곱해서 반대로 만든다.
        transform.eulerAngles = new Vector3(-mouseAngleY, mouseAngleX, 0);



        fireMagic = Input.GetMouseButtonDown(0);

        if (Input.GetMouseButtonDown(1))
        {
            changeCharacterState++;
            changeCharacterState = changeCharacterState % 4;
        }
    }
}
