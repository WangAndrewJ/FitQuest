using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
    public PlayerHealth myPlayerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentStage = PlayerPrefs.GetInt("Stage");
            int nextStage = PlayerPrefs.GetInt("Stage") + 1;
            PlayerPrefs.SetInt("PersistentHealth", myPlayerHealth.health);
            PlayerPrefs.SetInt("Stage", nextStage);
            SceneManager.LoadSceneAsync(nextStage, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(currentStage);
        }
    }
}
