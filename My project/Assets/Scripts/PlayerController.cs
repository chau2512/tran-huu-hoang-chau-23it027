using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 10f;
    public float currentSpeed;

    [Header("Spawn Settings")]
    public Vector3 spawnPoint;

    [Header("Score")]
    public int energyCoreCount = 0;

    private Rigidbody rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPoint = transform.position;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        moveInput = Vector2.zero;

        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) moveInput.y += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) moveInput.y -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) moveInput.x += 1f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) moveInput.x -= 1f;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.AddForce(movement * currentSpeed);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Respawn();
        }
    }
    public void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnPoint;
        currentSpeed = baseSpeed;

        Debug.Log("Robot đã bị phá hủy! Quay về điểm xuất phát.");
    }
    public void ActivateSpeedBoost()
    {
        currentSpeed = baseSpeed * 2f;
        Debug.Log("Kích hoạt tăng tốc!");
    }
    public void CollectEnergyCore()
    {
        energyCoreCount++;
        Debug.Log("Đã thu thập lõi năng lượng! [" + energyCoreCount + "]");
    }
}
