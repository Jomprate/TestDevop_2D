using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelVisibilityUtility : MonoBehaviour
{
    public static void EnsurePanelVisibilityToggleController(GameObject gameObject)
    {
        // Verifica si no hay un PanelVisibilityToggleController adjunto
        if (gameObject.GetComponent<PanelVisibilityToggleController>() == null)
        {
            // Si no hay ninguno, agrega uno automáticamente
            gameObject.AddComponent<PanelVisibilityToggleController>();
        }
    }
}
