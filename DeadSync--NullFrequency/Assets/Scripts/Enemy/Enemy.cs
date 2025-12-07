using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Zombie Stats")]
    public float health = 10f;
    public float moveSpeed = 1.5f;
    public float detectionRange = 50f;
    public int Coins;

    [Header("Patrol Settings")]
    public float patrolDistance = 30f; 
    public bool moveHorizontal = true; // true = left/right | false = forward/back

    public float damage;
    private Transform player => Player.instance?.transform;

    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Vector3 startPos;
    private int dir = 1;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        //// 🔥 ensures zombie never tilts or falls sideways
        KeepUpright();

        //float dist = Vector3.Distance(transform.position, player.position);

        //if (dist <= detectionRange)
        //    ChasePlayer();
        //else
        //    PatrolBackAndForth();

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (Player.instance != null)
        {
            if (playerInSightRange && !playerInAttackRange) ChasePlayer1();
            if (playerInAttackRange && playerInSightRange) ChasePlayer1();
        }
    }

    // ====================== PREVENT FALLING lmao ======================
    void KeepUpright()
    {
        Vector3 rot = transform.eulerAngles;
        rot.x = 0;    // no faceplant
        rot.z = 0;    // no sideways collapse
        transform.eulerAngles = rot;
    }
    private void OnDrawGizmosSelected()
    {

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, sightRange);

        // Attack Range = Red
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Sight Range = Yellow
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
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
        if (health <= 0)
        {
            SaveData.instance.Coins += Coins;
            Destroy(gameObject);
        }
    }

    // =======================NavMesh=====================

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Player.instance?.Damage(damage);
        }
    }
    private void ChasePlayer1()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    
}
