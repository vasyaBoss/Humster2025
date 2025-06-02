using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Button restartButton;

    public PlayerController player;

    void Start()
    {
        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartFromCheckpoint);
    }

    public void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Вы погибли!";
    }

    private void RestartFromCheckpoint()
    {
        gameOverPanel.SetActive(false);
        player.Respawn();  
    }
}
