using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCelestials : MonoBehaviour
{
    // actual value is 6.67 * 10^(-11) but want to actually see orbits
    //public const float G = 0.0001f;
    public const float physicsTimeStep = 0.01f;
    public const float G = 100f;
    Celestial[] celestials;
    static UpdateCelestials instance;

    void Start()
    {
        celestials = FindObjectsOfType<Celestial>();
        Time.fixedDeltaTime = physicsTimeStep;
    }




    // for each body in celestials:
    //      calculate its acceleration using its position
    //      then use that acceleration to calculate its new velocity and thus Update its Velocity
    // for each body in celestials:
    //      Update its Position


    void FixedUpdate()
    {
        for (int i = 0; i < celestials.Length; i++)
        {
            Vector3 acceleration = CalculateAcceleration(celestials[i].Position, celestials[i]);
            celestials[i].UpdateVelocity(acceleration, physicsTimeStep);
        }

        for (int i = 0; i < celestials.Length; i++)
        {
            celestials[i].UpdatePosition(physicsTimeStep);
        }

    }

    public static Vector3 CalculateAcceleration(Vector3 point, Celestial ignoreBody = null)
    {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.celestials)
        {
            if (body != ignoreBody)
            {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector3 forceDir = (body.Position - point).normalized;
                acceleration += forceDir * G * body.GetComponent<Rigidbody>().mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static Celestial[] Celestials
    {
        get
        {
            return Instance.celestials;
        }
    }

    static UpdateCelestials Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UpdateCelestials>();
            }
            return instance;
        }
    }
}