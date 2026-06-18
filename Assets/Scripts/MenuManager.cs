using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Scenes");
    }

    public void CargarNivel(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void CargarNivel1()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void CargarNivel2()
    {
        SceneManager.LoadScene("Nivel2");
    }

    public void CargarNivel3()
    {
        SceneManager.LoadScene("Nivel3");
    }

    public void CargarNivel4()
    {
        SceneManager.LoadScene("Nivel4");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void Salir()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
