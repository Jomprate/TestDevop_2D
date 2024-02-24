using UnityEngine;

public interface ICuyCache
{
    Cuy_O[] GetAllCuys(); // Cambiado de Cuy a Cuy_O
}

public class CuyCache : MonoBehaviour, ICuyCache
{
    // Instancia única de CuyCache
    private static CuyCache instance;

    // Variable para almacenar los cuyes en caché
    private Cuy_O[] cachedCuys; // Cambiado de Cuy a Cuy_O

    // Propiedad estática para acceder a la instancia única de CuyCache
    public static CuyCache Instance
    {
        get
        {
            // Si la instancia aún no ha sido asignada, buscarla en la escena
            if (instance == null)
            {
                instance = FindObjectOfType<CuyCache>();

                // Si no se encuentra, crear un nuevo GameObject y agregar CuyCache como componente
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(CuyCache).Name);
                    instance = singletonObject.AddComponent<CuyCache>();
                }
            }
            return instance;
        }
    }

    // Implementación de ICuyCache para obtener todos los cuyes
    public Cuy_O[] GetAllCuys() // Cambiado de Cuy a Cuy_O
    {
        // Si los cuyes ya están en caché, retornarlos directamente
        if (cachedCuys != null)
        {
            return cachedCuys;
        }

        // Si no están en caché, buscarlos y almacenarlos en caché para futuros usos
        cachedCuys = Object.FindObjectsOfType<Cuy_O>(); // Cambiado de Cuy a Cuy_O
        return cachedCuys;
    }

    // Método para limpiar la caché
    public void ClearCache()
    {
        cachedCuys = null;
    }

    // Método Awake para garantizar que solo haya una instancia de CuyCache
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
