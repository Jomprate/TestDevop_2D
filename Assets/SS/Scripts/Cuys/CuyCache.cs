using UnityEngine;

public interface ICuyCache
{
    Cuy_O[] GetAllCuys(); // Cambiado de Cuy a Cuy_O
}

public class CuyCache : MonoBehaviour, ICuyCache
{
    // Instancia �nica de CuyCache
    private static CuyCache instance;

    // Variable para almacenar los cuyes en cach�
    private Cuy_O[] cachedCuys; // Cambiado de Cuy a Cuy_O

    // Propiedad est�tica para acceder a la instancia �nica de CuyCache
    public static CuyCache Instance
    {
        get
        {
            // Si la instancia a�n no ha sido asignada, buscarla en la escena
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

    // Implementaci�n de ICuyCache para obtener todos los cuyes
    public Cuy_O[] GetAllCuys() // Cambiado de Cuy a Cuy_O
    {
        // Si los cuyes ya est�n en cach�, retornarlos directamente
        if (cachedCuys != null)
        {
            return cachedCuys;
        }

        // Si no est�n en cach�, buscarlos y almacenarlos en cach� para futuros usos
        cachedCuys = Object.FindObjectsOfType<Cuy_O>(); // Cambiado de Cuy a Cuy_O
        return cachedCuys;
    }

    // M�todo para limpiar la cach�
    public void ClearCache()
    {
        cachedCuys = null;
    }

    // M�todo Awake para garantizar que solo haya una instancia de CuyCache
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
