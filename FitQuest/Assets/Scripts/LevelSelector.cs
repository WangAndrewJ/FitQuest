using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public TextMeshProUGUI stageText;

    private void Start()
    {
        stageText.text = $"Stage: {PlayerPrefs.GetInt("Stage", 1)}";
    }

    public void Select()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Stage", 1));
    }
}
