using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform PosA, PosB;

    [SerializeField] private float moveSpeed;

    private Transform targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = PosB;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, PosA.position) < 0.1f) targetPos = PosB;
        if (Vector2.Distance(transform.position, PosB.position) < 0.1f) targetPos = PosA;

        transform.position = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed *Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collison )
    {
        if (collison.CompareTag("Player"))
        {
            collison.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
