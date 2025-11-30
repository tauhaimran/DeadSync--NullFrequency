using UnityEngine.AI;

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
