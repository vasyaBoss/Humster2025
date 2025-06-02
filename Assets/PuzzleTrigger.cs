using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public GameObject platform;  // Платформа, которая будет двигаться
    public float targetY = -5f;  // Целевая высота платформы (например, платформа опускается на -5 по оси Y)
    public float moveSpeed = 2f; // Скорость движения платформы

    private bool isTriggered = false;  // Флаг, активирован ли триггер
    private float startY;    // Начальная высота платформы

    // Звук платформы при движении
    public AudioClip platformMoveSound;  // Звук движения платформы
    private AudioSource audioSource;     // Источник звука

    // Звук решения головоломки
    public AudioClip puzzleSolvedSound;  // Звук решения головоломки

    void Start()
    {
        // Сохраняем начальную позицию платформы
        startY = platform.transform.position.y;
        Debug.Log("Start Position: " + startY);  // Логируем начальную позицию платформы

        // Инициализация компонента AudioSource
        audioSource = platform.GetComponent<AudioSource>();
        if (audioSource == null)  // Если AudioSource нет на платформе, создаём его
        {
            audioSource = platform.AddComponent<AudioSource>();
        }
    }

    // Когда объект входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))  // Проверяем, что на триггер попала коробка
        {
            isTriggered = true;  // Активируем движение платформы
            PlayPlatformMoveSound();  // Проигрываем звук движения платформы

            // Проигрываем звук решения головоломки один раз
            if (puzzleSolvedSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(puzzleSolvedSound);  // Проигрываем звук один раз
            }
        }
    }

    // Когда объект выходит из триггера
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))  // Если коробка выходит из триггера
        {
            isTriggered = false;  // Останавливаем движение платформы
        }
    }

    void Update()
    {
        // Логируем текущую позицию платформы для отладки
        Debug.Log("Current Platform Position Y: " + platform.transform.position.y);

        // Если триггер активирован (т.е. коробка на триггере), начинаем движение платформы вниз
        if (isTriggered)
        {
            // Двигаем платформу
            platform.transform.position = new Vector3(
                platform.transform.position.x,  // Оставляем X координату постоянной
                Mathf.MoveTowards(platform.transform.position.y, targetY, moveSpeed * Time.deltaTime),  // Двигаем только по Y
                platform.transform.position.z   // Оставляем Z координату постоянной
            );

            // Если платформа ещё двигается, продолжаем воспроизведение звука
            if (!audioSource.isPlaying)
            {
                PlayPlatformMoveSound();
            }
        }
        // Если коробка выходит из триггера, возвращаем платформу в начальное положение по Y
        else
        {
            platform.transform.position = new Vector3(
                platform.transform.position.x,  // Оставляем X координату постоянной
                Mathf.MoveTowards(platform.transform.position.y, startY, moveSpeed * Time.deltaTime),  // Возвращаем только по Y
                platform.transform.position.z   // Оставляем Z координату постоянной
            );
        }

        // Если платформа остановилась, останавливаем звук
        if (platform.transform.position.y == startY || platform.transform.position.y == targetY)
        {
            StopPlatformMoveSound();
        }
    }

    // Метод для проигрывания звука движения платформы
    private void PlayPlatformMoveSound()
    {
        if (platformMoveSound != null && audioSource != null)
        {
            audioSource.clip = platformMoveSound;  // Устанавливаем звук для движения
            audioSource.loop = true;  // Включаем зацикливание
            audioSource.Play();  // Проигрываем звук
        }
    }

    // Метод для остановки звука движения платформы
    private void StopPlatformMoveSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();  // Останавливаем звук
        }
    }
}
