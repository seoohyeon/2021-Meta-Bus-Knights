using UnityEngine;
using System.Collections;

public class DroneSpawn : MonoBehaviour {

    public GameObject drone;
	public float MIN_TIME = 1;
	public float MAX_TIME = 5;

    public int monsterCount = 0;
    public int monsterMaxCount = 5;

    // Use this for initialization
    void Start () {
		StartCoroutine("CreateDrone");
	}

    private void Update()
    {
        
    }


    IEnumerator CreateDrone()
	{
        while (Application.isPlaying)
        {
            float createTime = Random.Range(MIN_TIME, MAX_TIME);
            yield return new WaitForSeconds(createTime);


            Instantiate(drone, transform.position, Quaternion.identity);
        }
	}
}
