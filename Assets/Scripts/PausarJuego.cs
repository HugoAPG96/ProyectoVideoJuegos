using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa; // Reference to the pause menu UI
    public bool juegoPausado = false; // Flag to check if the game is paused

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(juegoPausado)
                {
                    Reanudar();
                }
                else
                {
                    Pausar();
                }
            }
     
        }
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void IrMenuPrincipal()
    {
        Time.timeScale = 1f; // Asegúrate de reactivar el tiempo
        SceneManager.LoadScene("Menu"); // Cambia "Menu" por el nombre exacto de tu escena
    }

}
