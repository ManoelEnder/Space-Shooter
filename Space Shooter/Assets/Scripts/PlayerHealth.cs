using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    public Slider healthSlider;

    public AudioSource audioSource;
    public AudioClip damageSound;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null) healthSlider.value = 1f;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        ScreenShake.Instance?.Shake(0.2f, 0.15f);

        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();

            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);

        if (healthSlider != null)
            healthSlider.value = (float)currentHealth / maxHealth;
    }
}
