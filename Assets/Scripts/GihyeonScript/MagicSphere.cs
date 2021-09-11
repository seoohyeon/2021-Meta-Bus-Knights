using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    // 발사 구체의 속도
    public float speed = 15f;

    // 오브젝트의 물리 제어를 위한 컴포넌트
    private Rigidbody magicRigidbody;

    //마법이 적중한 위치를 저장할 변수
    Vector3 hitPosition = Vector3.zero;

    //마법 적중시 효과
    public GameObject magicEffectPrefab;

    void Start()
    {
        magicRigidbody = GetComponent<Rigidbody>();
        magicRigidbody.velocity = transform.forward * speed;

        //일정 시간이 지난후 파괴
        Destroy(gameObject, 4f);
    }

    // 충돌 시 효과
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("적중됨!");
            GameObject magicEffect = Instantiate(magicEffectPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(magicEffect, 2f);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Level"))
        {
            Destroy(gameObject);
        }
    }

}


