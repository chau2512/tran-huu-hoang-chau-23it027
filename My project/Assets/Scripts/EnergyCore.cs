using UnityEngine;
public class EnergyCore : MonoBehaviour
{
    [Header("Floating Animation")]
    public float floatSpeed = 2f;
    public float floatAmplitude = 0.3f;
    public float rotateSpeed = 50f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectEnergyCore();
            }
            Destroy(gameObject);
        }
    }
}
