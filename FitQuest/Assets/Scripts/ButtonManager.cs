using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    public GameObject questButtonPrefab;
    public RectTransform newQuestButton;
    public LevelManager levelManager;
    public DateManager myDateManager;
    public MoreMenu moreMenu;
    public EditCardioMenu editCardioMenu;
    public PageSwiper pageSwiper;
    public StatManager myStatManager;
    private List<Quest> quests;
    public RectTransform content;
    public float buttonMaxY;
    [Space(10)]
    [Header("Quest Inputs")]
    public TMP_InputField questNameInput;
    public TMP_InputField repsPerSetInput;
    public TMP_InputField goalAmountInput;
    public TMP_InputField xpAmountInput;
    public TMP_InputField weightInput;
    public Toggle dailyToggle;
    public Toggle[] daysOfTheWeekToggles = new Toggle[7];
    [Space(10)]
    [Header("Cardio Inputs")]
    public TMP_InputField cardioNameInput;
    public TMP_InputField secondsInput;
    public TMP_InputField cardioGoalAmountInput;
    public TMP_InputField cardioXpAmountInput;
    public Toggle cardioDailyToggle;
    public Toggle[] cardioDaysOfTheWeekToggles = new Toggle[7];

    private void Start()
    {
        try
        {
            quests = JsonConvert.DeserializeObject<List<Quest>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "questsdata.json")));

            if (quests == null)
            {
                quests = new();
            }

            LoadQuest(quests);
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }

        StartCoroutine(myDateManager.MinuteLoop());
    }

    public void SaveQuests()
    {
        quests = new();
        List<QuestButton> questButtons = new();
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        List<RectTransform> orderedChildren = children.OrderByDescending(orderedChildren => orderedChildren.position.y).ToList();
        orderedChildren.Remove(newQuestButton);

        foreach (RectTransform child in orderedChildren)
        {
            questButtons.Add(child.GetComponent<QuestButton>());
        }

        for (int i = 0; i < questButtons.Count; i++)
        {
            quests.Add(questButtons[i].GetComponent<QuestButton>().GetQuest(i));
        }

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "questsdata.json"), JsonConvert.SerializeObject(quests));
    }

    public void RearrangeButtons()
    {
        float childYPos = buttonMaxY;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        foreach (RectTransform child in children.OrderByDescending(orderedChildren => orderedChildren.position.y))
        {
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
        }

        content.sizeDelta = new Vector2(0f, quests.Count * 135f + 200f);
        SaveQuests();
    }

    public void RearrangeMovingButtons()
    {
        float childYPos = buttonMaxY;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        List<RectTransform> orderedChildren = children.OrderByDescending(orderedChildren => orderedChildren.position.y).ToList();
        orderedChildren.Remove(newQuestButton);

        foreach (RectTransform child in orderedChildren)
        {
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
        }
    }

    public void MakeCustomQuest()
    {
        int repsPerSetText;
        int goalAmountText;
        int xpAmountText;
        float weightText;

        try
        {
            repsPerSetText = int.Parse(repsPerSetInput.text);
            goalAmountText = int.Parse(goalAmountInput.text);
            xpAmountText = int.Parse(xpAmountInput.text);
            weightText = float.Parse(weightInput.text);
        }
        catch (Exception exception)
        {
            Debug.Log($"Button Manager: {exception}");
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
                Debug.Log($"Button Manager: At Least One Day Of The Week Must Be Selected");
                return;
            }
        }

        if (repsPerSetText <= 0 || goalAmountText <= 0 || xpAmountText <= 0 || weightText < 0)
        {
            Debug.Log("Button Manager: Reps, Sets, and Xp must all be over 0, and Weight must not be less than 0");
            return;
        }

        if (questNameInput.text == "")
        {
            Debug.Log("Button Manager: Quest name must be filled out");
            return;
        }

        bool[] activeDaysOfTheWeek = new bool[7];

        for (int i = 0; i < 7; i++)
        {
            activeDaysOfTheWeek[i] = daysOfTheWeekToggles[i].isOn;
        }

        RectTransform instantiatedQuestButton = Instantiate(questButtonPrefab, transform).GetComponent<RectTransform>();
        QuestButton alreadyInstantiatedQuestButton = instantiatedQuestButton.GetComponent<QuestButton>();
        instantiatedQuestButton.anchoredPosition = new Vector3(0f, newQuestButton.anchoredPosition.y + 1f, 0f);
        alreadyInstantiatedQuestButton.ChangeValues(questNameInput.text, repsPerSetText, goalAmountText, xpAmountText, dailyToggle.isOn, activeDaysOfTheWeek, weightText, false, 0);
        Debug.Log(quests);
        content.sizeDelta = new Vector2(0f, quests.Count * 135f + 200f);
        RearrangeButtons();
    }

    public void MakeCustomCardio()
    {
        int secondsText;
        int goalAmountText;
        int xpAmountText;

        try
        {
            secondsText = int.Parse(secondsInput.text);
            goalAmountText = int.Parse(cardioGoalAmountInput.text);
            xpAmountText = int.Parse(cardioXpAmountInput.text);
        }
        catch (Exception exception)
        {
            Debug.Log($"Button Manager: {exception}");
            return;
        }

        if (cardioDailyToggle.isOn)
        {
            bool containsAtLeastOneDay = false;

            foreach (Toggle toggle in cardioDaysOfTheWeekToggles)
            {
                if (toggle.isOn)
                {
                    containsAtLeastOneDay = true;
                }
            }

            if (!containsAtLeastOneDay)
            {
                Debug.Log($"Button Manager: At Least One Day Of The Week Must Be Selected");
                return;
            }
        }

        if (secondsText <= 0 || goalAmountText <= 0 || xpAmountText <= 0)
        {
            Debug.Log("Button Manager: Seconds, Sets, and Xp must all be over 0");
            return;
        }

        if (cardioNameInput.text == "")
        {
            Debug.Log("Button Manager: Quest name must be filled out");
            return;
        }

        bool[] activeDaysOfTheWeek = new bool[7];

        for (int i = 0; i < 7; i++)
        {
            activeDaysOfTheWeek[i] = cardioDaysOfTheWeekToggles[i].isOn;
        }

        RectTransform instantiatedQuestButton = Instantiate(questButtonPrefab, transform).GetComponent<RectTransform>();
        QuestButton alreadyInstantiatedQuestButton = instantiatedQuestButton.GetComponent<QuestButton>();
        instantiatedQuestButton.anchoredPosition = new Vector3(0f, newQuestButton.anchoredPosition.y + 1f, 0f);
        alreadyInstantiatedQuestButton.ChangeValues(cardioNameInput.text, 0, goalAmountText, xpAmountText, cardioDailyToggle.isOn, activeDaysOfTheWeek, 0, true, secondsText);
        content.sizeDelta = new Vector2(0f, quests.Count * 135f + 200f);
        RearrangeButtons();
    }

    private void LoadQuest(List<Quest> quests)
    {
        if (quests == null)
        {
            return;
        }

        //bool[] activeDaysOfTheWeek = new bool[7];

        //for (int i = 0; i < 7; i++)
        //{
        //    activeDaysOfTheWeek[i] = daysOfTheWeekToggles[i].isOn;
        //}

        for (int i = 0; i < quests.Count; i++)
        {
            RectTransform instantiatedQuestButton = Instantiate(questButtonPrefab, transform).GetComponent<RectTransform>();
            QuestButton alreadyInstantiatedQuestButton = instantiatedQuestButton.GetComponent<QuestButton>();
            Debug.Log(buttonMaxY - quests[i].order * 135f);
            instantiatedQuestButton.anchoredPosition = new Vector3(0f, buttonMaxY - quests[i].order * 135f, 0f);
            alreadyInstantiatedQuestButton.LoadValues(quests[i].questName, quests[i].repsPerSet, quests[i].goalAmount, quests[i].xpAmount, quests[i].isDaily, quests[i].sliderValue, quests[i].isDisabled, quests[i].dailyStreak, quests[i].activeDaysOfTheWeek, quests[i].weight, quests[i].isCardio, quests[i].seconds);
            newQuestButton.anchoredPosition = new Vector3(0f, newQuestButton.anchoredPosition.y - 135f, 0f);
        }

        content.sizeDelta = new Vector2(0f, quests.Count * 135f + 200f);
    }

    public void UpdateDayOfWeek(DayOfWeek dayOfWeek)
    {
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        List<RectTransform> orderedChildren =/* children.OrderByDescending(orderedChildren => orderedChildren.position.y).ToList();*/ children.ToList();
        orderedChildren.Remove(newQuestButton);

        foreach (RectTransform child in orderedChildren)
        {
            QuestButton childQuestButton = child.GetComponent<QuestButton>();

            if (childQuestButton.GetQuest(0).isDaily && childQuestButton.daysOfWeek.Contains(dayOfWeek))
            {
                if (childQuestButton.GetQuest(0).isDisabled == false)
                {
                    childQuestButton.UpdateStreak(0);
                }

                childQuestButton.isDisabledCover.SetActive(false);
                childQuestButton.completionSlider.value = 0;
            }

            SaveQuests();
        }
    }
}
