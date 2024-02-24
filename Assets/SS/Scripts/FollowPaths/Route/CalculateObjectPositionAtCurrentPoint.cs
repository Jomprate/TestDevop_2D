using UnityEngine;

public static class CalculateObjectPositionAtCurrentPoint 
{
    public static Vector3 Calculate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Calculate the position of the object at the current point of the route using Bezier interpolation
        Vector3 objectPosition = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
        return objectPosition;
    }
}
