using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.18f;
    float nextFire = 0f;

    public AudioSource audioSource;
    public AudioClip tiroSom;
    public AudioClip explosaoSom;

    void Update()
    {
        bool fire = false;

        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.isPressed) fire = true;
        }

        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.isPressed) fire = true;
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.isPressed) fire = true;
        }

        if (fire && Time.time >= nextFire)
        {
            if (bulletPrefab != null && firePoint != null)
            {
                audioSource.PlayOneShot(tiroSom);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                nextFire = Time.time + fireRate;
            }
        }
    }

    public void ExplodirAsteroide()
    {
        audioSource.PlayOneShot(explosaoSom);
    }
}
