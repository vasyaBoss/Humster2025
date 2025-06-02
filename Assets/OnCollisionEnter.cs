using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 10f;  // ���������� �����
    public string playerTag = "Player";  // ��� ��� ������������� ������

    // �����, ���������� ��� ������������ � ��������
    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� �� ����������� � ��������, � �������� ��� "Player"
        if (other.CompareTag(playerTag))
        {
            // �������� ��������� PlayerController (��� ������ ������, ���������� �� ��������)
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // ������� ���� ������
                playerController.TakeDamage(damageAmount);
            }
        }
    }
}
