using UnityEngine;
using System.Collections;
using System;

public class DateManager : MonoBehaviour
{
    public ButtonManager myButtonManager;
    public int currentDayOfWeek;

    public IEnumerator MinuteLoop()
    {
        // when get back, change currentDay to currentDayOfWeek, then go to step 3
        currentDayOfWeek = PlayerPrefs.GetInt("currentDayOfTheWeek", 8);

        while (true)
        {
            if (currentDayOfWeek != (int)DateTime.Today.DayOfWeek)
            {
                Debug.Log("New Day!");
                currentDayOfWeek = (int)DateTime.Today.DayOfWeek;
                PlayerPrefs.SetInt("currentDayOfTheWeek", currentDayOfWeek);
                myButtonManager.UpdateDayOfWeek(Enum.Parse<DayOfWeek>(currentDayOfWeek.ToString()));
            }

            yield return new WaitForSeconds(60);
        }
    }
}
