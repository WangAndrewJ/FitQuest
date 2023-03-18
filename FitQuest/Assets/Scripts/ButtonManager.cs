using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class ButtonManager : MonoBehaviour
{
    public GameObject questButton;
    public RectTransform newQuestButton;
    public TMP_InputField questNameInput;
    public TMP_InputField questDescriptionInput;
    public TMP_InputField goalAmountInput;
    public TMP_InputField xpAmountInput;

    public void RearrangeButtons()
    {
        float childYPos = 90f;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        foreach (var go in children.OrderByDescending(go => go.position.y))
        {
            go.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
        }
    }


    public void MakeQuest()
    {
        int goalAmountText;
        int xpAmountText;

        try
        {
            goalAmountText = int.Parse(goalAmountInput.text);
            xpAmountText = int.Parse(xpAmountInput.text);
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
            return;
        }

        RectTransform instantiatedQuestButton = Instantiate(questButton, transform).GetComponent<RectTransform>();
        QuestButton questbutton = instantiatedQuestButton.GetComponent<QuestButton>();
        Debug.Log(instantiatedQuestButton.anchoredPosition.y);
        instantiatedQuestButton.anchoredPosition = new Vector3(0f, newQuestButton.anchoredPosition.y + 1f, 0f);
        questbutton.SetValues(questNameInput.text, questDescriptionInput.text, goalAmountText, xpAmountText);
        RearrangeButtons();
    }
}
