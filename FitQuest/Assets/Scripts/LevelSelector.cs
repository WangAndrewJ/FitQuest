using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public TextMeshProUGUI stageText;
    public GameObject mainMenu;

    private void Start()
    {
        stageText.text = $"Stage: {PlayerPrefs.GetInt("Stage", 1)}";
    }

    public void Select()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Stage", 1), LoadSceneMode.Additive);
        mainMenu.SetActive(false);
    }
}
