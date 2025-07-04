using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem :  MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel_1");      // Carga el primer nivel
        GameManager.Instance.PuntosTotales = 0; // Reinicia el puntaje
    }


    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        UnityEditor.EditorApplication.isPlaying = false; // Solo para que se detenga en el editor
    }
    public void IrANiveles()
    {
        SceneManager.LoadScene("Niveles"); // Aseg�rate de que la escena est� a�adida en Build Settings
    }


}
