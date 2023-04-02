using UnityEngine;
using TMPro;
using System;

public class StatManager : MonoBehaviour
{
    public TextMeshProUGUI placeholder;
    public GameObject EditMenu;
    public TMP_InputField statInput;
    public LevelManager myLevelManager;
    private Stat currentStat;
    public Stat healthStat;
    public Stat attackStat;
    public Stat speedStat;
    public Stat[] decimalStats;
    public Stat[] intStats;
    public Stat[] clampedIntStats;
    //public int healthBoost;
    public float attackMultiplier;
    public float speedMultiplier;

    private void Start()
    {
        UpdateMaxHealth();

        foreach (Stat stat in decimalStats)
        {
            stat.statText.text = $"{stat.statName}: {PlayerPrefs.GetFloat(stat.statName, stat.defaultFloat)}";
        }

        foreach (Stat stat in intStats)
        {
            stat.statText.text = $"{stat.statName}: {PlayerPrefs.GetInt(stat.statName, stat.defaultInt)}";
        }
    }

    public void UpdateMaxHealth()
    {
        int newMax = healthStat.maxStat + myLevelManager.level * 5;
        int currentValue = PlayerPrefs.GetInt(healthStat.statName, healthStat.defaultInt);
        PlayerPrefs.SetInt("Health.maxStat", newMax);
        PlayerPrefs.SetInt("Health", currentValue > newMax ? newMax : currentValue);

        healthStat.statText.text = $"Health: {PlayerPrefs.GetInt("Health", healthStat.defaultInt)} / {PlayerPrefs.GetInt($"Health.maxStat", healthStat.maxStat)}";
    }

    public void EnableEditMenu(Stat stat)
    {
        currentStat = stat;
        placeholder.text = stat.statName;
        EditMenu.SetActive(true);
    }

    public void ChangeStat()
    {
        float parsedStatInput;

        try
        {
            parsedStatInput = float.Parse(statInput.text);

            if (parsedStatInput <= 0)
            {
                return;
            }
        }
        catch (Exception exception)
        {
            Debug.Log($"Stat Manager: {exception}");
            return;
        }

        currentStat.statText.text = currentStat.maxStat > 0 ? $"{currentStat.statName}: {parsedStatInput} / {PlayerPrefs.GetInt($"{currentStat.statName}.maxStat", currentStat.maxStat)}" : $"{currentStat.statName}: {statInput.text}";
        PlayerPrefs.SetFloat(currentStat.statName, parsedStatInput);
    }

    public void ChangeStat(int change, Stat stat)
    {
        if (change <= 0)
        {
            return;
        }

        int newValue = PlayerPrefs.GetInt(stat.statName, stat.defaultInt) + change;

        stat.statText.text = $"{stat.statName}: {newValue}";
        PlayerPrefs.SetInt(stat.statName, newValue);
    }

    public void ChangeClampedStat(int change, Stat stat)
    {
        if (change <= 0)
        {
            return;
        }

        int maxStat = PlayerPrefs.GetInt($"{stat.statName}.maxStat", stat.maxStat);
        int newValue = Mathf.Clamp(PlayerPrefs.GetInt(stat.statName, stat.defaultInt) + change, 0, maxStat);

        stat.statText.text = $"{stat.statName}: {newValue} / {maxStat}";
        PlayerPrefs.SetInt(stat.statName, newValue);
    }
}
