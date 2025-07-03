using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public Transform Inti;
    public GameObject BulletPrefab;

    private int Health = 3;
    private float LastShoot;
    private bool IsDead = false;

    public Animator Animator;

    void Update()
    {
        if (IsDead || Inti == null) return;

        Vector3 direction = Inti.position - transform.position;
        if (direction.x >= 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Inti.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(transform.localScale.x, 0.0f, 0.0f);
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        if (IsDead) return;

        Health -= 1;
        //Animator.SetTrigger("hurt");

        if (Health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        IsDead = true;
        Animator.SetTrigger("die");

        // Desactivar el Rigidbody2D para detener cualquier movimiento físico
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; // O RigidbodyType2D.Kinematic

        GetComponent<Collider2D>().enabled = false; // Evitar colisiones adicionales
        StartCoroutine(DestroyAfterAnimation());
    }


    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(1.5f); // Ajusta el tiempo según tu animación
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        IntiMovement inti = collision.gameObject.GetComponent<IntiMovement>();
        if (inti != null)
        {
            Animator.SetTrigger("attack");
            inti.Hit(false); // false = no cayó
        }
    }
}
