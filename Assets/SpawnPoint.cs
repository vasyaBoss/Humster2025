using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool isActivated = false; // Чтобы не срабатывало по 10 раз

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetSpawnPoint(transform.position);
                isActivated = true;

                Debug.Log($"Спавнпоинт активирован на позиции {transform.position}");

                // Если хочешь добавить визуальный или аудио-эффект, можно здесь:
                // PlayCheckpointEffect();
            }
        }
    }
}
