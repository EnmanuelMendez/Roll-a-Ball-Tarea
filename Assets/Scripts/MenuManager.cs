using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	public void Jugar() {
		SceneManager.LoadScene ("Nivel1");
	}

	public  void Salir(){
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
