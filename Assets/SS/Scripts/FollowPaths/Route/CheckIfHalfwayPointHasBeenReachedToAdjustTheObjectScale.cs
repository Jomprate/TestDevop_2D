using UnityEngine;

public static class CheckIfHalfwayPointHasBeenReachedToAdjustTheObjectScale
{
    public static bool Check(int routeNum, float t, bool reachedHalfway, Transform transform)
    {
        if ((routeNum == 2 || routeNum == 4) && t >= 0.5f && !reachedHalfway)
        {
            Vector3 newScale = transform.localScale;
            newScale.y = -newScale.y;
            transform.localScale = newScale;
            reachedHalfway = true;
        }

        return reachedHalfway;
    }
}
