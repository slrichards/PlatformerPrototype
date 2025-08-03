using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Variables for the enemy controller
    [Header("Enemy Settings")]
    [SerializeField] private GameObject Player;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float enemyHealth = 3f;

    private float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Takes in 2 transforms, finds the distance between them, and moves the enemy towards the player.
        //Built into C#
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);

    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
