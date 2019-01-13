using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovementController : MonoBehaviour {
    public float AsteroidSpeed = 2;
    public float AsteroidAngularSpeed = 2;
    public Vector3 InitialDirection = Vector3.zero;

	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * AsteroidAngularSpeed;
        
        if (InitialDirection.Equals(Vector3.zero))
        {
            rb.velocity = Random.insideUnitSphere * AsteroidSpeed;
        }
        else
        {
            rb.velocity = InitialDirection * AsteroidSpeed;
        }
    }


}
