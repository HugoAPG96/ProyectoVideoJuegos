using Assets.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public IntiMovement intiMovement; // Referencia al script de movimiento del jugador
    public int PuntosTotales { get; private set; }
    private int puntosTotales;

    private int vidas = 3;
    public int barraVida = 3; // Para la barra de vida

    public HUD hud;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Este GameManager se mantiene entre escenas
        }
        else
        {
            Destroy(gameObject); // Si ya hay uno, destruye este
        }
    }


    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);
    }

    public bool RecuperarVida()
    {
        if (vidas == 3)
        {
            return false;
        }

        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
    }

    private void Start()
    {
        if (intiMovement == null)
        {
            intiMovement = FindObjectOfType<IntiMovement>();
        }

        if (hud == null)
        {
            hud = FindObjectOfType<HUD>();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Buscar nuevamente referencias de la escena cargada
        hud = FindObjectOfType<HUD>();
        intiMovement = FindObjectOfType<IntiMovement>();
    }


    public void LoseLife(bool isFall)
    {
        barraVida--;
        if (barraVida == 0)
        {
            vidas--; // Si la barra de vida está en 0, se pierde una vida directamente
            barraVida = 3; // Reinicia la barra de vida
        }

        if (vidas >= 0 && vidas < 3 && hud != null)
        {
            hud.DesactivarVida(vidas);
        }

        intiMovement = FindObjectOfType<IntiMovement>();

        if (vidas <= 0)
        {
            if (intiMovement != null)
            {
                intiMovement.Die();
                Debug.Log("¡GAME OVER!");
                intiMovement.StartCoroutine(GameOverDelay());
            }
            else
            {
                Debug.LogWarning("No se encontró al jugador para GAME OVER.");
                ResetGame(); // Si no hay jugador, reinicia directamente
            }
        }
        else
        {
            if (intiMovement != null)
            {
                if (isFall)
                {
                    Debug.Log("¡Caíste! Retrocediendo al punto seguro...");
                    intiMovement.ResetPosition();
                }
                else
                {
                    Debug.Log("¡Has perdido una vida, continúa desde aquí!");
                }
            }
            else
            {
                Debug.LogWarning("Jugador no encontrado después de perder una vida.");
            }
        }
    }


    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(2f); // espera 2 segundos para ver la animación
        ResetGame();       // Aquí podrías cargar una escena de Game Over si lo deseas
    }

    public void ResetGame()
    {
        vidas = 3;
        PuntosTotales = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
