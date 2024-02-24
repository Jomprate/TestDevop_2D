using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour, IMainControl
{
    #region Fields
    public delegate void OnStartRaceConfiguration();
    public static event OnStartRaceConfiguration StartRaceConfigurationEvent;

    [SerializeField] private RaceCoordinator raceCoordinator;
    [SerializeField] private Button startRace;
    [SerializeField] private Button resetRace;
    [SerializeField] private RaceUIController raceUIController; // Utilizamos una interfaz en lugar de una clase concreta

    public static MainControl Instance { get; private set; }
    #endregion Fields

    #region Singleton
    private void SetInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Ya existe una instancia de MainControl. No se deberían crear múltiples instancias.");
            Destroy(this);
        }
    }
    #endregion Singleton

    #region Methods
    private void Awake() => SetInstance();

    void Start()
    {
        startRace.onClick.AddListener(StartRace);
        resetRace.onClick.AddListener(ResetRace);
    }

    public void StartRace()
    {
        if (StartRaceConfigurationEvent != null)
        {
            StartRaceConfigurationEvent();
        }
        raceUIController.HidePanels();
        raceCoordinator.StartRace();
    }

    public void ResetRace()
    {
        if (StartRaceConfigurationEvent != null)
        {
            StartRaceConfigurationEvent();
        }
        raceUIController.HidePanels();
        StartRace();
    }
    #endregion Methods
}
