using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("Boomerang Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float returnDelay = 2f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject boomerangPrefab;

    public static void ThrowBoomerang(GameObject boomerangPrefab, Transform playerTransform)
    {
        Vector3 spawnPos = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
        Instantiate(boomerangPrefab, spawnPos, Quaternion.identity);
    }
}
