using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    public Vector3 initialVelocity;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        rb.velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        rb.MovePosition(rb.position + rb.velocity * timeStep);
    }

    public Rigidbody Rigidbody
    {
        get
        {
            return rb;
        }
    }

    public Vector3 Position
    {
        get
        {
            return rb.position;
        }
    }
}
