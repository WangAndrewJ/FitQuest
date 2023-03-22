using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    private List<Quest> quests;

    private void Start()
    {
        SaveQuests();
    }

    public void SaveQuests()
    {
        quests = new();
        List<QuestButton> questButtons = new();
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        List<RectTransform> orderedChildren = children.OrderByDescending(orderedChildren => orderedChildren.position.y).ToList();
        orderedChildren.Remove(newQuestButton);

        foreach (RectTransform child in orderedChildren)
        {
            questButtons.Add(child.GetComponent<QuestButton>());
        }

        for (int i = 0; i < questButtons.Count; i++)
        {
            quests.Add(questButtons[i].GetComponent<QuestButton>().GetQuest(i));
        }

        Debug.Log(JsonConvert.SerializeObject(quests));
    }

    public void RearrangeButtons()
    {
        float childYPos = 90f;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        foreach (RectTransform child in children.OrderByDescending(orderedChildren => orderedChildren.position.y))
        {
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
        }

        SaveQuests();
    }

    public void RearrangeMovingButtons()
    {
        float childYPos = 90f;
        RectTransform[] children = new RectTransform[transform.childCount];

        foreach (RectTransform child in transform)
        {
            children[child.GetSiblingIndex()] = child;
        }

        List<RectTransform> orderedChildren = children.OrderByDescending(orderedChildren => orderedChildren.position.y).ToList<RectTransform>();
        orderedChildren.Remove(newQuestButton);

        foreach (RectTransform child in orderedChildren)
        {
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
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
            Debug.Log($"Button Manager: {exception}");
            return;
        }

        RectTransform instantiatedQuestButton = Instantiate(questButton, transform).GetComponent<RectTransform>();
        QuestButton alreadyInstantiatedQuestButton = instantiatedQuestButton.GetComponent<QuestButton>();
        instantiatedQuestButton.anchoredPosition = new Vector3(0f, newQuestButton.anchoredPosition.y + 1f, 0f);
        alreadyInstantiatedQuestButton.SetValues(questNameInput.text, questDescriptionInput.text, goalAmountText, xpAmountText, dailyToggle.isOn);
        RearrangeButtons();
    }
}
