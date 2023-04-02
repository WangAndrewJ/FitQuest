using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct DailyQuest
{
    public DailyQuest(string questName, int goalAmount, int xpAmount, float sliderValue, bool isDisabled, bool isCardio)
    {
        this.questName = questName;
        this.goalAmount = goalAmount;
        this.xpAmount = xpAmount;
        this.sliderValue = sliderValue;
        this.isDisabled = isDisabled;
        this.isCardio = isCardio;
    }

    public int goalAmount;
    public string questName;
    public int xpAmount;
    public float sliderValue;
    public bool isDisabled;
    public bool isCardio;
}

public class DailyQuestButton : MonoBehaviour
{
    public int xpAmount = 1;
    public LevelManager myLevelManger;
    public Slider completionSlider;
    public int goalAmount = 1;
    public DailyQuestManager myDailyQuestManager;
    public TextMeshProUGUI questNameText;
    public GameObject isDisabledCover;
    private bool isCardio;
    public Image image;
    public Color strengthColor;
    public Color cardioColor;

    public void ChangeXp(bool isAdding)
    {
        if (isAdding)
        {
            completionSlider.value++;

            UpdateSlider(false);
        }
        else
        {
            if (completionSlider.value > 0)
            {
                completionSlider.value--;
            }
        }

        myDailyQuestManager.SaveQuests();
    }

    private void UpdateSlider(bool isAlreadyComplete)
    {
        if (completionSlider.value == goalAmount)
        {
            if (!isAlreadyComplete)
            {
                myLevelManger.ChangeCurrentLevelAndXp(xpAmount);
                isDisabledCover.SetActive(true);
                myDailyQuestManager.SaveQuests();
            }
        }
        else
        {
            isDisabledCover.SetActive(false);
        }
    }

    public void LoadValues(string questName, int goalAmount, int xpAmount, float sliderValue, bool isDisabled, bool isCardio)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        questNameText.text = questName;
        this.xpAmount = xpAmount;
        completionSlider.value = sliderValue;
        isDisabledCover.SetActive(isDisabled);
        this.isCardio = isCardio;
        image.color = isCardio ? cardioColor : strengthColor;
    }

    public DailyQuest GetQuest()
    {
        return new DailyQuest(questNameText.text, goalAmount, xpAmount, completionSlider.value, isDisabledCover.activeSelf, isCardio);
    }
}
