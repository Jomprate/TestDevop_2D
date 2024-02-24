using System.Collections;
using UnityEngine;

public class MovementLogic
{
    private IRacer racer;
    private GameObject runner;

    public MovementLogic(IRacer followRoundedPaths, GameObject runner)
    {
        this.racer = followRoundedPaths;
        this.runner = runner; // Asignar la referencia al cuy

    }
    public IEnumerator MoveAlongRoute(int routeNum)
    {
        // Inicializar la matriz distancesTravelledByRoute si aún no se ha inicializado
        if (racer.DistancesTravelledByRoute == null || racer.DistancesTravelledByRoute.Length != racer.Routes.Length)
        {
            racer.DistancesTravelledByRoute = new float[racer.Routes.Length];

        }

        // Variable para la distancia total recorrida por el cuy en todas las rutas
        float totalDistanceTravelled = 0;

        float t = 0; // Parámetro para la interpolación de Bezier
                     // Deshabilitar la corrutina para evitar que se ejecute simultáneamente
        racer.CoroutineAllowed = false;

        Vector3 p0, p1, p2, p3;
        GetRoutePositions.Get(racer.Routes, routeNum, out p0, out p1, out p2, out p3);

        Vector3 previousPosition = CalculateObjectPositionAtCurrentPoint.Calculate(p0, p1, p2, p3, t);

        float distanceTravelledOnRoute = 0;
        // Calcular la distancia total que el objeto recorrerá en la ruta
        float distance = CalculateTotalDistanceObjectWillTravel.Calculate(p0, p1, p2, p3);
        distanceTravelledOnRoute += distance;


        racer.SpeedModifier = racer.SpeedModifier * racer.Cuy.Fatigue;
        // Calcular la velocidad del objeto basada en la distancia total
        float speed = CalculateSpeedOfTheObject.Calculate(distance, racer.SpeedModifier, racer.Cuy.Fatigue);


        bool reachedHalfway = false; // Indica si el objeto ha alcanzado la mitad del camino

        // Bucle para mover gradualmente el objeto a lo largo de la ruta
        while (t < 1)
        {
            t += Time.deltaTime / speed; // Incrementar el parámetro de tiempo

            //Vector3 previousPosition = racer.transform.position;
            // Calcular la posición del objeto en el punto actual de la ruta
            Vector3 objectPosition = CalculateObjectPositionAtCurrentPoint.Calculate(p0, p1, p2, p3, t);


            Vector3 currentPosition = objectPosition;
            // Actualizar la posición del objeto
            //racer.transform.position = objectPosition;

            // Calcular la distancia recorrida entre la posición anterior y la posición actual
            float distanceTraveledThisFrame = Vector3.Distance(currentPosition, previousPosition);
            racer.DistancesTravelledByRoute[routeNum] += distanceTraveledThisFrame;

            // Actualizar la distancia recorrida total
            racer.UpdateDistanceTravelled(distanceTraveledThisFrame);

            // Actualizar la distancia total recorrida por el cuy en todas las rutas
            totalDistanceTravelled += distanceTraveledThisFrame;

            previousPosition = currentPosition;

            // Calcular y ajustar la rotación del objeto para que apunte en la dirección correcta
            Vector3 direction = CalculateCurrentDirection.Calculate(p0, p1, p2, p3, t);
            CalculateObjectRotationAngleToPointToCorrectDirection.Calculate(direction, racer.TransformS);

            // Calcular la distancia recorrida en esta iteración


            // Calcular la distancia restante que le falta al objeto para terminar la ruta
            float remainingDistance = distance - distanceTravelledOnRoute;

            // Actualizar la distancia recorrida total
            racer.UpdateDistanceTravelled(remainingDistance);

            // Verificar si se ha completado una vuelta
            if (t >= 1)
            {
                // Reiniciar la distancia recorrida en la ruta actual para la próxima vuelta
                distanceTravelledOnRoute = 0;

                // Incrementar el contador de vueltas completadas
                racer.LapsCompleted++;

                // Verificar si se ha completado la carrera
                if (racer.LapsCompleted == 6)
                {
                    // Detener la carrera
                    FinishRace();
                }
            }

            racer.TransformS.position = objectPosition;
            CalculateObjectRotationAngleToPointToCorrectDirection.Calculate(direction, racer.TransformS);
            reachedHalfway = CheckIfHalfwayPointHasBeenReachedToAdjustTheObjectScale.Check(routeNum, t, reachedHalfway, racer.TransformS);

            // Esperar al siguiente frame
            yield return null;
        }

        // Seleccionar la siguiente ruta en la lista circular de rutas
        racer.SelectedRoute = (routeNum + 1) % racer.Routes.Length;
        racer.CoroutineAllowed = true;
    }

    // Método para finalizar la carrera
    private void FinishRace()
    {
        racer.StopRace();
        // Invocar el evento de finalización de la carrera
        RaceCoordinator.InvokeEndRaceEvent(runner);
    }

}