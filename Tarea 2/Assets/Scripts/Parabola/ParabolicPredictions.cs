using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ParabolicPredictions : MonoBehaviour
{

    LineRenderer ir;
    Vector3 velocity;
    CharacterStatus status;
    public float percentResolution = 0.5f;
    public int resolution = 20;
    ParabolicThrow playerRoute;
    float g; // fuerza de gravedad (Para simplificar su uso).

    private void Awake()
    {
        status = gameObject.GetComponentInParent<CharacterStatus>();
        ir = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
        ir.positionCount = 2 * resolution + 1;
    }

    void Start()
    {
        playerRoute = gameObject.GetComponent<ParabolicThrow>();
    }

    void Update()
    {
        velocity = playerRoute.GetRelativeSpeed();
        RenderArc();
    }

    void RenderArc()
    {
        if (!status.isDeath)
            ir.SetPositions(CalculateArcArray());
        else
            ir.positionCount = 0;
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[2 * resolution + 1];
        //radAngle = Mathf.Deg2Rad * velocity.y;
        float absDistance = (velocity.x * velocity.x * Mathf.Abs(Mathf.Sin(2 * velocity.y))) / g;
        absDistance *= percentResolution;
        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, absDistance);
        }

        for (int i = resolution + 1; i <= resolution * 2; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, absDistance);
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float radDistance)
    {
        float x = t * radDistance;
        float y = x * Mathf.Tan(velocity.y) - (g * x * x) / (2 * velocity.x * velocity.x * Mathf.Cos(velocity.y) * Mathf.Cos(velocity.y));
        return new Vector3(x, y);
    }
}
