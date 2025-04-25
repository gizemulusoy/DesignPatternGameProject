using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hızı (Inspector'dan ayarlanabilir)

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileşeni alınır
    }

    void Update()
    {
        // Ok tuşlarıyla input al
        movement.x = Input.GetAxisRaw("Horizontal"); // Sol/Sağ (← →)
        movement.y = Input.GetAxisRaw("Vertical");   // Yukarı/Aşağı (↑ ↓)
    }

    void FixedUpdate()
    {
        // Hareket ettir
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}

