using UnityEngine;

public static class CalculateObjectRotationAngleToPointToCorrectDirection
{
    public static void Calculate(Vector3 direction, Transform transform)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
