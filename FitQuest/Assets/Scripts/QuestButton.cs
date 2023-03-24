using UnityEngine;
using UnityEngine.UI;
using TMPro;

public struct Quest
{
    public Quest(int goalAmount, string questName, int xpAmount, string questDescription, bool isDaily, int order, float sliderValue, bool isDisabled)
    {
        this.goalAmount = goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        this.order = order;
        this.sliderValue = sliderValue;
        this.isDisabled = isDisabled;
    }

    public int goalAmount;
    public string questName;
    public int xpAmount;
    public string questDescription;
    public bool isDaily;
    public int order;
    public float sliderValue;
    public bool isDisabled;
}

public class QuestButton : MonoBehaviour
{
    public int xpAmount = 1;
    private LevelManager levelManager;
    public Slider completionSlider;
    public int goalAmount = 1;
    public ButtonManager buttonManager;
    public TextMeshProUGUI questName;
    private MoreMenu moreMenu;
    private string questDescription;
    private PageSwiper pageSwiper;
    private bool isDaily;
    public GameObject isDisabledCover;
    public RectTransform rectTransform;

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

            if (completionSlider.value == goalAmount)
            {
                if (isDaily)
                {
                    isDisabledCover.SetActive(true);
                }
                else
                {
                    Debug.Log("Complete!");
                    transform.parent = null;
                    Destroy();
                    //Destroy(gameObject);
                }
            }
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

    public void SetValues(string questName, string questDescription, int goalAmount, int xpAmount, bool isDaily)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName.text = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
    }

    public void SetValues(string questName, string questDescription, int goalAmount, int xpAmount, bool isDaily, float sliderValue, bool isDisabled)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName.text = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        completionSlider.value = sliderValue;
        isDisabledCover.SetActive(isDisabled);
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
        moreMenu.questNameInput.text = questName.text;
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
        return new Quest(goalAmount, questName.text, xpAmount, questDescription, isDaily, order, completionSlider.value, isDisabledCover.activeSelf);
    }
}
