using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Other;

public enum TypeOfPowerUp
{
    Heal,
    AttackMultiplier,
    AttackSpeedMultiplier,
    AttackGainMultiplier,
    AttackSpeedGainMultiplier
}

public class PowerUpButton : MonoBehaviour
{
    public TypeOfPowerUp myTypeOfPowerUp;
    public GameObject cover;
    public Image myImage;
    public Color healthColor;
    public Color attackColor;
    public Color attackSpeedColor;
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
            case TypeOfPowerUp.AttackSpeedMultiplier:
            case TypeOfPowerUp.AttackSpeedGainMultiplier:
                myImage.color = attackSpeedColor;
                break;
        }

        text.text = $"Collect {Util.Split(typeOfPowerUp.ToString())}";
    }

    public void Collect()
    {
        cover.SetActive(true);
        string name = $"amountOf{myTypeOfPowerUp}";
        PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name, 0) + 1);
        myDailyQuestManager.SavePowerUp();
    }
}
