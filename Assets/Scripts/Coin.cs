using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int valor = 1;
    //public GameManager gameManager;
    //public AudioManager audioManager; // Referencia al AudioManager
    public AudioClip coinSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(coinSFX);
        }

    }
}