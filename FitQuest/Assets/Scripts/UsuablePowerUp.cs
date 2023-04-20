using UnityEngine;
using TMPro;
using Assets.Scripts.Other;

public class UsuablePowerUp : MonoBehaviour
{
    public TypeOfPowerUp myTypeOfPowerUp;
    public TextMeshProUGUI text;
    public GameObject cover;
    public StatManager myStatManager;
    public LevelManager myLevelManager;
    public DateManager myDateManager;
    private int amountOfPowerUps;
    private string nameOfPowerUp;
    private string splitName;

    private void Awake()
    {
        nameOfPowerUp = $"amountOf{myTypeOfPowerUp}";
        splitName = Util.Split(myTypeOfPowerUp.ToString());
    }

    private void OnEnable()
    {
        amountOfPowerUps = PlayerPrefs.GetInt(nameOfPowerUp, 0);
        text.text = $"{splitName} ({amountOfPowerUps})";
    }

    public void Use()
    {
        if (amountOfPowerUps > 0)
        {
            amountOfPowerUps--;
            PlayerPrefs.SetInt(nameOfPowerUp, amountOfPowerUps);
            text.text = $"{splitName} ({amountOfPowerUps})";
            cover.SetActive(myTypeOfPowerUp != TypeOfPowerUp.Heal);

            switch (myTypeOfPowerUp)
            {
                case TypeOfPowerUp.Heal:
                    Heal();
                    break;
                case TypeOfPowerUp.AttackMultiplier:
                    myDateManager.StartCountDown(0);
                    break;
                case TypeOfPowerUp.AttackGainMultiplier:
                    myDateManager.StartCountDown(1);
                    break;
                case TypeOfPowerUp.SpeedMultiplier:
                    myDateManager.StartCountDown(2);
                    break;
                case TypeOfPowerUp.SpeedGainMultiplier:
                    myDateManager.StartCountDown(3);
                    break;
            }
        }
    }

    private void Heal()
    {
        Debug.Log(Mathf.RoundToInt(5 * (myLevelManager.level * 0.1f + 1)));
        myStatManager.ChangeStat(Mathf.RoundToInt(5 * (myLevelManager.level * 0.1f + 1)), myStatManager.healthStat);
    }
}
