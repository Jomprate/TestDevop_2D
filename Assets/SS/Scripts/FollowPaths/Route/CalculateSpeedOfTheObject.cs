public static class CalculateSpeedOfTheObject
{
    public static float Calculate(float distance, float speedModifier, float fatigueState)
    {
        if(fatigueState > 0)
        {
            speedModifier = speedModifier * fatigueState;
            // speedModifier = speedModifier * 0.90f;
        }

        return distance / speedModifier;
    }
}
