using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float healAmount = 20f;      // Количество здоровья, которое восстанавливает этот объект
    public AudioClip healSound;         // Звук, который проигрывается при лечении
    public AudioClip crunchSound;       // Звук хруста при уничтожении
    public float moveHeight = 0.5f;     // Высота, на которую будет подниматься/опускаться объект
    public float moveSpeed = 1f;        // Скорость движения вверх/вниз

    private Vector3 startPosition;      // Начальная позиция аптечки
    private bool movingUp = true;       // Флаг, указывающий, двигается ли аптечка вверх или вниз

    void Start()
    {
        // Сохраняем начальную позицию аптечки
        startPosition = transform.position;
    }

    void Update()
    {
        // Двигаем аптечку вверх-вниз
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.up * moveHeight, moveSpeed * Time.deltaTime);
            if (transform.position.y >= startPosition.y + moveHeight)
            {
                movingUp = false;  // Меняем направление на опускание
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition - Vector3.up * moveHeight, moveSpeed * Time.deltaTime);
            if (transform.position.y <= startPosition.y - moveHeight)
            {
                movingUp = true;  // Меняем направление на поднятие
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что объект, который вошел в триггер, является игроком
        if (other.CompareTag("Player"))
        {
            // Получаем компонент PlayerController на игроке
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.Heal(healAmount);  // Восстанавливаем здоровье игрока
                Debug.Log("Игрок восстановил здоровье на " + healAmount);

                // Проигрываем звук, если он задан
                if (healSound != null)
                {
                    AudioSource.PlayClipAtPoint(healSound, transform.position);
                }

                // Проигрываем звук хруста перед уничтожением
                if (crunchSound != null)
                {
                    AudioSource.PlayClipAtPoint(crunchSound, transform.position);
                }

                // Уничтожаем аптечку после использования
                Destroy(gameObject);
            }
        }
    }
}
