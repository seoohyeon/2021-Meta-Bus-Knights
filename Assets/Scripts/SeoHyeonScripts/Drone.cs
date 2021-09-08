using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Drone : MonoBehaviour {
	NavMeshAgent agent;
	Transform tower;
	public float ATTACK_TIME = 1;
	float attackTime = 0;
	public int MAX_HP = 1;
	[System.NonSerialized]
	public int hp = 0;

	public float ATTACK_DISTANCE = 10;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		tower = GameObject.Find("Tower").transform;
		agent.destination = tower.position;

		hp = MAX_HP;
		attackTime = ATTACK_TIME;
	}


	void Update()
	{
		
		if(agent.remainingDistance <= ATTACK_DISTANCE)
		{
			attackTime += Time.deltaTime;
			if(attackTime > ATTACK_TIME)
			{
				attackTime = 0;
				Tower.Instance.Damage();
			}
		}
	}
}
