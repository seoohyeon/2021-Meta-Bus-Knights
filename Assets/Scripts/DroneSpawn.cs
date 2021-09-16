﻿using UnityEngine;
using System.Collections;

public class DroneSpawn : MonoBehaviour {
	public GameObject drone;
	public float MIN_TIME = 1;
	public float MAX_TIME = 2;
	// Use this for initialization
	void Start () {
		StartCoroutine("CreateDrone");
	}
	
	IEnumerator CreateDrone()
	{
		while(Application.isPlaying)
		{
			float createTime = Random.Range(MIN_TIME, MAX_TIME);
			yield return new WaitForSeconds(createTime);

			Instantiate(drone, transform.position, Quaternion.identity); // 생성객체, 생성위치, 그냥 회전X고 기본값
		}
	}
}
