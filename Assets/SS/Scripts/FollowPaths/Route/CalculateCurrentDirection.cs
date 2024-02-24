using UnityEngine;

public static class CalculateCurrentDirection 
{
    public static Vector3 Calculate(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        return 3 * Mathf.Pow(1 - t, 2) * (p1 - p0) + 6 * (1 - t) * t * (p2 - p1) + 3 * Mathf.Pow(t, 2) * (p3 - p2);
    }
}
