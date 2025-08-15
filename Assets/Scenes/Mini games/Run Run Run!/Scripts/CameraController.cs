using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + playerTransform.position.z);
        transform.position = newPosition;
    }
}
