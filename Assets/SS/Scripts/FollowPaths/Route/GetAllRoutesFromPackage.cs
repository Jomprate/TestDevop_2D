using UnityEngine;

public static class GetAllRoutesFromPackage
{
    public static Transform[] Get(Transform pack)
    {
        int count = pack.childCount;
        Transform[] routes = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            routes[i] = pack.GetChild(i);
        }
        return routes;
    }
}
