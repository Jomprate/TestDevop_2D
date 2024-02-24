
using UnityEngine;

public interface IRace
{
    
}
public interface IRaceConfigurable
{
    void ConfigureRace();
    void StartRace();
    void EndRace(); // Nuevo método para finalizar la carrera
}

public interface IRaceUpdatable
{
    void UpdateRace();
}

public interface IRacer
{
    public void StartRace();
    public void StopRace();
    public void UpdateDistanceTravelled(float distance);
    public int SelectedRoute { get; set; }
    public Transform TransformS { get; } // Transform del CUY
    public  Cuy Cuy { get; set; } // Cuy actualmente en carr
    public Transform[] Routes { get; set; }
    public float SpeedModifier { get; set; }
    public int LapsCompleted { get; set; }
    public float DistanceTravelled { get; set; }
    public float TotalDistanceToBeTravelled { get; set; }
    public float[] DistancesTravelledByRoute { get; set; }
    public float PercentageCompleted { get; set; }
    public string CuyName { get; set; }
    public bool CoroutineAllowed { get; set; }
}

public interface IRaceConfiguration
{
    void ConfigureRace();
    void StartRace();
    
}

public interface IRaceUpdater
{
    void UpdateRace();
}