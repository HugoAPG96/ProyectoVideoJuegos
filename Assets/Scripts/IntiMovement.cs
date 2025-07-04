using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Scripts
{
    public class IntiMovement : MonoBehaviour
    {
        public float Speed;
        public float JumpForce;
        public GameObject BulletPrefab;
        public Transform RespawnPoint; // Punto de reaparición después de ser golpeado
        public AudioClip JumpSound;
        public LayerMask groundLayer;//para detectar colision

        //para saltos
        private int jumpCount = 0;
        private bool isJumping = false;

        private bool recibeDanio;

        public float DoubleJumpForce = 7f;
        public AudioClip DoubleJumpSound;

        public float fuerzaRebote = 5f;

        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 2f;

        private Rigidbody2D Rigidbody2D;
        private Animator Animator;
        private AudioSource AudioSource;

        private float Horizontal;
        private bool Grounded;
        private float LastShoot;
        //private int Health = 3;
        private bool IsDead = false;


        private bool IsInvincible = false;
        private float InvincibilityDuration = 2f; // Duración de invencibilidad



        private void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            AudioSource = GetComponent<AudioSource>();

        }

        private void Update()
        {
            if (IsDead) return;

            Horizontal = Input.GetAxisRaw("Horizontal");

            // Animación y dirección
            if (Horizontal < 0.0f)
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else if (Horizontal > 0.0f)
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            Animator.SetBool("running", Horizontal != 0.0f);

            // Detección de suelo
            Debug.DrawRay(transform.position, Vector3.down * 0.3f, Color.red);
            Grounded = Physics2D.Raycast(transform.position, Vector3.down, 1.5f);

            // Si está en el suelo, reinicia los saltos
            if (Grounded)
            {
                jumpCount = 0;
            }

            // SALTO o DOBLE SALTO
            if (Input.GetKeyDown(KeyCode.W) && jumpCount < 2)
            {
                Jump();
                jumpCount++;

                // Sonido para salto y doble salto
                if (jumpCount == 1 && JumpSound != null)
                    AudioSource.PlayOneShot(JumpSound);
                else if (jumpCount == 2 && DoubleJumpSound != null)
                    AudioSource.PlayOneShot(DoubleJumpSound);

                isJumping = true;
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                isJumping = false;
            }

            // DISPARO
            if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
            {
                Shoot();
                LastShoot = Time.time;
            }

            // CAÍDA FUERA DEL MAPA
            if (transform.position.y < -10f)
            {
                Hit(true); // Pierde una vida al caer
            }

        }
    

        private void FixedUpdate()
        {
            if (IsDead) return;

            // Movimiento horizontal
            Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);

            // Ajuste de gravedad según estado
            if (isJumping || Rigidbody2D.linearVelocity.y > 0f)
            {
                // Mientras salta (subiendo), usa gravedad suave
                Rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (Rigidbody2D.linearVelocity.y < 0f)
            {
                // Al caer rápido, usa gravedad intensa
                Rigidbody2D.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }

            if (Grounded)
            {
                isJumping = false;
            }
        }

        private void Jump()
        {
            // Reinicia la velocidad vertical para evitar acumulación de fuerza
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, 0);

            // Aplica fuerza hacia arriba
            Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

            // Reproduce sonido de salto si está asignado
            if (JumpSound != null)
            {
                AudioSource.PlayOneShot(JumpSound);
            }
        }

        private void Shoot()
        {
            Vector3 direction = transform.localScale.x == 1.0f ? Vector3.right : Vector3.left;
            GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
            bullet.GetComponent<BulletScript>().SetDirection(direction);
        }

        public void Hit(bool isFall)
        {
            if (IsDead || IsInvincible) return;
            Animator.SetTrigger("hurt");
            StartCoroutine(EnableInvincibility());

            StartCoroutine(HandleDeath(isFall));
        }

        private IEnumerator EnableInvincibility()
        {
            IsInvincible = true;
            yield return new WaitForSeconds(InvincibilityDuration);
            IsInvincible = false;
        }


        public void Die()
        {
            IsDead = true;
            Animator.SetTrigger("die");
            //StartCoroutine(RestartGame());
        }

        private IEnumerator HandleDeath(bool isFall)
        {
            yield return new WaitForSeconds(0.5f); // animación de daño
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoseLife(isFall);
            }
            else
            {
                Debug.LogError("GameManager no encontrado.");
            }
        }


        IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ResetPosition()
        {
            if (RespawnPoint != null)
                transform.position = RespawnPoint.position;
            else
                transform.position = Vector3.zero;

            Rigidbody2D.linearVelocity = Vector2.zero; // Detiene cualquier movimiento
            Animator.SetTrigger("hurt"); // Muestra animación de daño
        }

    }
}
