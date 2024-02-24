using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Class to coordinate the race and manage race events
[System.Serializable]
public class RaceCoordinator : MonoBehaviour
{
    public delegate void OnStartRace();
    public static event OnStartRace StartRaceEvent;
    public delegate void OnEndRace(GameObject runner);
    public static event OnEndRace EndRaceEvent;

    // Method to invoke the end of the race event
    public static void InvokeEndRaceEvent(GameObject runner)
    {
        EndRaceEvent?.Invoke(runner);
    }

    [SerializeField] public List<IRacer> racers = new List<IRacer>();
    [SerializeField] private List<TextMeshProUGUI> positionsTexts = new List<TextMeshProUGUI>();
    [SerializeField] private GameObject positionsPanel;

    private IRaceConfiguration raceConfiguration;
    private IRaceUpdater raceUpdater;

    private static GameObject firstRunnerToEndRace;
    [SerializeField] private GameObject WinnerPanel;
    [SerializeField] private TextMeshProUGUI WinnerTextTMP;

    public static int racersFinishedCount = 0;

    // Awake method to initialize components
    private void Awake()
    {
        racers = new List<IRacer>(FindObjectsOfType<FollowRoundedPaths_Implementation>());
        raceConfiguration = new RaceConfiguration(racers, positionsPanel, GetComponent<IPanelVisibilityController>());
        raceUpdater = new RaceUpdater(racers, positionsTexts);
    }

    // Update method to update the race
    private void Update()
    {
        raceUpdater.UpdateRace();
    }

    // Method to start the race
    public void StartRace()
    {
        raceConfiguration.StartRace();
        StartRaceEvent?.Invoke();
    }

    // Method to handle the end of the race
    public void EndRace(GameObject runner)
    {
        if (firstRunnerToEndRace == null)
        {
            firstRunnerToEndRace = runner;
        }

        racersFinishedCount++;

        if (racersFinishedCount == racers.Count)
        {
            var vc = WinnerPanel.GetComponent<PanelVisibilityController>();
            vc.ShowPanel(WinnerPanel);
            WinnerTextTMP.text = firstRunnerToEndRace.GetComponent<Cuy>().CuyName;
        }
    }

    #region Enabled and Disabled
    private void OnEnable()
    {
        EndRaceEvent += EndRace;
    }

    private void OnDisable()
    {
        EndRaceEvent -= EndRace;
    }
    #endregion Enabled and Disabled

    // Method to add a racer to the race
    public void AddRacer(IRacer runner)
    {
        racers.Add(runner);
    }

    // Class to configure the race
    public class RaceConfiguration : IRaceConfiguration
    {
        private readonly List<IRacer> racers;
        private readonly GameObject positionsPanel;
        private readonly IPanelVisibilityController panelVisibilityController;

        public RaceConfiguration(List<IRacer> racers, GameObject positionsPanel, IPanelVisibilityController panelVisibilityController)
        {
            this.racers = racers;
            this.positionsPanel = positionsPanel;
            this.panelVisibilityController = panelVisibilityController;
        }

        // Method to configure the race
        public void ConfigureRace()
        {
            firstRunnerToEndRace = null;

            if (panelVisibilityController != null)
            {
                panelVisibilityController.ShowPanel(positionsPanel);
            }
            else
            {
                Debug.LogError("PanelVisibilityController is null.");
            }
        }

        // Method to start the race
        public void StartRace()
        {
            ConfigureRace();
            foreach (var racer in racers)
            {
                racersFinishedCount = 0;
                racer.StopRace();
                racer.StartRace();
            }
        }
    }

    // Class to update the race
    public class RaceUpdater : IRaceUpdater
    {
        private readonly List<IRacer> racers;
        private readonly List<TextMeshProUGUI> positionsTexts;

        public RaceUpdater(List<IRacer> racers, List<TextMeshProUGUI> positionsTexts)
        {
            this.racers = racers;
            this.positionsTexts = positionsTexts;
        }

        // Method to update the race
        public void UpdateRace()
        {
            UpdateRacerPositions.UpdateRacerPositionsUI(racers, positionsTexts);

        }

        public  void UpdateRacerPositionsUI(List<IRacer> racers, List<TextMeshProUGUI> positionsTexts)
        {
            // Obtener el porcentaje recorrido para cada corredor
            List<float> progressPercentages = new List<float>();
            foreach (var racer in racers)
            {
                float percentageCompleted = (racer.DistanceTravelled / racer.TotalDistanceToBeTravelled) * 100f;

                if (percentageCompleted > 100f)
                {
                    percentageCompleted = 100f;
                }
                progressPercentages.Add(percentageCompleted);
            }

            // Ordenar los corredores por el porcentaje recorrido (de mayor a menor)
            for (int i = 0; i < racers.Count - 1; i++)
            {
                for (int j = i + 1; j < racers.Count; j++)
                {
                    if (progressPercentages[j] > progressPercentages[i])
                    {
                        // Intercambiar los corredores
                        var tempRacer = racers[i];
                        racers[i] = racers[j];
                        racers[j] = tempRacer;

                        // Intercambiar los porcentajes de progreso
                        var tempPercentage = progressPercentages[i];
                        progressPercentages[i] = progressPercentages[j];
                        progressPercentages[j] = tempPercentage;
                    }
                }
            }


            // Actualizar los textos de posición
            for (int i = 0; i < racers.Count; i++)
            {
                positionsTexts[i].text = racers[i].CuyName;
            }

            
        }
    }
}
