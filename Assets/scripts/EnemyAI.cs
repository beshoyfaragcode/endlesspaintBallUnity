using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public Transform player;
    public NavMeshAgent Enemy;
    public Rigidbody rb;
    public NavMeshHit hit;

	
	
	// Update is called once per frame
	void Update () {
        Enemy.Warp(transform.position);
        Enemy.SetDestination(player.position);
        Enemy.Raycast(player.position, out hit);
        Color32 rayColor = new Color32(100, 255, 100, 1);
        Debug.DrawRay(transform.position, player.position,rayColor);
	}
}
