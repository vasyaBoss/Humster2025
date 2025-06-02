using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    public float bounceForce = 10f;
    public AudioClip bounceSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
               
                Vector3 velocity = playerRb.linearVelocity;
                velocity.y = 0f;
                playerRb.linearVelocity = velocity;

                
                playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);

               
                if (bounceSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(bounceSound);
                }

                Debug.Log("Прыжок!");
            }
        }
    }
}
