using UnityEngine;
using TMPro;
using System;

public class StatManager : MonoBehaviour
{
    public TextMeshProUGUI placeholder;
    public GameObject EditMenu;
    public TMP_InputField statInput;
    public LevelManager myLevelManager;
    public DateManager myDateManager;
    private Stat currentStat;
    public Stat healthStat;
    public Stat attackStat;
    public Stat attackSpeedStat;
    public Stat[] decimalStats;
    public Stat[] intStats;
    public Color boostedAttackColor;
    public Color boostedAttackSpeedColor;
    public Color normalColor;
    /// <summary>
    /// 0 = Attack Multiplier
    ///  1 = Attack Gain Multiplier
    ///  2 = Attack Speed Multiplier
    ///  3 = Attack Speed Gain Multiplier
    /// </summary>
    public GameObject[] covers;

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

        if (myDateManager.currentBoosts[0])
        {
            BoostStat(attackStat, 1f, true);
            covers[0].SetActive(true);
        }

        if (myDateManager.currentBoosts[2])
        {
            BoostStat(attackSpeedStat, 1f, true);
            covers[2].SetActive(true);
        }
    }

    public void UpdateMaxHealth()
    {
        int newMax = healthStat.defaultInt + myLevelManager.level * 5;
        PlayerPrefs.SetInt("Health", healthStat.defaultInt);

        healthStat.statText.text = $"Health: {newMax}";
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

        currentStat.statText.text = $"{currentStat.statName}: {parsedStatInput} / {PlayerPrefs.GetInt($"{currentStat.statName}", currentStat.defaultInt)}";
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

/*    public void ChangeClampedStat(int change, Stat stat)
    {
        if (change <= 0)
        {
            return;
        }

        int maxStat = PlayerPrefs.GetInt($"{stat.statName}", stat.maxStat);
        int newValue = Mathf.Clamp(PlayerPrefs.GetInt(stat.statName, stat.defaultInt) + change, 0, maxStat);
        stat.statText.text = $"{stat.statName}: {newValue} / {maxStat}";
        PlayerPrefs.SetInt(stat.statName, newValue);
    }*/

    public void BoostStat(Stat stat, float boost, bool isBoosting)
    {
        int newValue = PlayerPrefs.GetInt(stat.statName, stat.defaultInt);

        if (isBoosting && stat.statText.color != boostedAttackColor && stat.statText.color != boostedAttackSpeedColor)
        {
            newValue = Mathf.RoundToInt(newValue * boost);
            stat.statText.color = stat == attackStat ? boostedAttackColor : boostedAttackSpeedColor;
        }
        else if (!isBoosting && stat.statText.color != normalColor)
        {
            newValue = Mathf.RoundToInt(newValue / boost);
            stat.statText.color = normalColor;
        }

        stat.statText.text = $"{stat.statName}: {newValue}";
        PlayerPrefs.SetInt(stat.statName, newValue);
        myDateManager.SaveBoosts();
    }
}
