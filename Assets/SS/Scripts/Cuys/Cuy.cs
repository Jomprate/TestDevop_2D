using UnityEngine;


public abstract class Cuy : MonoBehaviour, ICuyStats, ICuyBehavior, ICuyAppearance
{
    // Method called when the object is validated in the editor
    private void OnValidate()
    {
        // Try to add the component if it doesn't exist
        if (!TryGetComponent<FollowRoundedPaths_Implementation>(out var roundedPaths))
        {
            roundedPaths = gameObject.AddComponent<FollowRoundedPaths_Implementation>();
        }

        // Modify the value of the CuyName property in the FollowRoundedPaths_Implementation component
        roundedPaths.CuyName = cuyName;
    }

    [SerializeField] public string cuyName;
    [SerializeField] public float initSpeed;
    [SerializeField] public float fatigue;
    [SerializeField] Sprite image;

    // Properties region
    #region Properties

    // Property to get or set the name of the Cuy
    public string CuyName
    {
        get { return cuyName; }
        set { cuyName = value; }
    }

    // Property to get or set the initial speed of the Cuy
    public float InitSpeed
    {
        get { return initSpeed; }
        set { initSpeed = value; }
    }

    // Property to get or set the fatigue of the Cuy
    public float Fatigue
    {
        get { return fatigue; }
        set { fatigue = value; }
    }

    // Property to get or set the image of the Cuy
    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    #endregion Properties

    // Method to set the name of the Cuy
    public virtual void SetCuyName(string newName)
    {
        CuyName = newName;
    }

    // Method to set the fatigue of the Cuy
    public virtual void SetFatigue(float newFatigue)
    {
        Fatigue = newFatigue;
    }

    // Method to set the initial speed of the Cuy
    public virtual void SetInitSpeed(float newSpeed)
    {
        InitSpeed = newSpeed;
    }
}
