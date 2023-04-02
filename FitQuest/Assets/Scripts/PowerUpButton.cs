using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TypeOfPowerUp
{
    Heal,
    AttackMultiplier,
    SpeedMultiplier,
    AttackGainMultiplier,
    SpeedGainMultiplier
}

public class PowerUpButton : MonoBehaviour
{
    public TypeOfPowerUp myTypeOfPowerUp;
    public GameObject cover;
    public Image myImage;
    public Color healthColor;
    public Color attackColor;
    public Color speedColor;
    public TextMeshProUGUI text;
    private DailyQuestManager myDailyQuestManager;

    public void SetTypeOfPowerUp(TypeOfPowerUp typeOfPowerUp, DailyQuestManager dailyQuestManager)
    {
        myTypeOfPowerUp = typeOfPowerUp;
        myDailyQuestManager = dailyQuestManager;

        switch (typeOfPowerUp)
        {
            case TypeOfPowerUp.Heal:
                myImage.color = healthColor;
                break;
            case TypeOfPowerUp.AttackMultiplier:
            case TypeOfPowerUp.AttackGainMultiplier:
                myImage.color = attackColor;
                break;
            case TypeOfPowerUp.SpeedMultiplier:
            case TypeOfPowerUp.SpeedGainMultiplier:
                myImage.color = speedColor;
                break;
        }

        text.text = $"Collect {Split(typeOfPowerUp.ToString())}";
    }

    private string Split(string input)
    {
        string result = "";

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsUpper(input[i]))
            {
                result += ' ';
            }

            result += input[i];
        }

        return result.Trim();
    }

    public void Collect()
    {
        cover.SetActive(true);
        myDailyQuestManager.SavePowerUp();
        // save this in playerprefs
    }
}
