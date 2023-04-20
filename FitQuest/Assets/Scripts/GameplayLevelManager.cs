using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayLevelManager : MonoBehaviour
{
    public TextMeshProUGUI stageText;

    private void Start()
    {
        SetStage();
    }

    private void SetStage()
    {
        stageText.text = $"Stage: {PlayerPrefs.GetInt("Stage", 1)}";
    }

    public void GoToScene(int buildIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
