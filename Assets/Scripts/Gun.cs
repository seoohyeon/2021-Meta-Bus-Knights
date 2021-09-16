using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour {
	public Transform bulletImpact;
	public Transform explosion;
	ParticleSystem bulletps;
	ParticleSystem explosionPs;

    // 20210915_KDH
    // udp client socket
    //public Transform ServerManager;
    private UdpSocket udpSoc;

    // 20210915_KDH
    // magic casting 확인용 bool 변수
    public bool isReadytoCast_Magic = false;

    public Transform crossHair;

    Vector3 originSize;
	void Start()
	{
        // 20210916_KDH udpsocket 컴포넌트 가져옴
        udpSoc = GetComponent<UdpSocket>();

        originSize = crossHair.localScale * 3.2f;
        if (bulletImpact)
			bulletps = bulletImpact.GetComponent<ParticleSystem>();
		if(explosion)
			explosionPs = explosion.GetComponent<ParticleSystem>();
	}
	// Update is called once per frame
	void Update () {
        //if(Input.GetButtonDown("Fire1"))
        {
			Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            // 메인카메라의 렌즈 위치

			RaycastHit hitInfo;  // 충돌되는 collider 반환.

			if(Physics.Raycast(ray, out hitInfo))
			{
                crossHair.position = hitInfo.point;
                crossHair.forward = -1 * ray.direction;
                crossHair.localScale = originSize * hitInfo.distance;
                if (Input.GetButtonDown("Fire1"))
                {
                    if (bulletImpact) //걍 아무거나 맞을때
                    {
                        bulletImpact.up = hitInfo.normal;
                        bulletImpact.position = hitInfo.point + hitInfo.normal * 0.2f;
                        bulletps.Stop();
                        bulletps.Play();
                    }

                    if (hitInfo.transform.name.Contains("Drone")) //drone에 총알이 hit하면
                    {
                        if (explosion) //폭파이펙트가 있으면
                        {
                            explosion.position = hitInfo.transform.position; //폭파되는 위치
                            explosionPs.Stop();
                            explosionPs.Play(); //이펙트 재생

                        }
                        Destroy(hitInfo.transform.gameObject);
                    }
                    else
                    {
                        if (bulletImpact)
                        {

                        }
                    }
                }
			}
		}
	}
}
