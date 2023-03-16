using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public int level = 1;
    public int xpRequired;
    public float x = 0.3f;
    public float y = 2f;
    public int currentXp = 0;

    private void Start()
    {
        ActiveValues();
    }

    private void ActiveValues()
    {
        if (PlayerPrefs.GetInt("level") == 0)
        {
            PlayerPrefs.SetInt("level", 1);
        }
        else
        {
            level = PlayerPrefs.GetInt("level");
        }

        xpRequired = Mathf.RoundToInt(Mathf.Pow((level / x), y));

        if (PlayerPrefs.GetInt("currentXp") == 0)
        {
            PlayerPrefs.SetInt("currentXp", 0);
        }
        else
        {
            currentXp = PlayerPrefs.GetInt("currentXp");
        }
    }

    private void ChangeValues()
    {
        level = PlayerPrefs.GetInt("level");
        xpRequired = Mathf.RoundToInt(Mathf.Pow((level / x), y));
        currentXp = PlayerPrefs.GetInt("currentXp");
    }
}
