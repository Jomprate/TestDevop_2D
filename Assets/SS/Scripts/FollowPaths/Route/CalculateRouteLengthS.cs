using UnityEngine;

public static class CalculateRouteLengthS 
{
    public static float Calculate(Vector3[] routePoints)
    {
        float length = 0;
        for (int i = 0; i < routePoints.Length - 1; i++)
        {
            length += Vector3.Distance(routePoints[i], routePoints[i + 1]);
        }
        return length;
    }
}
