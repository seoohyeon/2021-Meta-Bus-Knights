using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
