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
}
