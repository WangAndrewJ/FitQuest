using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MoreMenu : MonoBehaviour
{
    public QuestButton questButton;
    public TMP_InputField questNameInput;
    public TMP_InputField questDescriptionInput;
    public TMP_InputField goalAmountInput;
    public TMP_InputField xpAmountInput;
    public Toggle dailyToggle;
    public Toggle[] daysOfTheWeekToggles = new Toggle[7];
    public GameObject daysOfTheWeekTogglesHolder;

    public void DeleteQuest()
    {
        questButton.Destroy();
    }

    public void EditQuest()
    {
        int goalAmount;
        int xpAmount;

        try
        {
            goalAmount = int.Parse(goalAmountInput.text);
            xpAmount = int.Parse(xpAmountInput.text);
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
        questButton.ChangeValues(questNameInput.text, questDescriptionInput.text, goalAmount, xpAmount, dailyToggle.isOn, activeDaysOfTheWeek);
        questButton.buttonManager.SaveQuests();
    }
}
