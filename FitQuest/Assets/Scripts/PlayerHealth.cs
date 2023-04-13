using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public Slider healthSlider;

    private void Start()
    {
        health = PlayerPrefs.GetInt("Health");
        healthSlider.maxValue = PlayerPrefs.GetInt("Health.maxStat");
        healthSlider.value = health;
    }

    public void Damage(int damage)
    {
        health -= damage;
        PlayerPrefs.SetInt("Health", health);
        healthSlider.maxValue = PlayerPrefs.GetInt("Health.maxStat");
        healthSlider.value = health;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }
}
