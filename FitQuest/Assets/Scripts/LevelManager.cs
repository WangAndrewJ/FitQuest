using UnityEngine;
using System;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int xpRequired;
    public float x = 0.3f;
    public float y = 2f;
    public int currentXp = 0;
    public int totalXp;
    public LevelVisual levelVisual;
    public StatManager myStatManager;
    public PlayerHealth myPlayerHealth;
    public GameObject popupPrefab;
    public Transform pageSwiper;

    private void Start()
    {
        try
        {
            myStatManager = FindObjectOfType<StatManager>();
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }

        SetValues();
    }

    private void SetValues()
    {
        if (PlayerPrefs.GetInt("totalXp") == 0)
        {
            PlayerPrefs.SetInt("totalXp", 0);
        }
        else
        {
            totalXp = PlayerPrefs.GetInt("totalXp");
        }

        UpdateCurrentLevelAndXp();
    }

    private void UpdateCurrentLevelAndXp()
    {
        currentXp = totalXp;
        level = 1;
        int maxXp = Mathf.RoundToInt(Mathf.Pow(1 / x, y));

        while (currentXp >= maxXp)
        {
            currentXp -= maxXp;
            level++;
            Debug.Log(level);
            maxXp = Mathf.RoundToInt(Mathf.Pow(level / x, y));
        }

        try
        {
            myStatManager.UpdateMaxHealth();
        }
        catch
        {
            int max = PlayerPrefs.GetInt("Health", 45);
            int newMax = max + level * 5;
            PlayerPrefs.SetInt("Health", max);
            myPlayerHealth.UpdateHealth(newMax, newMax);
        }

        levelVisual.UpdateValues(maxXp, currentXp, level);
    }

    public void ChangeCurrentLevelAndXp(int increment)
    {
        int startingLevel = level;
        totalXp += increment;

        if (totalXp < 0)
        {
            totalXp = 0;
        }

        PlayerPrefs.SetInt("totalXp", totalXp);
        UpdateCurrentLevelAndXp();

        if (level != startingLevel)
        {
            try
            {
                myStatManager.ChangeStat(5, myStatManager.healthStat);
                GameObject popup = Instantiate(popupPrefab, transform.position, Quaternion.identity, pageSwiper);
                popup.GetComponent<TextMeshProUGUI>().text = $"+ 5 Health";
            }
            catch
            {

            }
        }
    }
}
