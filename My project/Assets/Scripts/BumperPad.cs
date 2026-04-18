using UnityEngine;
public class BumperPad : MonoBehaviour
{
    [Header("Bumper Settings")]
    public float bounceForce = 15f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 vel = playerRb.linearVelocity;
                vel.y = 0f;
                playerRb.linearVelocity = vel;
                playerRb.AddForce(transform.up * bounceForce, ForceMode.Impulse);

                Debug.Log("Bàn đạp nảy kích hoạt!");
            }
        }
    }
}
