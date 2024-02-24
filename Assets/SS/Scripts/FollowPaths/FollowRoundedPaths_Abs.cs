using System.Collections.Generic;
using UnityEngine;

public abstract class FollowRoundedPaths_Abs : MonoBehaviour, IRacer
{
    #region Fields
    protected IMovementManager movementManager;
    protected MovementLogic movementLogic;
    protected SetCuysValues setCuysValues;

    public delegate void OnPathEndEvent();
    public static event OnPathEndEvent OnPathEnd;

    public delegate void OnLapCompletedEvent(int lapsCompleted);

    public Transform package;
    public Transform[] routes;
    public string cuyName;
    public float speedModifier;
    public int selectedRoute;
    public Vector3 objectPosition;
    public Cuy cuy;
    public bool coroutineAllowed;
    public bool raceStarted = false;
    public int lapsCompleted = -1;
    public float distanceTravelled;
    public float totalDistanceToBeTravelled;
    public float[] ToTravelRouteArray;
    public float[] distancesTravelledByRoute;
    [SerializeField] protected float percentageCompleted;
    public float remainingDistance;
    public bool HasFinishedTheRace = false;
    #endregion Fields

    #region Properties
    public string CuyName
    {
        get { return cuyName; }
        set { cuyName = value; }
    }

    public int LapsCompleted
    {
        get { return lapsCompleted; }
        set { lapsCompleted = value; }
    }

    public float DistanceTravelled
    {
        get { return distanceTravelled; }
        set { distanceTravelled = value; }
    }

    public float TotalDistanceToBeTravelled
    {
        get { return totalDistanceToBeTravelled; }
        set { totalDistanceToBeTravelled = value; }
    }

    public float[] DistancesTravelledByRoute
    {
        get { return distancesTravelledByRoute; }
        set { distancesTravelledByRoute = value; }
    }

    public float PercentageCompleted
    {
        get { return percentageCompleted; }
        set { percentageCompleted = value; }
    }

    public bool CoroutineAllowed
    {
        get { return coroutineAllowed; }
        set { coroutineAllowed = value; }
    }

    public Transform[] Routes
    {
        get { return routes; }
        set { routes = value; }
    }

    public float SpeedModifier
    {
        get { return speedModifier; }
        set { speedModifier = value; }
    }

    public Cuy Cuy
    {
        get { return cuy; }
        set { cuy = value; }
    }

    public Transform TransformS => transform;

    public int SelectedRoute
    {
        get { return selectedRoute; }
        set { selectedRoute = value;  }
    }
    #endregion Properties

    // Called when the object is validated. If cuy is null, assigns the Cuy component of the GameObject.
    protected virtual void OnValidate()
    {
        if (cuy == null) cuy = GetComponent<Cuy>();
    }

    // Called at the start. Initiates initialization.
    protected virtual void Start() => Initialize();

    // Initializes the object, creates the movement manager and movement logic, gets the routes, etc.
    protected virtual void Initialize()
    {
        movementManager = new BezierMovementManager();
        movementLogic = new MovementLogic(this, this.gameObject);
        routes = GetAllRoutesFromPackage.Get(package);
        selectedRoute = 0;
        GetTotalDistanceToBeTravelledS.Get(routes, out ToTravelRouteArray);
        CalculateAllDistanceToTravel();
    }


    #region Enable/Disable
    // Called when the object is enabled. Subscribes to the StartRace event of the RaceCoordinator and gets the SetCuysValues component.
    protected virtual void OnEnable()
    {
        RaceCoordinator.StartRaceEvent += StartRace;
        setCuysValues = GetComponent<SetCuysValues>();
    }

    // Called when the object is disabled. Unsubscribes from the StartRace event of the RaceCoordinator.
    protected virtual void OnDisable()
    {
        RaceCoordinator.StartRaceEvent -= StartRace;
    }
    #endregion Enable/Disable

    // Called every frame. If coroutineAllowed is true, starts the movement coroutine.
    protected virtual void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(movementLogic.MoveAlongRoute(selectedRoute));
        }
    }

    // Starts the race if it hasn't started yet. Calls InitializeRace, enables the coroutine, and sets raceStarted to true.
    public void StartRace()
    {
        if (!raceStarted)
        {
            InitializeRace();
            coroutineAllowed = true;
            raceStarted = true;
        }
    }

    // Initializes the race, gets the routes, resets the object's position, sets Cuy values, etc.
    protected virtual void InitializeRace()
    {
        routes = GetAllRoutesFromPackage.Get(package);
        selectedRoute = 0;
        ResetSpritePosition();
        setCuysValues.SetCuyValues();
        PercentageCompleted = 0;
        distanceTravelled = 0;
        lapsCompleted = -1;
        speedModifier = cuy.InitSpeed;
        OnPathEnd += HandlePathEnd;
    }

    // Calculates the total distance to travel by summing all distances in ToTravelRouteArray.
    public void CalculateAllDistanceToTravel()
    {
        foreach (var Route in ToTravelRouteArray)
        {
            TotalDistanceToBeTravelled += Route;
        }
    }

    // Stops the race, stops all coroutines, sets coroutineAllowed to false, and resets the object's position.
    public void StopRace()
    {
        StopAllCoroutines();
        coroutineAllowed = false;
        raceStarted = false;
        ResetSpritePosition();
    }

    // Handles the path end event.
    private void HandlePathEnd()
    {
        OnPathEnd?.Invoke();
    }

    // Resets the object's position, setting position, rotation, and scale to initial values.
    protected virtual void ResetSpritePosition()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    // Gets the remaining distance to travel by summing all distances in ToTravelRouteArray and subtracting the travelled distance.
    public float GetRemainingDistance()
    {
        return GetRemainingDistanceS.Get(ToTravelRouteArray, DistancesTravelledByRoute);
    }

    // Updates the travelled distance and calculates the percentage completed based on the travelled distance and total distance to travel.
    public void UpdateDistanceTravelled(float distance)
    {
        distanceTravelled += distance;
        CalculatePercentageCompleted();
    }

    // Calculates the percentage completed of the race based on the travelled distance and total distance to travel.
    public void CalculatePercentageCompleted()
    {
        percentageCompleted = (distanceTravelled / totalDistanceToBeTravelled) * 100f;
    }


}
