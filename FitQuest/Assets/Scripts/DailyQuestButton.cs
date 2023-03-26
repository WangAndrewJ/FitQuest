using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public struct DailyQuest
{
    public DailyQuest(int goalAmount, string questName, int xpAmount, float sliderValue, bool isDisabled)
    {
        this.goalAmount = goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.sliderValue = sliderValue;
        this.isDisabled = isDisabled;
    }

    public int goalAmount;
    public string questName;
    public int xpAmount;
    public float sliderValue;
    public bool isDisabled;
}

public class DailyQuestButton : MonoBehaviour
{
    public int xpAmount = 1;
    private LevelManager levelManager;
    public Slider completionSlider;
    public int goalAmount = 1;
    public DailyQuestManager myDailyQuestManager;
    public TextMeshProUGUI questNameText;
    public GameObject isDisabledCover;
    //public RectTransform rectTransform;
    //private int dailyStreak;

    private void Start()
    {
        myDailyQuestManager = transform.parent.GetComponent<DailyQuestManager>();
        levelManager = myDailyQuestManager.myLevelManager;
    }

    public void ChangeXp(bool isAdding)
    {
        if (isAdding)
        {
            levelManager.ChangeCurrentLevelAndXp(xpAmount);
            completionSlider.value++;

            UpdateSlider(false);
        }
        else
        {
            if (completionSlider.value > 0)
            {
                levelManager.ChangeCurrentLevelAndXp(-xpAmount);
                completionSlider.value--;
            }
        }

        myDailyQuestManager.SaveQuests();
    }

    private void UpdateSlider(bool isAlreadyComplete)
    {
        if (completionSlider.value == goalAmount)
        {
            if (!isAlreadyComplete)
            {
                isDisabledCover.SetActive(true);
                myDailyQuestManager.SaveQuests();
            }
        }
        else
        {
            isDisabledCover.SetActive(false);
        }
    }

/*    public void ChangeValues(string questName, int repsPerSet, int goalAmount, int xpAmount, bool isDaily, bool[] activeDaysOfTheWeek, float weight, bool isCardio, int seconds)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName = (questName == "") ? this.questName : questName;
        questNameText.text = isDaily ? $"{this.questName} ({dailyStreak})" : this.questName;
        this.xpAmount = xpAmount;
        this.repsPerSet = repsPerSet;
        this.isDaily = isDaily;
        UpdateSlider(true);
        this.activeDaysOfTheWeek = activeDaysOfTheWeek;
        this.weight = weight;
        this.isCardio = isCardio;
        this.seconds = seconds;

        if (isDaily)
        {
            this.activeDaysOfTheWeek = activeDaysOfTheWeek;
            daysOfWeek = new();

            for (int i = 0; i < 7; i++)
            {
                if (activeDaysOfTheWeek[i])
                {
                    daysOfWeek.Add(copy[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < 7; i++)
            {
                activeDaysOfTheWeek[i] = false;
            }

            daysOfWeek = new();
        }
    }*/

    public void LoadValues(string questName, int goalAmount, int xpAmount, float sliderValue, bool isDisabled)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        questNameText.text = questName;
        this.xpAmount = xpAmount;
        completionSlider.value = sliderValue;
        isDisabledCover.SetActive(isDisabled);
    }

    public DailyQuest GetQuest()
    {
        return new DailyQuest(goalAmount, questNameText.text, xpAmount, completionSlider.value, isDisabledCover.activeSelf);
    }
}
