using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public struct Quest
{
    public Quest(int goalAmount, string questName, int xpAmount, int repsPerSet, bool isDaily, int order, float sliderValue, bool isDisabled, int dailyStreak, bool[] activeDaysOfTheWeek, float weight, bool isCardio, int seconds)
    {
        this.goalAmount = goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.repsPerSet = repsPerSet;
        this.isDaily = isDaily;
        this.order = order;
        this.sliderValue = sliderValue;
        this.isDisabled = isDisabled;
        this.dailyStreak = dailyStreak;
        this.activeDaysOfTheWeek = activeDaysOfTheWeek;
        this.weight = weight;
        this.isCardio = isCardio;
        this.seconds = seconds;
    }

    public int goalAmount;
    public string questName;
    public int xpAmount;
    public int repsPerSet;
    public bool isDaily;
    public int order;
    public float sliderValue;
    public bool isDisabled;
    public int dailyStreak;
    public bool[] activeDaysOfTheWeek;
    public float weight;
    public bool isCardio;
    public int seconds;
}

public class QuestButton : MonoBehaviour
{
    public int xpAmount = 1;
    private LevelManager levelManager;
    public Slider completionSlider;
    public int goalAmount = 1;
    public ButtonManager buttonManager;
    public string questName;
    public TextMeshProUGUI questNameText;
    private MoreMenu moreMenu;
    private EditCardioMenu editCardioMenu;
    private int repsPerSet;
    private PageSwiper pageSwiper;
    private bool isDaily;
    public GameObject isDisabledCover;
    //public RectTransform rectTransform;
    private int dailyStreak;
    public bool[] activeDaysOfTheWeek = new bool[7];
    public List<DayOfWeek> daysOfWeek = new() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
    private DayOfWeek[] copy = new DayOfWeek[7] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
    private float weight;
    private bool isCardio;
    private int seconds;
    public Image image;
    public Color strengthColor;
    public Color cardioColor;

    private void Start()
    {
        buttonManager = transform.parent.GetComponent<ButtonManager>();
        levelManager = buttonManager.levelManager;
        moreMenu = buttonManager.moreMenu;
        editCardioMenu = buttonManager.editCardioMenu;
        pageSwiper = buttonManager.pageSwiper;
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

        buttonManager.SaveQuests();
    }

    private void UpdateSlider(bool isAlreadyComplete)
    {
        if (completionSlider.value == goalAmount)
        {
            if (isDaily)
            {
                if (!isAlreadyComplete)
                {
                    isDisabledCover.SetActive(true);
                    IncreaseStreak(1);
                    buttonManager.SaveQuests();
                }
            }
            else
            {
                isDisabledCover.SetActive(false);
                Debug.Log("Complete!");
                transform.parent = null;
                Destroy();
                //Destroy(gameObject);
            }
        }
        else
        {
            isDisabledCover.SetActive(false);
        }
    }

    public void ChangeValues(string questName, int repsPerSet, int goalAmount, int xpAmount, bool isDaily, bool[] activeDaysOfTheWeek, float weight, bool isCardio, int seconds)
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
    }

    public void LoadValues(string questName, int repsPerSet, int goalAmount, int xpAmount, bool isDaily, float sliderValue, bool isDisabled, int dailyStreak, bool[] activeDaysOfTheWeek, float weight, bool isCardio, int seconds)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.repsPerSet = repsPerSet;
        this.isDaily = isDaily;
        completionSlider.value = sliderValue;
        isDisabledCover.SetActive(isDisabled);
        UpdateStreak(dailyStreak);
        questNameText.text = isDaily ? $"{questName} ({dailyStreak})" : questName;
        this.weight = weight;
        this.isCardio = isCardio;
        image.color = isCardio ? cardioColor : strengthColor;
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
    }

    public void Destroy()
    {
        transform.parent = null;
        Destroy(gameObject);
        buttonManager.RearrangeButtons();
    }

    public void OpenMoreMenu()
    {
        pageSwiper.Block(true);
        if (isCardio)
        {
            editCardioMenu.gameObject.SetActive(true);
            editCardioMenu.questButton = this;
            //moreMenu.questNameInput.text = questName;
            editCardioMenu.secondsInput.text = repsPerSet.ToString();
            editCardioMenu.goalAmountInput.text = goalAmount.ToString();
            editCardioMenu.xpAmountInput.text = xpAmount.ToString();
            editCardioMenu.dailyToggle.isOn = isDaily;
            editCardioMenu.daysOfTheWeekTogglesHolder.SetActive(isDaily);

            for (int i = 0; i < 7; i++)
            {
                editCardioMenu.daysOfTheWeekToggles[i].isOn = activeDaysOfTheWeek[i];
            }
        }
        else
        {
            moreMenu.gameObject.SetActive(true);
            moreMenu.questButton = this;
            //moreMenu.questNameInput.text = questName;
            moreMenu.repsPerSetInput.text = repsPerSet.ToString();
            moreMenu.goalAmountInput.text = goalAmount.ToString();
            moreMenu.xpAmountInput.text = xpAmount.ToString();
            moreMenu.weightInput.text = weight.ToString();
            moreMenu.dailyToggle.isOn = isDaily;
            moreMenu.daysOfTheWeekTogglesHolder.SetActive(isDaily);

            for (int i = 0; i < 7; i++)
            {
                moreMenu.daysOfTheWeekToggles[i].isOn = activeDaysOfTheWeek[i];
            }
        }
    }

    public Quest GetQuest(int order)
    {
        return new Quest(goalAmount, questName, xpAmount, repsPerSet, isDaily, order, completionSlider.value, isDisabledCover.activeSelf, dailyStreak, activeDaysOfTheWeek, weight, isCardio, seconds);
    }
    public void UpdateStreak(int newDailyStreak)
    {
        dailyStreak = newDailyStreak;

        questNameText.text = $"{questName} ({dailyStreak})";
    }

    public void IncreaseStreak(int changeIncrement)
    {
        dailyStreak += changeIncrement;

        if (dailyStreak == 0)
        {
            return;
        }

        questNameText.text = $"{questName} ({dailyStreak})";
    }
}
