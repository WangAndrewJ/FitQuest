using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct Quest
{
    public Quest(int goalAmount, string questName, int xpAmount, string questDescription, bool isDaily, int order, float sliderValue, bool isDisabled, int dailyStreak)
    {
        this.goalAmount = goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        this.order = order;
        this.sliderValue = sliderValue;
        this.isDisabled = isDisabled;
        this.dailyStreak = dailyStreak;
    }

    public int goalAmount;
    public string questName;
    public int xpAmount;
    public string questDescription;
    public bool isDaily;
    public int order;
    public float sliderValue;
    public bool isDisabled;
    public int dailyStreak;
}

public class QuestButton : MonoBehaviour
{
    public int xpAmount = 1;
    private LevelManager levelManager;
    public Slider completionSlider;
    public int goalAmount = 1;
    public ButtonManager buttonManager;
    //[HideInInspector]
    public string questName;
    public TextMeshProUGUI questNameText;
    private MoreMenu moreMenu;
    private string questDescription;
    private PageSwiper pageSwiper;
    private bool isDaily;
    public GameObject isDisabledCover;
    public RectTransform rectTransform;
    private int dailyStreak;

    private void Start()
    {
        buttonManager = transform.parent.GetComponent<ButtonManager>();
        levelManager = buttonManager.levelManager;
        moreMenu = buttonManager.moreMenu;
        pageSwiper = buttonManager.pageSwiper;
    }

    public void ChangeXp(bool isAdding)
    {
        if (isAdding)
        {
            levelManager.ChangeCurrentLevelAndXp(xpAmount);
            completionSlider.value++;

            UpdateSlider(false);
        }
        else
        {
            if (completionSlider.value > 0)
            {
                levelManager.ChangeCurrentLevelAndXp(-xpAmount);
                completionSlider.value--;
            }
        }

        buttonManager.SaveQuests();
    }

    private void UpdateSlider(bool isAlreadyComplete)
    {
        if (completionSlider.value == goalAmount)
        {
            if (isDaily)
            {
                if (!isAlreadyComplete)
                {
                    isDisabledCover.SetActive(true);
                    IncreaseStreak(1);
                    buttonManager.SaveQuests();
                }
            }
            else
            {
                isDisabledCover.SetActive(false);
                Debug.Log("Complete!");
                transform.parent = null;
                Destroy();
                //Destroy(gameObject);
            }
        }
        else
        {
            isDisabledCover.SetActive(false);
        }
    }

    public void ChangeValues(string questName, string questDescription, int goalAmount, int xpAmount, bool isDaily)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName = questName;
        questNameText.text = isDaily ? $"{questName} ({dailyStreak})" : questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        UpdateSlider(true);
    }

    public void LoadValues(string questName, string questDescription, int goalAmount, int xpAmount, bool isDaily, float sliderValue, bool isDisabled, int dailyStreak)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        completionSlider.value = sliderValue;
        isDisabledCover.SetActive(isDisabled);
        UpdateStreak(dailyStreak);
        questNameText.text = isDaily ? $"{questName} ({dailyStreak})" : questName;
    }

    public void Destroy()
    {
        transform.position = new Vector3(0f, -1000f, 0f);
        buttonManager.RearrangeButtons();
        Destroy(gameObject);
    }

    public void OpenMoreMenu()
    {
        pageSwiper.Block(true);
        moreMenu.gameObject.SetActive(true);
        moreMenu.questButton = this;
        moreMenu.questNameInput.text = questName;
        moreMenu.questDescriptionInput.text = questDescription;
        moreMenu.goalAmountInput.text = goalAmount.ToString();
        moreMenu.xpAmountInput.text = xpAmount.ToString();
        moreMenu.dailyToggle.isOn = isDaily;
    }

    public Quest GetQuest(int order)
    {
        //Quest quest = new Quest(goalAmount, questName.text, xpAmount, questDescription, isDaily, order);
        //File.WriteAllText(Application.persistentDataPath, JsonUtility.ToJson(quest));
        //Debug.Log(JsonUtility.ToJson(quest));
        return new Quest(goalAmount, questName, xpAmount, questDescription, isDaily, order, completionSlider.value, isDisabledCover.activeSelf, dailyStreak);
    }
    public void UpdateStreak(int newDailyStreak)
    {
        dailyStreak = newDailyStreak;

        questNameText.text = $"{questName} ({dailyStreak})";
    }

    public void IncreaseStreak(int changeIncrement)
    {
        dailyStreak += changeIncrement;

        if (dailyStreak == 0)
        {
            return;
        }

        questNameText.text = $"{questName} ({dailyStreak})";
    }
}
