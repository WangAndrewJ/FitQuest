using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public int changeIncrement;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void ChangeXp()
    {
        levelManager.ChangeCurrentLevelAndXp(changeIncrement);
    }
}
