using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// actual value is 6.67 * 10^(-11) but want to actually see orbits
//public const float gravitationalConstant = 0.0001f;
readonly float physicsTimeStep = 0.01f;
readonly float G = 100f;
GameObject[] celestials;

// Start is called before the first frame update
void Start()
{
    celestials = GameObject.FindGameObjectsWithTag("Celestial");

    // gives celestials velocity upon first frame update
    InitialVelocity();
}

// Update is called once per frame
void Update()
{

}

// used for physics stuff
private void FixedUpdate()
{
    Gravity();
}

// enables SolarSytem gravity with Newton's law
void Gravity()
{
    foreach (GameObject a in celestials)
    {
        foreach (GameObject b in celestials)
        {
            // applies gravity to a if a neq to itself
            if (!a.Equals(b))
            {
                float m1 = a.GetComponent<Rigidbody>().mass;
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);

                // here we add the force of gravity to the Rigidbody component of a
                //  first we obtain the normalized vector for direction, then we simply
                //  multiply by Newton's law
                a.GetComponent<Rigidbody>().AddForce(
                    (b.transform.position - a.transform.position).normalized *
                    (G * (m1 * m2) / (r * r)));
            }
        }
    }
}

// gives celestials initial velocity
void InitialVelocity()
{
    foreach (GameObject a in celestials)
    {
        foreach (GameObject b in celestials)
        {
            if (!a.Equals(b))
            {
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);
                a.transform.LookAt(b.transform);

                a.GetComponent<Rigidbody>().velocity += a.transform.right *
                    Mathf.Sqrt((G * m2) / r);
            }
        }
    }
}