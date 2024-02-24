using UnityEngine;

public class RandomCuyValueGenerator : MonoBehaviour, ICuyValueGenerator
{
    public float GenerateSpeed()
    {
        float speed = Random.Range(180f, 230f);
        return speed;
    }

    public float GenerateFatigue()
    {
        float fatigue = Random.Range(0.9f, 0.98f);
        return fatigue;
    }
}
