using UnityEngine;

public static class GetRoutePositions
{
    public static void Get(Transform[] routes, int routeNum, out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3)
    {
        p0 = routes[routeNum].GetChild(0).position;
        p1 = routes[routeNum].GetChild(1).position;
        p2 = routes[routeNum].GetChild(2).position;
        p3 = routes[routeNum].GetChild(3).position;
    }
}
