using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float tiempoInicial = 60f;
	private float tiempoActual;
	public Text textoReloj;
	public Text textoVictoria;
	private bool juegoTerminado = false;

	void Start()
	{
		Debug.Log("GameManager iniciado en " + gameObject.name);
		tiempoActual = tiempoInicial;
		if (textoVictoria != null)
			textoVictoria.gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

	void Update()
	{
		if (juegoTerminado) return;
		tiempoActual -= Time.deltaTime;
		textoReloj.text = "Tiempo: " + Mathf.Max(0, Mathf.CeilToInt(tiempoActual)).ToString();
		if (tiempoActual <= 0f && !juegoTerminado)
			PerderJuego();
	}

	public void GanarJuego()
	{
		if (juegoTerminado) return;
		juegoTerminado = true;
		textoVictoria.text = "¡Ganaste!";
		textoVictoria.gameObject.SetActive(true);
		Time.timeScale = 0f;
		// Usar Invoke con tiempo real (sin corrutina)
		Invoke("RegresarAlMenuPrincipal", 5f);
		Debug.Log("GanarJuego ejecutado. Se llamará a RegresarAlMenuPrincipal en 5 segundos.");
	}

	void PerderJuego()
	{
		juegoTerminado = true;
		Invoke("RegresarAlMenuPrincipal", 5f);
		Debug.Log("PerderJuego ejecutado.");
	}

	void RegresarAlMenuPrincipal()
	{
		Debug.Log("RegresarAlMenuPrincipal: Cargando MenuPrincipal (índice 0)...");
		Time.timeScale = 1f;
		// Cargar por índice (0) que es MenuPrincipal según Build Settings
		SceneManager.LoadScene("MenuPrincipal");
		// Este log no se verá si la escena se descarga inmediatamente, pero ayuda
		Debug.Log("LoadScene ejecutado (si ves esto, la carga empezó)");
	}
}