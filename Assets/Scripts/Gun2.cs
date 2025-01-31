using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Gun2 : MonoBehaviour
{
    public Transform bulletImpact;
    public Transform dustexplosion;
    ParticleSystem bulletps;
    ParticleSystem explosionPs;

    public Transform crossHair;

    Vector3 originSize;
    void Start()
    {
        originSize = crossHair.localScale * 3.2f;
        if (bulletImpact)
            bulletps = bulletImpact.GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                crossHair.position = hitInfo.point;
                crossHair.forward = -1 * ray.direction;
                crossHair.localScale = originSize * hitInfo.distance;
                if (Input.GetButtonDown("Fire1"))
                {
                    if (bulletImpact)
                    {
                        bulletImpact.up = hitInfo.normal;
                        bulletImpact.position = hitInfo.point + hitInfo.normal * 0.2f;
                        bulletps.Stop();
                        bulletps.Play();
                    }

                    if (hitInfo.transform.name.Contains("Drone"))
                    {
                        if (dustexplosion)
                        {
                            dustexplosion.position = hitInfo.transform.position;
                            explosionPs.Stop();
                            explosionPs.Play();


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
