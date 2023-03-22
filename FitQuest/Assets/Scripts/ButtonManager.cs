using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public GameObject questButton;
    public RectTransform newQuestButton;
    public TMP_InputField questNameInput;
    public TMP_InputField questDescriptionInput;
    public TMP_InputField goalAmountInput;
    public TMP_InputField xpAmountInput;
    public Toggle dailyToggle;
    public LevelManager levelManager;
    public MoreMenu moreMenu;
    public PageSwiper pageSwiper;
    private List<Quest> quests = new List<Quest>();

    public void AddQuest(Quest quest)
    {

    }

    public void RearrangeButtons()
    {
        float childYPos = 90f;
        //int currentOrder = 0;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        foreach (RectTransform child in children.OrderByDescending(orderedChildren => orderedChildren.position.y))
        {
            Debug.Log(child);
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
            //child.GetComponent<QuestButton>().SetQuest(currentOrder);
            //currentOrder++;
        }
    }
    public void RearrangeButtonsMoving()
    {
        float childYPos = 90f;
        int currentOrder = 0;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        List<RectTransform> orderedChildren = children.OrderByDescending(orderedChildren => orderedChildren.position.y).ToList<RectTransform>();
        orderedChildren.Remove(newQuestButton);
        //orderedChildren.Add(newQuestButton);

        foreach (RectTransform child in orderedChildren)
        {
            Debug.Log(child);
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
            child.GetComponent<QuestButton>().SetQuest(currentOrder);
            currentOrder++;
        }

        newQuestButton.anchoredPosition = new Vector3(0f, childYPos, 0f);

        // Save Quest
    }

    public void SaveQuests()
    {

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
            Debug.Log($"Button Manager: {exception}");
            return;
        }

        RectTransform instantiatedQuestButton = Instantiate(questButton, transform).GetComponent<RectTransform>();
        QuestButton alreadyInstantiatedQuestButton = instantiatedQuestButton.GetComponent<QuestButton>();
        Debug.Log(instantiatedQuestButton.anchoredPosition.y);
        instantiatedQuestButton.anchoredPosition = new Vector3(0f, newQuestButton.anchoredPosition.y + 1f, 0f);
        alreadyInstantiatedQuestButton.SetValues(questNameInput.text, questDescriptionInput.text, goalAmountText, xpAmountText, dailyToggle.isOn);
        RearrangeButtons();
    }
}
