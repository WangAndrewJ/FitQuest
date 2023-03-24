using UnityEngine;
using TMPro;

public class StreakCounter : MonoBehaviour
{
    public int dailyStreak = 0;
    public TextMeshProUGUI questNameText;

    public void UpdateStreak(int newDailyStreak, string questName)
    {
        dailyStreak = newDailyStreak;

        if (dailyStreak == 0)
        {
            return;
        }

        questNameText.text = $"{questName} {dailyStreak}";
    }

    public void IncreaseStreak(int changeIncrement, string questName)
    {
        dailyStreak += changeIncrement;

        if (dailyStreak == 0)
        {
            return;
        }

        questNameText.text = $"{questName} {dailyStreak}";
    }
}
