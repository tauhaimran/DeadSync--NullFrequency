using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Zombie Stats")]
    public float health = 10f;
    public float moveSpeed = 1.5f;
    public float detectionRange = 50f;

    [Header("Patrol Settings")]
    public float patrolDistance = 30f; 
    public bool moveHorizontal = true; // true = left/right | false = forward/back

    [Header("References")]
    public Transform player;

    private Vector3 startPos;
    private int dir = 1;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 🔥 ensures zombie never tilts or falls sideways
        KeepUpright();

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= detectionRange)
            ChasePlayer();
        else
            PatrolBackAndForth();
    }

    // ====================== PREVENT FALLING lmao ======================
    void KeepUpright()
    {
        Vector3 rot = transform.eulerAngles;
        rot.x = 0;    // no faceplant
        rot.z = 0;    // no sideways collapse
        transform.eulerAngles = rot;
    }

    // ====================== PATROL ======================
    void PatrolBackAndForth()
    {
        Vector3 target = moveHorizontal ?
            startPos + new Vector3(patrolDistance * dir, 0, 0) :
            startPos + new Vector3(0, 0, patrolDistance * dir);

        MoveTowards(target);

        if (Vector3.Distance(transform.position, target) < 1f)
            dir *= -1;
    }

    // ====================== CHASE ======================
    void ChasePlayer()
    {
        MoveTowards(player.position);
    }

    // ====================== MOVEMENT ======================
    void MoveTowards(Vector3 target)
    {
        Vector3 dirMove = (target - transform.position).normalized;
        dirMove.y = 0;

        transform.position += dirMove * moveSpeed * Time.deltaTime;

        if (dirMove != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirMove), 4f * Time.deltaTime);
    }

    // ====================== DAMAGE ======================
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Destroy(gameObject);
    }
}
