using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path path = (Path)target;

        if (GUILayout.Button("Auto Populate Waypoints"))
        {
            AutoPopulateWaypoints();
        }
        if (GUILayout.Button("Clear Waypoints"))
        {
            ClearWaypoints();
        }
        if (GUILayout.Button("Toggle Waypoint Image"))
        {
            ToggleWaypointImage();
        }
    }

    private void AutoPopulateWaypoints()
    {
        Path path = (Path)target;

        // Clear existing waypoints
        //path.wayPoints = new Transform[path.transform.childCount];

        // Add child objects as waypoints
        for (int i = 0; i < path.transform.childCount; i++)
        {
            path.wayPoints[i] = path.transform.GetChild(i);
        }
    }

    private void ClearWaypoints()
    {
        Path path = (Path)target;
        path.wayPoints = new Transform[0];
    }

    private void ToggleWaypointImage()
    {
        Path path = (Path)target;

        for (int i = 0; i < path.transform.childCount; i++)
        {
            Transform child = path.transform.GetChild(i);
            Image imageComponent = child.GetComponent<Image>();

            if (imageComponent != null)
            {
                imageComponent.enabled = !imageComponent.enabled; // Invierte el estado actual (activado/desactivado)
            }
        }
        // Aquí puedes agregar la lógica para alternar la visibilidad de la imagen de los waypoints
        // Por ejemplo, si la imagen está asociada a un objeto en la escena, puedes activar o desactivar ese objeto.
        // O si la imagen es parte de la interfaz de usuario, puedes cambiar su visibilidad.
    }
}
