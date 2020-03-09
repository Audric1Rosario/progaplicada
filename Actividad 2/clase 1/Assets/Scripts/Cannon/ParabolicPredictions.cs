using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ParabolicPredictions : MonoBehaviour
{
    LineRenderer ir;

    private float velocity;
    private float angle;
    public int resolution = 20;
    CannonController playerRoute;
    float g; // fuerza de gravedad
             // Start is called before the first frame update
    float radAngle;
    private void Awake()
    {
        ir = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics.gravity.y);
        ir.positionCount = 2 * resolution + 1;
    }

    
    void Start()
    {
        playerRoute = gameObject.GetComponent<CannonController>();
    }

    //[System.Obsolete]
    void Update()
    {
        velocity = playerRoute.triggerSpeed * playerRoute.power;
        angle = playerRoute.triggerAngle * Mathf.Rad2Deg;
        
        RenderArc();

    }

    //[System.Obsolete]
    void RenderArc()
    {
        
        //ir.SetVertexCount(resolution + 1);
        ir.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[2*resolution + 1];
        radAngle = Mathf.Deg2Rad * angle;
        float absDistance = (velocity * velocity * Mathf.Sin(2 * radAngle)) / g;
        
        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, absDistance);
        }

        for (int i = resolution + 1; i <= resolution*2; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, absDistance);
        }
        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float radDistance)
    {
        float x = t * radDistance;
        float y = x * Mathf.Tan(radAngle) - (g * x * x) / (2 * velocity * velocity * Mathf.Cos(radAngle) * Mathf.Cos(radAngle));
        return new Vector3(x, y);
    }

}
