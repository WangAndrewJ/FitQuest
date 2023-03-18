using UnityEngine;

public class NewQuestButton : MonoBehaviour
{
    public GameObject newQuestMenu;
    public void AddQuest()
    {
        newQuestMenu.SetActive(true);
    }
}
