using UnityEngine;
using System.Collections;
using System;

public class DateManager : MonoBehaviour
{
    public ButtonManager myButtonManager;
    public int currentDay;

    public IEnumerator MinuteLoop()
    {
        currentDay = PlayerPrefs.GetInt("currentDay");

        while (true)
        {
            if (currentDay != DateTime.Today.Day)
            {
                Debug.Log("New Day!");
                currentDay = DateTime.Today.Day;
                PlayerPrefs.SetInt("currentDay", currentDay);
                myButtonManager.ReenableDailyQuests();
            }

            yield return new WaitForSeconds(60);
        }
    }
}
