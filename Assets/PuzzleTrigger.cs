using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject platform;  // ���������, ������� ����� ���������
    public float targetY = -5f;  // ������� ������ ��������� (��������, ��������� ���������� �� -5 �� ��� Y)
    public float moveSpeed = 2f; // �������� �������� ���������

    private bool isTriggered = false;  // ����, ����������� �� �������
    private float startY;    // ��������� ������ ���������

    // ���� ��������� ��� ��������
    public AudioClip platformMoveSound;  // ���� �������� ���������
    private AudioSource audioSource;     // �������� �����

    // ���� ������� �����������
    public AudioClip puzzleSolvedSound;  // ���� ������� �����������

    void Start()
    {
        // ��������� ��������� ������� ���������
        startY = platform.transform.position.y;
        Debug.Log("Start Position: " + startY);  // �������� ��������� ������� ���������

        // ������������� ���������� AudioSource
        audioSource = platform.GetComponent<AudioSource>();
        if (audioSource == null)  // ���� AudioSource ��� �� ���������, ������ ���
        {
            audioSource = platform.AddComponent<AudioSource>();
        }
    }

    // ����� ������ ������ � �������
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))  // ���������, ��� �� ������� ������ �������
        {
            isTriggered = true;  // ���������� �������� ���������
            PlayPlatformMoveSound();  // ����������� ���� �������� ���������

            // ����������� ���� ������� ����������� ���� ���
            if (puzzleSolvedSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(puzzleSolvedSound);  // ����������� ���� ���� ���
            }
        }
    }

    // ����� ������ ������� �� ��������
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))  // ���� ������� ������� �� ��������
        {
            isTriggered = false;  // ������������� �������� ���������
        }
    }

    void Update()
    {
        // �������� ������� ������� ��������� ��� �������
        Debug.Log("Current Platform Position Y: " + platform.transform.position.y);

        // ���� ������� ����������� (�.�. ������� �� ��������), �������� �������� ��������� ����
        if (isTriggered)
        {
            // ������� ���������
            platform.transform.position = new Vector3(
                platform.transform.position.x,  // ��������� X ���������� ����������
                Mathf.MoveTowards(platform.transform.position.y, targetY, moveSpeed * Time.deltaTime),  // ������� ������ �� Y
                platform.transform.position.z   // ��������� Z ���������� ����������
            );

            // ���� ��������� ��� ���������, ���������� ��������������� �����
            if (!audioSource.isPlaying)
            {
                PlayPlatformMoveSound();
            }
        }
        // ���� ������� ������� �� ��������, ���������� ��������� � ��������� ��������� �� Y
        else
        {
            platform.transform.position = new Vector3(
                platform.transform.position.x,  // ��������� X ���������� ����������
                Mathf.MoveTowards(platform.transform.position.y, startY, moveSpeed * Time.deltaTime),  // ���������� ������ �� Y
                platform.transform.position.z   // ��������� Z ���������� ����������
            );
        }

        // ���� ��������� ������������, ������������� ����
        if (platform.transform.position.y == startY || platform.transform.position.y == targetY)
        {
            StopPlatformMoveSound();
        }
    }

    // ����� ��� ������������ ����� �������� ���������
    private void PlayPlatformMoveSound()
    {
        if (platformMoveSound != null && audioSource != null)
        {
            audioSource.clip = platformMoveSound;  // ������������� ���� ��� ��������
            audioSource.loop = true;  // �������� ������������
            audioSource.Play();  // ����������� ����
        }
    }

    // ����� ��� ��������� ����� �������� ���������
    private void StopPlatformMoveSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();  // ������������� ����
        }
    }
}
