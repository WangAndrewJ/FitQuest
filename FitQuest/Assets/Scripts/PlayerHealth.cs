using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public Slider healthSlider;
    public GameObject deathMenu;
    public LevelManager myLevelManager;

    public void Damage(int damage)
    {
        health -= damage;
        UpdateHealth(health, (int)healthSlider.maxValue);
    }

    public void UpdateHealth(int health, int maxStat)
    {
        this.health = health;
        healthSlider.maxValue = maxStat;
        healthSlider.value = health;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        PlayerPrefs.SetInt("PersistentHealth", 0);
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
