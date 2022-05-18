using UnityEngine;
using UnityEngine.AI;
public class ShootingAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Animator animator;

    public Transform player;

    public Transform weapon;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    
    public bool isDead;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject projectile;

    private bool _alreadyAttacked;
    
    private void Awake() {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    
    private void Update() {
        if (!isDead) {
            
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


            if (!playerInSightRange && !playerInAttackRange) {
                Patroling();
                animator.SetFloat("Move", agent.velocity.magnitude);
            }

            if (playerInSightRange && !playerInAttackRange) {
                ChasePlayer();
                animator.SetFloat("Move", 3.5f);
            }

            if (playerInAttackRange && playerInSightRange) {
                AttackPlayer();
                animator.SetFloat("Move", 3.5f);
            }
            
        }
    }

    private void Patroling() {
        if (isDead) {
            return;
        }

        if (!walkPointSet) {
            SearchWalkPoint();
        }
        
        if (walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }
    
    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        
        if (Physics.Raycast(walkPoint,-transform.up, 2,whatIsGround))
            walkPointSet = true;
    }
    
    private void ChasePlayer() {
        if (isDead) return;

        agent.SetDestination(player.position);
    }
    
    private void AttackPlayer() {
        if (isDead) return;

        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        if (!_alreadyAttacked) {

            Rigidbody rb = Instantiate(projectile, weapon.transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            _alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);
        }

    }
    private void ResetAttack() {
        if (isDead) return;

        _alreadyAttacked = false;
    }
}
