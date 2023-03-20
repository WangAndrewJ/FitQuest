using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject cover;

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
        Debug.Log(completionSlider.maxValue);
        Debug.Log(this.goalAmount);
        this.questName.text = questName;
        this.xpAmount = xpAmount;
        this.questDescription = questDescription;
        this.isDaily = isDaily;
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
}
