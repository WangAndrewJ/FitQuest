using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

public class DailyQuestManager : MonoBehaviour
{
    public DailyQuestButton[] dailyQuestButtons;
    public LevelManager myLevelManager;
    private List<DailyQuest> myQuests;

    private void Start()
    {
        try
        {
            myQuests = JsonConvert.DeserializeObject<List<DailyQuest>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "dailyquests.json")));

            LoadQuests(myQuests);
        }
        catch (Exception exception)
        {
            Debug.Log("Line 23: " + ListOfExercises.ListOfQuests().Count);
            LoadRandomQuests(ListOfExercises.ListOfQuests());
            Debug.Log($"Daily Quest Manager: {exception}");
        }
    }

    public void SaveQuests()
    {
        myQuests = new();

        foreach (DailyQuestButton dailyQuestButton in dailyQuestButtons)
        {
            myQuests.Add(dailyQuestButton.GetQuest());
        }

        File.WriteAllText(Path.Combine(Application.persistentDataPath, "dailyquests.json"), JsonConvert.SerializeObject(myQuests));
        Debug.Log(JsonConvert.SerializeObject(myQuests));
    }

    public void LoadRandomQuests(List<DailyQuest> quests)
    {
        myQuests = new();

        for (int i = 0; i < dailyQuestButtons.Length; i++)
        {
            Debug.Log("Line 41: " + UnityEngine.Random.Range(0, 1));
            Debug.Log("Line 42: " + UnityEngine.Random.Range(0, quests.Count));
            DailyQuest currentQuest = quests[UnityEngine.Random.Range(0, quests.Count)];
            dailyQuestButtons[i].LoadValues(currentQuest.questName, currentQuest.goalAmount, currentQuest.xpAmount, currentQuest.sliderValue, currentQuest.isDisabled);
            myQuests.Add(currentQuest);
            quests.Remove(currentQuest);
        }

        SaveQuests();
    }

    private void LoadQuests(List<DailyQuest> quests)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            dailyQuestButtons[i].LoadValues(quests[i].questName, quests[i].goalAmount, quests[i].xpAmount, quests[i].sliderValue, quests[i].isDisabled);
        }
    }
}
