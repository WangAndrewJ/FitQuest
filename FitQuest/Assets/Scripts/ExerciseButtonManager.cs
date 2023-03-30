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
    private List<ExerciseButton> startingExerciseButtons;
    private List<RectTransform> children;
    private List<ExerciseButton> exerciseButtons;
    //private bool isFirstTime = true;
    public TMP_InputField searchBar;

    private void OnEnable()
    {
        exercises = ListOfExercises.Exercises();
        LoadQuest(exercises);
    }

    public void RearrangeButtons()
    {
        float childYPos = 90f;

        foreach (RectTransform child in children.OrderByDescending(orderedChildren => orderedChildren.position.y))
        {
            child.anchoredPosition = new Vector3(0f, childYPos, 0f);
            childYPos -= 135f;
        }
    }

    private void LoadQuest(List<Exercise> exercises)
    {
        children = new();
        exerciseButtons = new();

        for (int i = 0; i < exercises.Count; i++)
        {
            RectTransform instantiatedExerciseButton = Instantiate(exerciseButtonPrefab, transform).GetComponent<RectTransform>();
            ExerciseButton alreadyInstantiatedExerciseButton = instantiatedExerciseButton.GetComponent<ExerciseButton>();
            instantiatedExerciseButton.anchoredPosition = new Vector3(0f, 90f - i * 135f, 0f);
            alreadyInstantiatedExerciseButton.LoadValues(exercises[i].name, exercises[i].isCardio, newQuestMenu, newCardioMenu, defaultQuestsMenu, questName, cardioName, content, this);
            children.Add(instantiatedExerciseButton);
            exerciseButtons.Add(alreadyInstantiatedExerciseButton);
        }

        startingExerciseButtons = exerciseButtons;
        Debug.Log(startingExerciseButtons.Count);

        Debug.Log(exercises.Count);
        float height = exercises.Count * 13.5f + 100f;
        content.sizeDelta = new Vector2(0f, height);
        self.anchoredPosition = new Vector3(0f, height / 2f - 70f);
        //isFirstTime = false;
        RearrangeButtons();
    }

    public void OnSearchChange()
    {
        children = new();
        exerciseButtons = new();

        if (searchBar.textComponent.text == "")
        {
            foreach (ExerciseButton exerciseButton in startingExerciseButtons)
            {
                children.Add(exerciseButton.self);
                exerciseButtons.Add(exerciseButton);
            }

            return;
        }

        Debug.Log(startingExerciseButtons.Count);

        foreach (ExerciseButton exerciseButton in startingExerciseButtons)
        {
            Debug.Log($"{searchBar.text}, {exerciseButton.exerciseName}, {exerciseButton.exerciseName.ToLower().Contains(searchBar.text)}");

            if (exerciseButton.exerciseName.ToLower().Contains(searchBar.text))
            {
                exerciseButton.gameObject.SetActive(true);
                children.Add(exerciseButton.self);
                exerciseButtons.Add(exerciseButton);
            }
            else
            {
                exerciseButton.gameObject.SetActive(false);
            }
        }

        RearrangeButtons();
    }

    public void ExitMenu()
    {
        searchBar.text = "";

        foreach (ExerciseButton button in startingExerciseButtons)
        {
            Destroy(button.gameObject);
        }
    }
}
