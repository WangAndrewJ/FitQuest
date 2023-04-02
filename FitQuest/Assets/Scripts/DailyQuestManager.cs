using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DailyQuestManager : MonoBehaviour
{
    public DailyQuestButton[] dailyQuestButtons;
    public LevelManager myLevelManager;
    private List<DailyQuest> myQuests;
    private TypeOfPowerUp currentPowerUp;
    public PowerUpButton powerUpButton;

    private void Start()
    {
        try
        {
            myQuests = JsonConvert.DeserializeObject<List<DailyQuest>>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "dailyquests.json")));
            LoadQuests(myQuests);
        }
        catch (System.Exception exception)
        {
            Debug.Log("Line 23: " + ListOfExercises.Exercises().Count);
            LoadRandomQuests(ListOfExercises.Exercises());
            Debug.Log($"Daily Quest Manager: {exception}");
        }

        if (PlayerPrefs.HasKey("currentPowerUp"))
        {
            LoadPowerUp((TypeOfPowerUp)PlayerPrefs.GetInt("currentPowerUp"), false);
        }
        else
        {
            LoadPowerUp((TypeOfPowerUp)Random.Range(0, 5), false);
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
    }

    public void SavePowerUp()
    {
        PlayerPrefs.SetInt("currentPowerUp", (int)currentPowerUp);
        PlayerPrefs.SetInt("dailyPowerUpCoverActive", System.Convert.ToInt32(powerUpButton.cover.activeSelf));
    }

    public void LoadRandomQuests(List<Exercise> quests)
    {
        myQuests = new();

        for (int i = 0; i < dailyQuestButtons.Length; i++)
        {
            Exercise currentExercise = quests[Random.Range(0, quests.Count)];
            int randomGoalAmount = Random.Range(1, 5);
            DailyQuest currentDailyQuest = new DailyQuest(currentExercise.name, randomGoalAmount, Mathf.RoundToInt(Random.Range(0.5f, 2f) * randomGoalAmount), 0f, false, currentExercise.isCardio);
            dailyQuestButtons[i].LoadValues(currentExercise.name, randomGoalAmount, Mathf.RoundToInt(Random.Range(0.55f, 2f) * randomGoalAmount), 0f, false, currentExercise.isCardio);
            dailyQuestButtons[i].myDailyQuestManager = this;
            dailyQuestButtons[i].myLevelManger = myLevelManager;
            myQuests.Add(currentDailyQuest);
            quests.Remove(currentExercise);
        }

        SaveQuests();
    }

    private void LoadQuests(List<DailyQuest> quests)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            dailyQuestButtons[i].LoadValues(quests[i].questName, quests[i].goalAmount, quests[i].xpAmount, quests[i].sliderValue, quests[i].isDisabled, quests[i].isCardio);
            dailyQuestButtons[i].myDailyQuestManager = this;
            dailyQuestButtons[i].myLevelManger = myLevelManager;
        }
    }


    public void LoadPowerUp(TypeOfPowerUp typeOfPowerUp, bool isNewDay)
    {
        currentPowerUp = typeOfPowerUp;
        powerUpButton.SetTypeOfPowerUp(currentPowerUp, this);
        powerUpButton.cover.SetActive(isNewDay ? false : PlayerPrefs.GetInt("dailyPowerUpCoverActive", 0) != 0);
        SavePowerUp();
    }
}
