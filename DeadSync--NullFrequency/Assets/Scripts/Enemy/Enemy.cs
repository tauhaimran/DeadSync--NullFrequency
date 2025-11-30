using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public NavMeshAgent agent;
    public Transform player;

    void Update() => agent.SetDestination(player.position);

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Destroy(gameObject);
    }
}
