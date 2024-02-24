using System;
using System.Collections;
using UnityEngine;

public interface IMovementManager
{
    IEnumerator MoveAlongRoute(Transform[] route, int routeIndex, float speedModifier, Cuy cuy, Action<Vector3> onPositionChanged);
}

