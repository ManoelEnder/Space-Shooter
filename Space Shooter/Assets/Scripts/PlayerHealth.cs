using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public Slider healthSlider;
    public DamageFlash damageFlash;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthSlider != null) healthSlider.value = currentHealth;
        if (damageFlash != null) damageFlash.PlayFlash();

        if (currentHealth <= 0)
        {
            GameManager.Instance.PlayerHit();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        if (healthSlider != null) healthSlider.value = currentHealth;
    }
}
