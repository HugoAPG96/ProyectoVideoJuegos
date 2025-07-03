using Assets.Scripts;
using UnityEngine;

public class RamaMovimiento : MonoBehaviour
{
    public float velocidad = 2f;

    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime);
    }
}
