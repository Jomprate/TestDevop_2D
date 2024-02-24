using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMovementManager : IMovementManager
{
    public IEnumerator MoveAlongRoute(Transform[] route, int routeIndex, float speedModifier, Cuy cuy, Action<Vector3> onPositionChanged)
    {
        Vector3 p0, p1, p2, p3;
        GetRoutePositions.Get(route, routeIndex, out p0, out p1, out p2, out p3);

        float distance = CalculateTotalDistanceObjectWillTravel.Calculate(p0, p1, p2, p3);
        float speed = CalculateSpeedOfTheObject.Calculate(distance, speedModifier, cuy.Fatigue);

        float t = 0; // Parameter for Bezier interpolation

        while (t <= 1)
        {
            t += Time.deltaTime / speed; // Increment the time parameter

            Vector3 direction = CalculateCurrentDirection.Calculate(p0, p1, p2, p3, t);
            Vector3 objectPosition = CalculateObjectPositionAtCurrentPoint.Calculate(p0, p1, p2, p3, t);

            // Notify when the position of the object changes
            onPositionChanged?.Invoke(objectPosition);

            yield return new WaitForEndOfFrame();
        }
    }
}
