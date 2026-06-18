using UnityEngine;

public class MoverObstaculo : MonoBehaviour
{
    public Vector3 puntoA;
    public Vector3 puntoB;
    public float velocidad = 2f;

    void Update()
    {
        float t = Mathf.PingPong(Time.time * velocidad, 1f);
        transform.position = Vector3.Lerp(puntoA, puntoB, t);
    }
}
