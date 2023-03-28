using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class ExerciseButtonManager : MonoBehaviour
{
    public GameObject exerciseButtonPrefab;
    private List<Exercise> exercises;
    public RectTransform content;
    public RectTransform self;
    public GameObject newQuestMenu;
    public GameObject newCardioMenu;
    public GameObject defaultQuestsMenu;
    public TMP_InputField questName;
    public TMP_InputField cardioName;

    private void Start()
    {
        exercises = ListOfExercises.Exercises();
        LoadQuest(exercises);
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
    }

    private void LoadQuest(List<Exercise> exercises)
    {
        for (int i = 0; i < exercises.Count; i++)
        {
            RectTransform instantiatedExerciseButton = Instantiate(exerciseButtonPrefab, transform).GetComponent<RectTransform>();
            ExerciseButton alreadyInstantiatedExerciseButton = instantiatedExerciseButton.GetComponent<ExerciseButton>();
            instantiatedExerciseButton.anchoredPosition = new Vector3(0f, 90f - i * 135f, 0f);
            alreadyInstantiatedExerciseButton.LoadValues(exercises[i].name, exercises[i].isCardio, newQuestMenu, newCardioMenu, defaultQuestsMenu, questName, cardioName, content);
        }

        Debug.Log(exercises.Count);
        float height = exercises.Count * 13.5f + 100f;
        content.sizeDelta = new Vector2(0f, height);
        self.anchoredPosition = new Vector3(0f, height / 2f - 70f);
        RearrangeButtons();
    }
}
