using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Background Settings")]
    [SerializeField] private Vector3 moveOffSet = new Vector3(2f, 0f, 0f);
    [SerializeField] private float moveDuration = 2f;

    private Vector3 initialPosition;    
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {  
        // Move the background
        Vector3 targetPosition = initialPosition + moveOffSet;
        transform.position = Vector3.Lerp(initialPosition, targetPosition, Mathf.PingPong(Time.time / moveDuration, 1));
       
    }
}
