using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public Transform rabbitMesh;  // Ссылка на модель кролика (дочерний объект Player)

    void Start()
    {
        if (rabbitMesh != null)
        {
            // Устанавливаем модель кролика так, чтобы она стояла на лапках
            // Например, если капсула имеет высоту 2, то модель кролика будет смещена на 1.5 по оси Y
            rabbitMesh.localPosition = new Vector3(0f, 1.5f, 0f);
        }
        else
        {
            Debug.LogError("RabbitMesh не назначен в инспекторе!");
        }
    }
}