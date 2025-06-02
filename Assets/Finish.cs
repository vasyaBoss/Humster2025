using UnityEngine;
using TMPro; // Для использования TextMeshPro
using UnityEngine.SceneManagement; // Для перехода между сценами

public class LevelFinishFlag : MonoBehaviour
{
    public GameObject finishUI;          // UI-панель, которая появится при завершении уровня
    public TMP_Text promptText;          // Текстовая подсказка (например: "Нажмите E")
    public GameObject player;            // Игрок (для проверки входа в триггер)

    private bool playerInRange = false;  // Флаг, указывает, рядом ли игрок
    private bool levelCompleted = false; // Флаг, чтобы не завершить уровень дважды

    void Start()
    {
        if (finishUI != null)
        {
            finishUI.SetActive(false);  // UI выключен в начале
        }

        if (promptText != null)
        {
            promptText.text = "";  // Подсказка пуста
        }
    }

    void Update()
    {
        if (playerInRange && !levelCompleted)
        {
            if (promptText != null)
            {
                promptText.text = "Нажмите E для завершения уровня";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CompleteLevel();
            }
        }
        else
        {
            if (promptText != null)
            {
                promptText.text = "";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void CompleteLevel()
    {
        levelCompleted = true;

        if (promptText != null)
        {
            promptText.text = "";
        }

        if (finishUI != null)
        {
            finishUI.SetActive(true);
        }

        Debug.Log("Уровень завершен!");

        // Через 2 секунды загружаем следующую сцену
        Invoke("LoadNextScene", 2f);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Это был последний уровень! Можно показать титры или меню победы.");
            // TODO: добавить сцену победы или титров, если хочешь
        }
    }
}
