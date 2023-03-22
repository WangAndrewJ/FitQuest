using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public struct Quest
{
    public Quest(int goalAmount, string questName, int xpAmount, string questDescription, bool isDaily, int order)
    {
        this.goalAmount = goalAmount;
        this.questName = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        this.order = order;
    }

    public int goalAmount;
    public string questName;
    public int xpAmount;
    public string questDescription;
    public bool isDaily;
    public int order;
}

public class QuestButton : MonoBehaviour
{
    public Quest quest;
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
    public GameObject cover;
    public RectTransform rectTransform;

    private void Start()
    {
        buttonManager = transform.parent.GetComponent<ButtonManager>();
        levelManager = buttonManager.levelManager;
        moreMenu = buttonManager.moreMenu;
        pageSwiper = buttonManager.pageSwiper;
        SetQuest();
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
                    cover.SetActive(true);
                }
                else
                {
                    Debug.Log("Complete!");
                    transform.parent = null;
                    Destroy(gameObject);
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
    }

    public void SetValues(string questName, string questDescription, int goalAmount, int xpAmount, bool isDaily)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName.text = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
        SetQuest();
    }

    private void OnDestroy()
    {
        transform.position = new Vector3(0f, -1000f, 0f);
        buttonManager.RearrangeButtons();
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

    public void SetQuest()
    {
        int relativeQuestPositionDistance = (int)Mathf.Abs(90f - rectTransform.anchoredPosition.y);
        quest = new Quest(goalAmount, questName.text, xpAmount, questDescription, isDaily, relativeQuestPositionDistance == 0 ? 0 : relativeQuestPositionDistance / 135);
        //File.WriteAllText(Application.persistentDataPath, JsonUtility.ToJson(quest));
        Debug.Log(JsonUtility.ToJson(quest));
    }

    public void SetQuest(int order)
    {
        quest = new Quest(goalAmount, questName.text, xpAmount, questDescription, isDaily, order);
        //File.WriteAllText(Application.persistentDataPath, JsonUtility.ToJson(quest));
        Debug.Log(JsonUtility.ToJson(quest));
    }
}
