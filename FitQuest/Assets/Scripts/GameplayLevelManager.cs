using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayLevelManager : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public int currentStage;

    private void Start()
    {
        SetStage();
    }

    private void SetStage()
    {
        stageText.text = $"Stage: {currentStage}";
        //PlayerPrefs.SetInt("Stage", SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToScene(int buildIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(buildIndex);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(currentStage, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(currentStage);
    }
}
