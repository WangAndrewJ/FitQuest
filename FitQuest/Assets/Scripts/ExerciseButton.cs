using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExerciseButton : MonoBehaviour
{
    public RectTransform self;
    public Exercise exercise;
    [HideInInspector]
    public string exerciseName;
    [HideInInspector]
    public bool isCardio;
    public TextMeshProUGUI nameText;
    private GameObject newQuestMenu;
    private GameObject newCardioMenu;
    private GameObject defaultQuestsButton;
    private TMP_InputField questName;
    private TMP_InputField cardioName;
    private RectTransform content;
    public Image image;
    public Color strengthColor;
    public Color cardioColor;

    public void LoadValues(string name, bool isCardio, GameObject newQuestMenu, GameObject newCardioMenu, GameObject defaultQuestsButton, TMP_InputField questName, TMP_InputField cardioName, RectTransform content)
    {
        exerciseName = name;
        this.isCardio = isCardio;
        nameText.text = name;
        this.newQuestMenu = newQuestMenu;
        this.newCardioMenu = newCardioMenu;
        this.defaultQuestsButton = defaultQuestsButton;
        this.questName = questName;
        this.cardioName = cardioName;
        this.content = content;
        image.color = isCardio ? cardioColor : strengthColor;
    }

    public void Select()
    {
        if (isCardio)
        {
            newCardioMenu.SetActive(true);
            cardioName.text = nameText.text;
        }
        else
        {
            newQuestMenu.SetActive(true);
            questName.text = nameText.text;
        }

        content.anchoredPosition = new Vector3(0f, 0f);
        defaultQuestsButton.SetActive(false);
    }
}
