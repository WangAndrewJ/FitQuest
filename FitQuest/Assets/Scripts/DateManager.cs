using UnityEngine;
using System.Collections;
using System;

public class DateManager : MonoBehaviour
{
    public ButtonManager myButtonManager;
    public DailyQuestManager myDailyQuestManager;
    public int currentDayOfWeek;

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
                myButtonManager.UpdateDayOfWeek(Enum.Parse<DayOfWeek>(currentDayOfWeek.ToString()));
                myDailyQuestManager.LoadRandomQuests(ListOfExercises.ListOfQuests());
            }

            yield return new WaitForSeconds(60);
        }
    }
}
