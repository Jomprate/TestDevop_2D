using UnityEngine;

public static class CalculateTotalDistanceObjectWillTravel 
{
    public static float Calculate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Vector3.Distance(p0, p1) + Vector3.Distance(p1, p2) + Vector3.Distance(p2, p3);
    }

    public static float CalculateBezierCurveLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int segments = 100)
    {
        float totalLength = 0f;
        float deltaT = 1f / segments;

        for (int i = 0; i < segments; i++)
        {
            float t0 = i * deltaT;
            float t1 = (i + 1) * deltaT;

            Vector3 startPoint = CalculateBezierPoint(p0, p1, p2, p3, t0);
            Vector3 endPoint = CalculateBezierPoint(p0, p1, p2, p3, t1);

            totalLength += Vector3.Distance(startPoint, endPoint);
        }

        return totalLength;
    }

    private static Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // (1 - t)^3 * p0
        p += 3 * uu * t * p1; // 3 * (1 - t)^2 * t * p1
        p += 3 * u * tt * p2; // 3 * (1 - t) * t^2 * p2
        p += ttt * p3; // t^3 * p3

        return p;
    }


}

