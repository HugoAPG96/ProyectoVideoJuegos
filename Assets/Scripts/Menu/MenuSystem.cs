using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem :  MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel_1");
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        UnityEditor.EditorApplication.isPlaying = false; // Solo para que se detenga en el editor
    }
    public void IrANiveles()
    {
        SceneManager.LoadScene("Niveles"); // Asegúrate de que la escena esté añadida en Build Settings
    }


}
