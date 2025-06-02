using UnityEngine;
using UnityEngine.SceneManagement;  // ��� ������ �� �������

public class MainMenuController : MonoBehaviour
{
    // ����� ��� ������ ����
    public void StartGame()
    {
        // ��������� ����� � ��������� "GameScene"
        // �������� �� �������� ����� �����, ��� ���������� ���� ����
        SceneManager.LoadScene("Level1");
    }

    // ����� ��� ������ �� ����
    public void QuitGame()
    {
        Debug.Log("����� �� ����");
        Application.Quit(); // �������� ����������
    }
}
