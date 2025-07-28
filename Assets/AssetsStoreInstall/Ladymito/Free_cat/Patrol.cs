using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] points;    
    public float speed = 2f;      
    public float rotationSpeed = 5f; 
    private int currentPoint = 0; 

    void Update()
    {
        if (points.Length == 0) return;

        Vector3 targetPosition = points[currentPoint].position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
       
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}