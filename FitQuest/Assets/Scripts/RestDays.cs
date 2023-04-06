using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class RestDays : MonoBehaviour
{
    public StatManager myStatManager;
    public Toggle[] daysOfTheWeekToggles = new Toggle[7];
    private List<DayOfWeek> daysOfWeek = new() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
    private DayOfWeek[] copy = new DayOfWeek[7] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday };
    private bool[] activeDaysOfTheWeek = new bool[7];

    private void Start()
    {
        try
        {
            LoadRestDays();
        }
        catch (Exception exception)
        {
            Debug.Log($"Rest Days: {exception}");
        }
    }

    public void StartUpdateRestDays()
    {
        StartCoroutine(UpdateRestDays());
    }

    private IEnumerator UpdateRestDays()
    {
        yield return new WaitForEndOfFrame();

        daysOfWeek = new();

        for (int i = 0; i < 7; i++)
        {
            if (daysOfTheWeekToggles[i].isOn)
            {
                daysOfWeek.Add(copy[i]);
            }
        }

        SaveRestDays();
    }

    private void LoadRestDays()
    {
        activeDaysOfTheWeek = JsonConvert.DeserializeObject<bool[]>(File.ReadAllText(Path.Combine(Application.persistentDataPath, "restdays.json")));
        Debug.Log(File.ReadAllText(Path.Combine(Application.persistentDataPath, "restdays.json")));

        for (int i = 0; i < 7; i++)
        {
            Debug.Log($"{i}: {activeDaysOfTheWeek[i]}");
            daysOfTheWeekToggles[i].isOn = activeDaysOfTheWeek[i];
        }
    }

    private void SaveRestDays()
    {
        for (int i = 0; i < 7; i++)
        {
            activeDaysOfTheWeek[i] = daysOfTheWeekToggles[i].isOn;
        }

        Debug.Log(JsonConvert.SerializeObject(activeDaysOfTheWeek));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "restdays.json"), JsonConvert.SerializeObject(activeDaysOfTheWeek));
    }

    public void UpdateDayOfWeek(DayOfWeek dayOfWeek)
    {
        if (daysOfWeek.Contains(dayOfWeek))
        {
            Debug.Log(true);
            myStatManager.ChangeClampedStat(UnityEngine.Random.Range(1, 11), myStatManager.healthStat);
        }
    }
}
