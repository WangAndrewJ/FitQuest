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

    private void Start()
    {
        foreach (Stat stat in decimalStats)
        {
            stat.statText.text = $"{stat.statName}: {PlayerPrefs.GetFloat(stat.statName)}";
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

        currentStat.statText.text = $"{currentStat.statName}: {statInput.text}";
        PlayerPrefs.SetFloat(currentStat.statName, parsedStatInput);
    }
}
