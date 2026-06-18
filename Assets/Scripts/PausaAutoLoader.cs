using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Se ejecuta automáticamente en TODAS las escenas.
/// Si la escena no es MenuPrincipal ni Opciones, crea un PausaManager.
/// No necesitas añadir nada manualmente a las escenas de juego.
/// </summary>
public class PausaAutoLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnSceneLoaded()
    {
        SceneManager.sceneLoaded += OnNewSceneLoaded;
        // También ejecutar para la escena actual
        CheckAndCreate();
    }

    static void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckAndCreate();
    }

    static void CheckAndCreate()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // No añadir pausa en menús
        if (sceneName == "MenuPrincipal" || sceneName == "Opciones")
            return;

        // Si ya existe un PausaManager, no crear otro
        if (Object.FindObjectOfType<PausaManager>() != null)
            return;

        GameObject pausaObj = new GameObject("PausaManager_Auto");
        pausaObj.AddComponent<PausaManager>();
    }
}
