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

    public void DeleteQuest()
    {
        Destroy(questButton.gameObject);
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

        if (goalAmount <= questButton.completionSlider.value)
        {
            Debug.Log("GoalAmnt" + goalAmount);
            Debug.Log("CSV" + questButton.completionSlider.value);
            Debug.Log($"More Menu: New Goal Amount Exceeds Or Equals Past Current Goal Completion Progress");
            return;
        }

        Debug.Log("Past All Checks");
        questButton.SetValues(questNameInput.text, questDescriptionInput.text, goalAmount, xpAmount, dailyToggle.isOn);
    }
}
