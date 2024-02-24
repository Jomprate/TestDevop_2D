using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPath : MonoBehaviour
{
    [SerializeField] Color rayColor = Color.white;
    public List<Transform> wayPointsList = new List<Transform>();
    [SerializeField] Transform wayPointArray;
    
    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        wayPointArray = GetComponentInChildren<Transform>();
        wayPointsList.Clear();
        foreach (Transform wayPoint in wayPointArray)
        {
            if (wayPoint != this.transform) 
            {
                wayPointsList.Add(wayPoint);
                
            }
        }

        for (int i = 0; i < wayPointsList.Count; i++)
        {
            Vector3 position = wayPointsList[i].position;
            //Gizmos.color = rayColor;

            if (i > 0)
            {
                Gizmos.DrawLine(wayPointsList[i - 1].position, wayPointsList[i].position);
            }
        }
        
    }
}
