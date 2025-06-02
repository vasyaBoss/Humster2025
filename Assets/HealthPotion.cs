using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 20f;      // ���������� ��������, ������� ��������������� ���� ������
    public AudioClip healSound;         // ����, ������� ������������� ��� �������
    public AudioClip crunchSound;       // ���� ������ ��� �����������
    public float moveHeight = 0.5f;     // ������, �� ������� ����� �����������/���������� ������
    public float moveSpeed = 1f;        // �������� �������� �����/����

    private Vector3 startPosition;      // ��������� ������� �������
    private bool movingUp = true;       // ����, �����������, ��������� �� ������� ����� ��� ����

    void Start()
    {
        // ��������� ��������� ������� �������
        startPosition = transform.position;
    }

    void Update()
    {
        // ������� ������� �����-����
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.up * moveHeight, moveSpeed * Time.deltaTime);
            if (transform.position.y >= startPosition.y + moveHeight)
            {
                movingUp = false;  // ������ ����������� �� ���������
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition - Vector3.up * moveHeight, moveSpeed * Time.deltaTime);
            if (transform.position.y <= startPosition.y - moveHeight)
            {
                movingUp = true;  // ������ ����������� �� ��������
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ��� ������, ������� ����� � �������, �������� �������
        if (other.CompareTag("Player"))
        {
            // �������� ��������� PlayerController �� ������
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Heal(healAmount);  // ��������������� �������� ������
                Debug.Log("����� ����������� �������� �� " + healAmount);

                // ����������� ����, ���� �� �����
                if (healSound != null)
                {
                    AudioSource.PlayClipAtPoint(healSound, transform.position);
                }

                // ����������� ���� ������ ����� ������������
                if (crunchSound != null)
                {
                    AudioSource.PlayClipAtPoint(crunchSound, transform.position);
                }

                // ���������� ������� ����� �������������
                Destroy(gameObject);
            }
        }
    }
}
