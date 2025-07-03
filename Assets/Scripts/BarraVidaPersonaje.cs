using UnityEngine;
using UnityEngine.UI;

public class BarraVidaPersonaje : MonoBehaviour
{
    public Image rellenoBarraVida;
    private GameManager playerController;
    private float vidaMaxima;

    void Start()
    {
        playerController = GameManager.Instance;

        vidaMaxima = playerController.barraVida;
    }

    private void Update()
    {
        rellenoBarraVida.fillAmount = playerController.barraVida / vidaMaxima;
    }
}
