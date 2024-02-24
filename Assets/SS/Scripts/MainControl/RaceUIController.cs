// Interfaz para controladores de interfaz de usuario de la carrera
using UnityEngine;



// Implementación concreta de IRaceUIController
public class RaceUIController : MonoBehaviour
{
    [SerializeField] private GameObject positionsPanel;
    [SerializeField] private GameObject resultsMenuPanel;

    public void HidePanels()
    {
        HidePanel(resultsMenuPanel);
        HidePanel(positionsPanel);
    }

    private void HidePanel(GameObject panel)
    {
        var panelVC = panel.GetComponent<IPanelVisibilityController>();
        panelVC.HidePanel(panel);
    }
}
