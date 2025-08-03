using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Variables for the enemy controller
    [Header("Enemy Settings")]
    [SerializeField] private GameObject Player;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float enemyHealth = 3f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private float attackCoolDown = 1f;

    private float distance;
    private float lastAttackTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Takes in 2 transforms, finds the distance between them, and moves the enemy towards the player.
        //Built into C#
        distance = Vector2.Distance(transform.position, Player.transform.position);
        //Vector2 direction = Player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

        if(distance < attackDistance && Time.time > lastAttackTime + attackCoolDown)
        {
            AnimatedController animatedController = Player.GetComponent<AnimatedController>();
            if (animatedController != null)
            {
                animatedController.DoDamage(attackDamage);
                lastAttackTime = Time.time;
            }
        }

    }

    //Function to take damage from the enemy, and destroy it if health is 0 or less.
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
