using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelVisual : MonoBehaviour
{
    private LevelManager levelManager;
    public Slider levelBar;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void UpdateValues(int maxXp, int currentXp, int level)
    {
        levelBar.maxValue = maxXp;
        levelBar.value = currentXp;
        xpText.text = $"{currentXp}/{maxXp}";
        levelText.text = level.ToString();
    }
}
