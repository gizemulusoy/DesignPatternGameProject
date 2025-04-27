using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hızı
    private Rigidbody2D rb;
    private Vector2 movement;

    public bool isSelected = false; // Seçili mi?

    private Collider2D clickCollider; // Empty child'taki collider

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        clickCollider = transform.Find("ClickCollider").GetComponent<Collider2D>(); // Empty child'ı bul, ismi doğru olduğundan emin olun
    }

    void Update()
    {
        // Raycast ile tıklama kontrolü
        if (Input.GetMouseButtonDown(0)) // Sol fare tuşu
        {
            // Mouse pozisyonunu dünyaya dönüştür
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Raycast at
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // Raycast başarılıysa debug mesajı yazdır
            if (hit.collider != null)
            {
                Debug.Log("Raycast tıklama başarılı: " + hit.collider.name);

                if (hit.collider == clickCollider) // Eğer tıklanan collider empty child ise
                {
                    isSelected = true; // Seçildi!
                    SelectionManager.Instance.Select(this); // Seçim işlemi
                    Debug.Log("Tıklandı: " + gameObject.name); // Debug log
                    Debug.Log("Seçili mi? " + isSelected); // Debug: Seçili durumu kontrol et
                }
            }
            else
            {
                Debug.Log("Raycast başarısız.");
            }
        }

        // Seçili değilse hareket etmeyi engelle
        if (!isSelected)
        {
            movement = Vector2.zero; // Hareketi sıfırlıyoruz
            return; // Seçilmediyse hareket etme
        }

        // Yürüyüş kodu: 
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Debug: Hareketin doğru olup olmadığını kontrol et
        Debug.Log("Movement X: " + movement.x + " Y: " + movement.y);
    }

    void FixedUpdate()
    {
        // Hareket ettir
        if (isSelected) // Sadece seçiliyse hareket et
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
