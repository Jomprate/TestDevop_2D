using System.Collections.Generic;
using TMPro;


public static class UpdateRacerPositions 
{
    public static void UpdateRacerPositionsUI(List<IRacer> racers, List<TextMeshProUGUI> positionsTexts)
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
