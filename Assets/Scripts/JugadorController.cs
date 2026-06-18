using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JugadorController : MonoBehaviour
{
    private Rigidbody rb;
    private GameManager gameManager;

    public float velocidad = 20f;
    private int contador;
    public int totalColeccionables = 12;
    public Text textoContador, textoGanar;
    public GameObject textoVictoria;

    void Start()
    {
        if (velocidad <= 0f)
        {
            velocidad = 20f;
        }

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Buscar textos automáticamente si no están asignados
        if (textoContador == null)
        {
            GameObject obj = GameObject.Find("TextoContador");
            if (obj != null) textoContador = obj.GetComponent<Text>();
        }

        contador = 0;
        if (textoContador != null)
            setTextoContador();

        if (textoGanar != null)
            textoGanar.text = "";

        gameManager = FindObjectOfType<GameManager>();

        if (PlayerPrefs.HasKey("Velocidad"))
        {
            velocidad = PlayerPrefs.GetFloat("Velocidad");
        }
    }

    void FixedUpdate()
    {
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);
        rb.AddForce(movimiento * velocidad, ForceMode.Force);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            contador++;
            setTextoContador();
        }
    }

    void setTextoContador()
    {
        if (textoContador != null)
            textoContador.text = "Contador: " + contador.ToString() + " / " + totalColeccionables.ToString();

        if (contador >= totalColeccionables)
        {
            if (gameManager != null)
                gameManager.GanarJuego();
        }
    }
}
