using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MoreMenu : MonoBehaviour
{
    [HideInInspector]
    public QuestButton questButton;
    public TMP_InputField questNameInput;
    public TMP_InputField repsPerSetInput;
    public TMP_InputField goalAmountInput;
    public TMP_InputField xpAmountInput;
    public TMP_InputField weightInput;
    public Toggle dailyToggle;
    public Toggle[] daysOfTheWeekToggles = new Toggle[7];
    public GameObject daysOfTheWeekTogglesHolder;

    public void DeleteQuest()
    {
        questButton.Destroy();
    }

    public void EditQuest()
    {
        int repsPerSet;
        int goalAmount;
        int xpAmount;
        float weight;

        try
        {
            repsPerSet = int.Parse(repsPerSetInput.text);
            goalAmount = int.Parse(goalAmountInput.text);
            xpAmount = int.Parse(xpAmountInput.text);
            weight = float.Parse(weightInput.text);
        }
        catch (Exception exception)
        {
            Debug.Log($"More Menu: {exception}");
            return;
        }

        if (dailyToggle.isOn)
        {
            bool containsAtLeastOneDay = false;

            foreach (Toggle toggle in daysOfTheWeekToggles)
            {
                if (toggle.isOn)
                {
                    containsAtLeastOneDay = true;
                }
            }

            if (!containsAtLeastOneDay)
            {
                Debug.Log($"More Menu: At Least One Day Of The Week Must Be Selected");
                return;
            }
        }

        if (goalAmount < questButton.completionSlider.value)
        {
            Debug.Log("GoalAmnt" + goalAmount);
            Debug.Log("CSV" + questButton.completionSlider.value);
            Debug.Log($"More Menu: New Goal Amount Exceeds Past Current Goal Completion Progress");
            return;
        }
        else if (goalAmount == questButton.completionSlider.value && !questButton.isDisabledCover)
        {
            Debug.Log("GoalAmnt" + goalAmount);
            Debug.Log("CSV" + questButton.completionSlider.value);
            Debug.Log($"More Menu: New Goal Amount Equals Current Goal Completion Progress");
            return;
        }

        bool[] activeDaysOfTheWeek = new bool[7];

        for (int i = 0; i < 7; i++)
        {
            activeDaysOfTheWeek[i] = daysOfTheWeekToggles[i].isOn;
        }

        Debug.Log("Past All Checks");
        questButton.ChangeValues(questNameInput.text, repsPerSet, goalAmount, xpAmount, dailyToggle.isOn, activeDaysOfTheWeek, weight);
        questButton.buttonManager.SaveQuests();
    }
}
