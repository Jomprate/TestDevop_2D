using UnityEngine;

public static class GetTotalDistanceToBeTravelledS
{
    public static void Get(Transform[] routes, out float[] toTravelRouteArray)
    {
        Vector3 p0, p1, p2, p3;
        float totalDistance = 0;
        toTravelRouteArray = new float[routes.Length];

        for (int i = 0; i < routes.Length; i++)
        {
            GetRoutePositions.Get(routes, i, out p0, out p1, out p2, out p3);
            float distance = CalculateTotalDistanceObjectWillTravel.CalculateBezierCurveLength(p0, p1, p2, p3);
            totalDistance += distance;

            if (i == 0) { distance *= 2; }

            toTravelRouteArray[i] = distance;
        }

    }
}
