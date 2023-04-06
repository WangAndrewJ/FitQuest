using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public int levelIndex;
    public TextMeshProUGUI label;

    private void Start()
    {
        label.text = levelIndex.ToString();
    }

    public void Select()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
