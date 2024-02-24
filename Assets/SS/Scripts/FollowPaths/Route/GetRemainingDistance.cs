using UnityEngine;

public static class GetRemainingDistanceS
{
    public static float Get(float[] toTravelRouteArray, float[] distancesTravelledByRoute)
    {
        
        float totalDistance = 0f;
        foreach (float routeDistance in toTravelRouteArray)
        {
            totalDistance += routeDistance;
        }

        float totalDistanceTravelled = 0f;
        foreach (float distanceTravelled in distancesTravelledByRoute)
        {
            totalDistanceTravelled += distanceTravelled;
        }

        float remainingDistance = totalDistance - totalDistanceTravelled;
        return Mathf.Max(0, remainingDistance);
    }
}
