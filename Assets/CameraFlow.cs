using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // ������ �� ������ ������ (�������)
    public Vector3 offset;          // �������� ������ ������������ ������
    public float rotationSpeed = 5f; // �������� �������� ������

    private float currentYaw = 0f;   // ������� ���� �������� �� ��� Y (��� ������)

    void LateUpdate()
    {
        if (player != null)
        {
            // �������� �������� ���� �� ��� X
            float mouseX = Input.GetAxis("Mouse X");

            // ������� ������ � ����������� �� �������� ���� (��������������)
            currentYaw += mouseX * rotationSpeed;

            // ������� ������ ������ ������ (�������)
            Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);

            // ����� ������� ������, ������ �� ������ �������
            Vector3 desiredPosition = player.position - rotation * offset;

            // ������������� ������� ������
            transform.position = desiredPosition;

            // ������ ������ ������� �� �������
            transform.LookAt(player);
        }
    }
}