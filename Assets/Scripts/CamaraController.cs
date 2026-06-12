using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour {

	public GameObject jugador;

	//registra la diferencia entre la posicion de la camara y la del jugador
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		//diferencia entre la posicion de la camara y la del jugador
		offset = transform.position - jugador.transform.position;
	}

	//se ejecuta cada frame, pero despues de haber procesado todo. es mas exacto para la camara
	void LateUpdate(){
		//actualizo la posicion de la camara
		transform.position = jugador.transform.position + offset;
	}



	// Update is called once per frame
	void Update () {
		
	}
}
