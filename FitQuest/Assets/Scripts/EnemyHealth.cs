using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void Damage(int damage)
    {
        health -= damage;
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
