using UnityEngine;
using System.Collections.Generic;

public class RepetidorArboles : MonoBehaviour
{
    public GameObject arbolPrefab; // Prefab del obstáculo

    private void Start()
    {
        CrearArbol();
    }

    private void Update()
    {

    }

    public void CrearArbol()
    {
        GameObject a = Instantiate(arbolPrefab) as GameObject;
        a.transform.position = new Vector3(-0.78f, -0.40f, 0f);
    }
}
