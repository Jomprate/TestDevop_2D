using UnityEngine;

// Class to handle exceptions
public class ExceptionHandler
{
    // Method to handle exceptions and log errors
    public static void HandleException(string context, System.Exception ex)
    {
        Debug.LogError($"Error in {context}: {ex.Message}");
    }
}

public class SetCuysValues : MonoBehaviour
{
    private ICuyValueGenerator valueGenerator;

    // Method called when the object is validated in the editor
    private void OnValidate()
    {
        ConfigureValueGenerator();
    }

    // Method to configure the value generator component
    private void ConfigureValueGenerator()
    {
        if (valueGenerator == null)
        {
            valueGenerator = GetComponent<RandomCuyValueGenerator>();

            // Add the value generator component if not found
            if (valueGenerator == null)
            {
                valueGenerator = gameObject.AddComponent<RandomCuyValueGenerator>();
            }
        }
    }

    // Method to set random speed and fatigue values for all guinea pigs
    public void SetCuyValues()
    {
        // Exception handling in case of errors while getting the guinea pigs
        try
        {
            var cuys = CuyCache.Instance.GetAllCuys(); // Use the singleton instance of CuyCache
            if (cuys != null)
            {
                foreach (var cuy in cuys)
                {
                    SetRandomValues(cuy);
                }
            }
            else
            {
                Debug.LogWarning("No cuys found in the cache.");
            }
        }
        catch (System.Exception ex)
        {
            ExceptionHandler.HandleException("SetCuysValues", ex);
        }
    }

    // Method to set random speed and fatigue values for a single guinea pig
    private void SetRandomValues(Cuy_O cuy)
    {
        // Exception handling in case of errors while generating random values
        try
        {
            if (valueGenerator == null)
            {
                valueGenerator = GetComponent<RandomCuyValueGenerator>();
                return; // Exit the method if the generator is null
            }

            cuy.SetInitSpeed(valueGenerator.GenerateSpeed());
            cuy.SetFatigue(valueGenerator.GenerateFatigue());
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error generating random values for {cuy.CuyName}: {ex.Message}");
        }
    }

    // Method called on object start to set guinea pig values
    void Start() => SetCuyValues();
}

// Interface to generate random speed and fatigue values
public interface ICuyValueGenerator
{
    float GenerateSpeed();
    float GenerateFatigue();
}
