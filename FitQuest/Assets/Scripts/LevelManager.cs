using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int xpRequired;
    public float x = 0.3f;
    public float y = 2f;
    public int currentXp = 0;
    public int totalXp;
    public LevelVisual levelVisual;

    private void Start()
    {
        SetValues();
    }

    private void SetValues()
    {
        if (PlayerPrefs.GetInt("totalXp") == 0)
        {
            PlayerPrefs.SetInt("totalXp", 12);
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
            maxXp = Mathf.RoundToInt(Mathf.Pow(level / x, y));
        }

        levelVisual.UpdateValues(maxXp, currentXp, level);
    }

    public void ChangeCurrentLevelAndXp(int increment)
    {
        totalXp += increment;

        if (totalXp < 0)
        {
            totalXp = 0;
        }

        PlayerPrefs.SetInt("totalXp", totalXp);
        UpdateCurrentLevelAndXp();
    }
}
