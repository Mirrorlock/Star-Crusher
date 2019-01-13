using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {
    public float speed = 5f;
    public float rotateSpeed = 1;
    public bool debugDraw = false;

    private float rotateStep;
    
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotateStep = rotateSpeed * Time.deltaTime;
    }

    private void GoForward()
    {
        rb.velocity = transform.forward * speed;
    }

    private void RotateTowards(Vector3 point)
    {
        Vector3 targetPos =  point - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetPos, rotateStep, 0.0f);
        if (debugDraw)
        {
            Debug.DrawRay(transform.position, targetPos, Color.red);
            Debug.DrawRay(transform.position, newDirection, Color.red);
        }
        rb.rotation = Quaternion.LookRotation(newDirection);
    }

    public void Move(Vector3 playerPos)
    {
        GoForward();
        RotateTowards(playerPos);
    }
    
}
