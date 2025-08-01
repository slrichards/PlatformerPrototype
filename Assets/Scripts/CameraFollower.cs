using UnityEngine;
/*
 * CameraFollower.cs
 * This script is responsible for making the camera follow the player object.
 */
public class CameraFollower : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float offset;

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // finds the player object by tag name.
        }
        else
        {
            transform.position = new Vector3(playerTransform.position.x + offset, playerTransform.position.y + offset, -10);
        }
    }

    void Update()
    {
        //Moves the camera to follow the player object with an offset
        transform.position = new Vector3(playerTransform.position.x + offset, playerTransform.position.y + offset, -10);
    }

}
