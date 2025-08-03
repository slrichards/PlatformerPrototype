using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("Boomerang Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float damage = 1f;

    private Transform playerTransform;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isReturning = false;
    public void Initialize(Vector3 targetPosition, Transform playerTransform)
    {
        this.targetPosition = targetPosition;
        this.playerTransform = playerTransform;
        startPosition = playerTransform.position;
    }
    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        //Boomering moves towards the target position.
        if (!isReturning)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f || Vector3.Distance(transform.position, startPosition) > maxDistance)
            {
                isReturning = true;
            }
        }
        //Boomerang returns to the player and deletes itself when in range.
        else
        {
            if(playerTransform != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, returnSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, playerTransform.position) <= 0.2f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public float GetDamage() { return damage; }


    /*    public static void ThrowBoomerang(GameObject boomerangPrefab, Transform playerTransform)
        {
            Vector3 spawnPos = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
            Instantiate(boomerangPrefab, spawnPos, Quaternion.identity);
        }*/
}


