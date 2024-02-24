using UnityEngine;

public class PanelVisibilityController : MonoBehaviour, IPanelVisibilityController
{
    private void OnValidate()
    {
        // Verifica si no hay un PanelVisibilityToggleController adjunto
        if (GetComponent<PanelVisibilityToggleController>() == null)
        {
            // Si no hay ninguno, agrega uno automáticamente
            gameObject.AddComponent<PanelVisibilityToggleController>();
        }
        PanelVisibilityUtility.EnsurePanelVisibilityToggleController(gameObject);
    }

    public void ShowPanel(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            Debug.LogError("Panel does not have a CanvasGroup component.");
        }
    }

    public void HidePanel(GameObject panel)
    {
        CanvasGroup canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            Debug.LogError("Panel does not have a CanvasGroup component.");
        }
    }
}


