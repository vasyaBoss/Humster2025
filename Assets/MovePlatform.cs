using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Границы движения")]
    public Transform pointA;       // первая точка
    public Transform pointB;       // вторая точка

    [Header("Настройки скорости")]
    [Tooltip("Скорость: сколько раз платформа пробежит путь A→B за 1 секунду")]
    public float speed = 0.5f;

    private Rigidbody rb;

    void Awake()
    {
        // Если на платформе есть Rigidbody — делаем его кинематическим,
        // чтобы физика не пыталась его «сбросить» обратно
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }
    }

    void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;

        // t от 0 до 1 и обратно
        float t = Mathf.PingPong(Time.time * speed, 1f);
        Vector3 newPos = Vector3.Lerp(pointA.position, pointB.position, t);

        // Двигаем через MovePosition, чтобы не конфликтовать с физикой
        if (rb != null)
            rb.MovePosition(newPos);
        else
            transform.position = newPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
