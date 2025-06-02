using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // Ссылка на объект игрока (кролика)
    public Vector3 offset;          // Смещение камеры относительно игрока
    public float rotationSpeed = 5f; // Скорость вращения камеры

    private float currentYaw = 0f;   // Текущий угол вращения по оси Y (для камеры)

    void LateUpdate()
    {
        if (player != null)
        {
            // Получаем движение мыши по оси X
            float mouseX = Input.GetAxis("Mouse X");

            // Поворот камеры в зависимости от движения мыши (горизонтальный)
            currentYaw += mouseX * rotationSpeed;

            // Поворот камеры вокруг игрока (кролика)
            Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);

            // Новая позиция камеры, всегда за спиной кролика
            Vector3 desiredPosition = player.position - rotation * offset;

            // Устанавливаем позицию камеры
            transform.position = desiredPosition;

            // Камера всегда смотрит на кролика
            transform.LookAt(player);
        }
    }
}