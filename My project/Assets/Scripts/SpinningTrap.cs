using UnityEngine;
public class SpinningTrap : MonoBehaviour
{
    [Header("Spin Settings")]
    public Vector3 rotationAxis = Vector3.up;
    public float rotationSpeed = 100f;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
