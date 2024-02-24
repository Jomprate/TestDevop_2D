using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    public Transform[] wayPoints;

    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * wayPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * wayPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * wayPoints[2].position + Mathf.Pow(t, 3) * wayPoints[3].position;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(gizmosPosition, 10f);
        }

        Gizmos.DrawLine(new Vector2(wayPoints[0].position.x, wayPoints[0].position.y), new Vector2(wayPoints[1].position.x, wayPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(wayPoints[2].position.x, wayPoints[2].position.y), new Vector2(wayPoints[3].position.x, wayPoints[3].position.y));

    }
}