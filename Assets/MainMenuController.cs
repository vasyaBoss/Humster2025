using UnityEngine;
using UnityEngine.SceneManagement;  // Для работы со сценами

public class MainMenuController : MonoBehaviour
{
    // Метод для начала игры
    public void StartGame()
    {
        // Загружаем сцену с названием "GameScene"
        // Замените на название вашей сцены, где происходит сама игра
        SceneManager.LoadScene("Level1");
    }

    // Метод для выхода из игры
    public void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit(); // Закрытие приложения
    }
}
