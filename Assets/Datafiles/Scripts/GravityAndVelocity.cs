using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAndVelocity : MonoBehaviour
{
    // actual value is 6.67 * 10^(-11) but want to actually see orbits
    readonly float G = 100f;
    GameObject[] celestials;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
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
        foreach(GameObject a in celestials)
        {
            foreach(GameObject b in celestials)
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
}
