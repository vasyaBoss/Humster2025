using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public Transform rabbitMesh;  // ������ �� ������ ������� (�������� ������ Player)

    void Start()
    {
        if (rabbitMesh != null)
        {
            // ������������� ������ ������� ���, ����� ��� ������ �� ������
            // ��������, ���� ������� ����� ������ 2, �� ������ ������� ����� ������� �� 1.5 �� ��� Y
            rabbitMesh.localPosition = new Vector3(0f, 1.5f, 0f);
        }
        else
        {
            Debug.LogError("RabbitMesh �� �������� � ����������!");
        }
    }
}