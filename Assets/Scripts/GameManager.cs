using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public float tiempoInicial = 60f;
	private float tiempoActual;
	public Text textoReloj;
	public Text textoVictoria;
	private bool juegoTerminado = false;
	private int siguienteNivelIndex;

	void Start()
	{
		Debug.Log("GameManager iniciado en " + gameObject.name);
		tiempoActual = tiempoInicial;

		// Buscar textos automáticamente si no están asignados
		if (textoReloj == null)
		{
			GameObject obj = GameObject.Find("TextoReloj");
			if (obj != null) textoReloj = obj.GetComponent<Text>();
		}
		if (textoVictoria == null)
		{
			GameObject obj = GameObject.Find("TextoVictoria");
			if (obj != null) textoVictoria = obj.GetComponent<Text>();
		}

		if (textoVictoria != null)
			textoVictoria.gameObject.SetActive(false);

		Time.timeScale = 1f;
		siguienteNivelIndex = SceneManager.GetActiveScene().buildIndex + 1;
	}

	void Update()
	{
		if (juegoTerminado) return;
		tiempoActual -= Time.deltaTime;
		if (textoReloj != null)
			textoReloj.text = "Tiempo: " + Mathf.Max(0, Mathf.CeilToInt(tiempoActual)).ToString();
		if (tiempoActual <= 0f && !juegoTerminado)
			PerderJuego();
	}

	public void GanarJuego()
	{
		if (juegoTerminado) return;
		juegoTerminado = true;
		if (textoVictoria != null)
		{
			textoVictoria.text = "¡Ganaste!";
			textoVictoria.gameObject.SetActive(true);
		}
		Time.timeScale = 0f;
		StartCoroutine(EsperarYCargarSiguiente());
	}

	void PerderJuego()
	{
		juegoTerminado = true;
		if (textoVictoria != null)
		{
			textoVictoria.text = "¡Tiempo agotado!";
			textoVictoria.gameObject.SetActive(true);
		}
		Time.timeScale = 0f;
		StartCoroutine(EsperarYReiniciar());
	}

	IEnumerator EsperarYCargarSiguiente()
	{
		yield return new WaitForSecondsRealtime(3f);
		Time.timeScale = 1f;
		if (siguienteNivelIndex < SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(siguienteNivelIndex);
		}
		else
		{
			SceneManager.LoadScene("MenuPrincipal");
		}
	}

	IEnumerator EsperarYReiniciar()
	{
		yield return new WaitForSecondsRealtime(3f);
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
