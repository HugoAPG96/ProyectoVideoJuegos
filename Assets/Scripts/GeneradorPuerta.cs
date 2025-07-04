using UnityEngine;

public class GeneradorPuerta : MonoBehaviour
{
    public GameObject prefabPuerta;
    public int monedasNecesarias = 10;
    public Vector2 offset = new Vector2(2f, 0);
    private bool puertaCreada = false;

    void Update()
    {
        if (!puertaCreada && GameManager.Instance.PuntosTotales >= monedasNecesarias)
        {
            Vector2 posicionJugador = GameObject.FindGameObjectWithTag("Player").transform.position;
            Instantiate(prefabPuerta, posicionJugador + offset, Quaternion.identity);
            puertaCreada = true;
        }
    }
}
