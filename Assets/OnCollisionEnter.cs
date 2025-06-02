using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 10f;  // Количество урона
    public string playerTag = "Player";  // Тег для идентификации игрока

    // Метод, вызываемый при столкновении с объектом
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что мы столкнулись с объектом, у которого тег "Player"
        if (other.CompareTag(playerTag))
        {
            // Получаем компонент PlayerController (или другой скрипт, отвечающий за здоровье)
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                // Наносим урон игроку
                playerController.TakeDamage(damageAmount);
            }
        }
    }
}
