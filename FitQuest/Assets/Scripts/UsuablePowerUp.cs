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
        Debug.Log(splitName);
        text.text = $"{splitName} ({amountOfPowerUps})";
    }

    public void Use()
    {
        if (amountOfPowerUps > 0)
        {
            amountOfPowerUps--;
            PlayerPrefs.SetInt(nameOfPowerUp, amountOfPowerUps);
            text.text = $"{splitName} ({amountOfPowerUps})";
            cover.SetActive(true);

            switch (myTypeOfPowerUp)
            {
                case TypeOfPowerUp.Heal:
                    Heal();
                    break;
                case TypeOfPowerUp.AttackMultiplier:
                case TypeOfPowerUp.AttackGainMultiplier:

                    break;
                case TypeOfPowerUp.SpeedMultiplier:
                case TypeOfPowerUp.SpeedGainMultiplier:

                    break;
            }
        }
    }

    private void Heal()
    {
        Debug.Log(Mathf.RoundToInt(5 * (myLevelManager.level * 0.1f + 1)));
        myStatManager.ChangeClampedStat(Mathf.RoundToInt(5 * (myLevelManager.level * 0.1f + 1)), myStatManager.healthStat);
    }
}
