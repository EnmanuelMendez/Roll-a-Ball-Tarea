using UnityEngine;

/// <summary>
/// Añade este script a cualquier rampa para que la bola pueda subirla.
/// Configura un PhysicMaterial con alta fricción y sin rebote.
/// </summary>
public class ConfigurarRampa : MonoBehaviour
{
    void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            PhysicMaterial mat = new PhysicMaterial("RampaMaterial");
            mat.dynamicFriction = 1f;
            mat.staticFriction = 1f;
            mat.frictionCombine = PhysicMaterialCombine.Maximum;
            mat.bounceCombine = PhysicMaterialCombine.Minimum;
            mat.bounciness = 0f;
            col.material = mat;
        }
    }
}
