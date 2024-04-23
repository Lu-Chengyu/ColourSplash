using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        offset.x = 2.65f;
        offset.y = 1.4f;
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void moveCameraToPlayer(Vector3 pos)
    {
        transform.position = pos + offset;
        
    }
}