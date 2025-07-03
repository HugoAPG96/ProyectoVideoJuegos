using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObstaculo : MonoBehaviour
{
    public GameObject prefabRama;
    public Transform jugador;

    private float rangoXMin = 8f;
    private float rangoXMax = 14f;
    private float rangoYMin = -1f;
    private float rangoYMax = 2f;

    private int ramaLimite = 12;
    private List<GameObject> ramasActivas = new List<GameObject>();

    void Start()
    {
        StartCoroutine(EsperarYCrearRama());
    }

    void CrearRama()
    {
        LimpiarLista();

        if (ramasActivas.Count < ramaLimite && jugador != null)
        {
            float randomX = Random.Range(rangoXMin, rangoXMax) + jugador.position.x;
            float randomY = Random.Range(rangoYMin, rangoYMax);

            GameObject nuevaRama = Instantiate(prefabRama, new Vector3(randomX, randomY, 0f), Quaternion.identity);
            ramasActivas.Add(nuevaRama);

            // Le agregamos movimiento automático
            RamaMovimiento movimiento = nuevaRama.AddComponent<RamaMovimiento>();
            movimiento.velocidad = 2f;

            Destroy(nuevaRama, 8f); // Destruye tras 8 segundos
        }
    }

    void LimpiarLista()
    {
        ramasActivas.RemoveAll(rama => rama == null);
    }

    IEnumerator EsperarYCrearRama()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CrearRama();
        }
    }

}
