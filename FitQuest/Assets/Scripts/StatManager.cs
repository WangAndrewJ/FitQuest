using UnityEngine;
using TMPro;
using System;

public class StatManager : MonoBehaviour
{
    public TextMeshProUGUI placeholder;
    public GameObject EditMenu;
    public TMP_InputField statInput;
    private Stat currentStat;
    public Stat[] decimalStats;
    public Stat[] clampedIntStats;

    private void Start()
    {
        foreach (Stat stat in decimalStats)
        {
            stat.statText.text = $"{stat.statName}: {PlayerPrefs.GetFloat(stat.statName)}";
        }

        foreach (Stat stat in clampedIntStats)
        {
            stat.statText.text = $"{stat.statName}: {PlayerPrefs.GetInt(stat.statName, stat.maxStat)} / {PlayerPrefs.GetInt($"{PlayerPrefs.GetInt(stat.statName)}.maxStat", stat.maxStat)}";
        }
    }

    public void EnableEditMenu(Stat stat)
    {
        currentStat = stat;
        placeholder.text = currentStat.statName;
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

        currentStat.statText.text = currentStat.maxStat > 0 ? $"{currentStat.statName}: {parsedStatInput} / {PlayerPrefs.GetInt($"{PlayerPrefs.GetInt(currentStat.statName)}.maxStat", currentStat.maxStat)}" : $"{currentStat.statName}: {statInput.text}";
        PlayerPrefs.SetFloat(currentStat.statName, parsedStatInput);
    }
}
