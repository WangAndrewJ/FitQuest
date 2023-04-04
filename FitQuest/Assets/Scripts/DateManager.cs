using UnityEngine;
using System;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

public class DateManager : MonoBehaviour
{
    public ButtonManager myButtonManager;
    public DailyQuestManager myDailyQuestManager;
    public StatManager myStatManager;
    private int currentDayOfWeek;
    [HideInInspector]
    /// <summary>
    /// 0 = Attack Multiplier
    ///  1 = Attack Gain Multiplier
    ///  2 = Speed Multiplier
    ///  3 = Speed Gain Multiplier
    /// </summary>
    public bool[] currentBoosts = new bool[4];
    private DateTime[] startTimes = new DateTime[4];
    public int powerUpDuration;
    public RestDays restDays;

    private void Awake()
    {
        try
        {
            startTimes = JsonConvert.DeserializeObject<DateTime[]>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "starttimes.json")));
            currentBoosts = JsonConvert.DeserializeObject<bool[]>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "currentboosts.json")));

            if (startTimes == null)
            {
                startTimes = new DateTime[4];
            }
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }

    public IEnumerator MinuteLoop()
    {
        while (true)
        {
            currentDayOfWeek = PlayerPrefs.GetInt("currentDayOfTheWeek", 8);

            if (currentDayOfWeek != (int)DateTime.Today.DayOfWeek)
            {
                Debug.Log("New Day!");
                currentDayOfWeek = (int)DateTime.Today.DayOfWeek;
                PlayerPrefs.SetInt("currentDayOfTheWeek", currentDayOfWeek);
                DayOfWeek dayOfWeek = Enum.Parse<DayOfWeek>(currentDayOfWeek.ToString());
                myButtonManager.UpdateDayOfWeek(dayOfWeek);
                restDays.UpdateDayOfWeek(dayOfWeek);
                myDailyQuestManager.LoadRandomQuests(ListOfExercises.Exercises());
                myDailyQuestManager.LoadPowerUp((TypeOfPowerUp)UnityEngine.Random.Range(0, 5), true);
            }

            for (int i = 0; i < 4; i++)
            {
                if (currentBoosts[i] && (DateTime.Now - startTimes[i]).Minutes >= powerUpDuration)
                {
                    Debug.Log($"index: {DateTime.Now}");
                    currentBoosts[i] = false;
                    myStatManager.covers[i].SetActive(false);

                    if (i == 0)
                    {
                        myStatManager.BoostStat(myStatManager.attackStat, 1.5f, false);
                    }
                    else if (i == 2)
                    {
                        myStatManager.BoostStat(myStatManager.speedStat, 1.5f, false);
                    }

                    SaveBoosts();
                    Debug.Log("Boost Ended");
                }
            }

            yield return new WaitForSeconds(60);
        }
    }

    /// <summary>
    /// 0 = Attack Multiplier
    ///  1 = Attack Gain Multiplier
    ///  2 = Speed Multiplier
    ///  3 = Speed Gain Multiplier
    /// </summary>
    public void StartCountDown(int index)
    {
        currentBoosts[index] = true;
        myStatManager.covers[index].SetActive(true);
        startTimes[index] = DateTime.Now;
        SaveBoosts();
        Debug.Log($"index: {startTimes[index]}");
        Debug.Log("Boost Started");

        if (index == 0)
        {
            myStatManager.BoostStat(myStatManager.attackStat, 1.5f, true);
        }
        else if (index == 2)
        {
            myStatManager.BoostStat(myStatManager.speedStat, 1.5f, true);
        }
    }

    public void SaveBoosts()
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "starttimes.json"), JsonConvert.SerializeObject(startTimes));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "currentboosts.json"), JsonConvert.SerializeObject(currentBoosts));
    }
}
