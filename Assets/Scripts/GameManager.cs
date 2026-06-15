
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float tiempoInicial = 60f;  // 60 segundos para el nivel
	private float tiempoActual;

	public Text textoReloj;       // Arrastra el Text del UI del reloj aquí
	public Text textoVictoria;    // Arrastra el Text "¡Ganaste!" aquí

	private bool juegoTerminado = false;

	void Start()
	{
		tiempoActual = tiempoInicial;
		textoVictoria.gameObject.SetActive(false);
		Time.timeScale = 1f;
	}

	void Update()
	{
		if (juegoTerminado) return;

		tiempoActual -= Time.deltaTime;
		textoReloj.text = "Tiempo: " + Mathf.Max(0, Mathf.CeilToInt(tiempoActual)).ToString();

		if (tiempoActual <= 0f && !juegoTerminado)
		{
			PerderJuego();
		}
	}

	public void GanarJuego()
	{
		if (juegoTerminado) return;
		juegoTerminado = true;
		textoVictoria.gameObject.SetActive(true);
		Time.timeScale = 0f;
		Invoke("RegresarAlMenuPrincipal", 5f);
	}
	void PerderJuego()
	{
		juegoTerminado = true;
		// Aquí puedes mostrar un mensaje de "Game Over" si quieres
		Invoke("RegresarAlMenuPrincipal", 5f);
	}

	void RegresarAlMenuPrincipal()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MenuPrincipal");
	}
}