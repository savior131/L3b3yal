using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float radius = 5f;
    public float detectionRadius = 3f;
    public float stoppingDistance = 1f; // Distance at which the enemy stops moving
    private Vector3 centerPosition;
    private float angle;
    private bool isPlayerDetected;
    private Transform playerTransform;

    private void Start()
    {
        centerPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPlayerDetected)
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer > stoppingDistance)
            {
                // Move towards the player until reaching the stopping distance
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Calculate the next position based on the current angle
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;
            Vector3 nextPosition = centerPosition + new Vector3(x, 0f, z);

            // Move the enemy to the next position
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, movementSpeed * Time.deltaTime);
            // Update the angle for the next frame
            angle += movementSpeed * Time.deltaTime;

            // Reset the angle if it exceeds a full circle (2pi)
            if (angle >= Mathf.PI * 2f)
            {
                angle -= Mathf.PI * 2f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
}
