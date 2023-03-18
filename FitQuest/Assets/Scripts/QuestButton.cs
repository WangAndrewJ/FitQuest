using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestButton : MonoBehaviour
{
    public int xpAmount;
    private LevelManager levelManager;
    public Slider completionSlider;
    private int goalAmount = 1;
    private ButtonManager buttonManager;
    public TextMeshProUGUI questName;

    private void Start()
    {
        buttonManager = transform.parent.GetComponent<ButtonManager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void ChangeXp()
    {
        levelManager.ChangeCurrentLevelAndXp(xpAmount);
        completionSlider.value++;

        if (completionSlider.value == goalAmount)
        {
            Debug.Log("Complete!");
            transform.parent = null;
            Destroy(gameObject);
        }
    }

    public void SetValues(string questName, string questDescription, int goalAmount, int xpAmount)
    {
        this.goalAmount = goalAmount;
        completionSlider.maxValue = this.goalAmount;
        this.questName.text = questName;
        this.xpAmount = xpAmount;
    }

    private void OnDestroy()
    {
        buttonManager.RearrangeButtons();
    }
}
