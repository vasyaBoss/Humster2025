using UnityEngine;
using UnityEngine.SceneManagement; // Для управления сценами

public class LevelFinish : MonoBehaviour
{
    // Переход на следующую сцену
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Последняя сцена! Возвращаемся в главное меню.");
            ReturnToMainMenu(); // Можно также загрузить титры или финальный экран
        }
    }

    // Метод для возврата в главное меню
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Убедись, что сцена с этим именем добавлена в Build Settings
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
