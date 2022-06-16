using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMoon : MonoBehaviour
{
    // actual value is 6.67 * 10^(-11) but want to actually see orbits
    //public const float G = 0.0001f;
    public const float physicsTimeStep = 0.01f;
    public const float G = 7.836f;
    MoonPlanet[] MoonPlanetSystems;
    static UpdateMoon instance;

    void Start()
    {
        MoonPlanetSystems = FindObjectsOfType<MoonPlanet>();
        Time.fixedDeltaTime = physicsTimeStep;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < MoonPlanetSystems.Length; i++)
        {
            Vector3 acceleration = CalculateAcceleration(MoonPlanetSystems[i].Position, MoonPlanetSystems[i]);
            MoonPlanetSystems[i].UpdateVelocity(acceleration, physicsTimeStep);
        }

        for (int i = 0; i < MoonPlanetSystems.Length; i++)
        {
            MoonPlanetSystems[i].UpdatePosition(physicsTimeStep);
        }

    }

    public static Vector3 CalculateAcceleration(Vector3 point, MoonPlanet ignoreBody = null)
    {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.MoonPlanetSystems)
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

    public static MoonPlanet[] MoonPlanetSystems
    {
        get
        {
            return Instance.MoonPlanetSystems;
        }
    }

    static UpdateMoon Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UpdateMoon>();
            }
            return instance;
        }
    }
}
