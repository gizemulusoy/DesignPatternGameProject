using UnityEngine;
using TMPro;  // TextMeshPro kullanabilmek için gerekli namespace

public class NameTag : MonoBehaviour
{
    private string objectName; // Objelerin ismini tutacak değişken
    private Renderer objectRenderer; // Karakterin renderer'ını alıyoruz
    public Color boxColor = new Color(0, 0, 0, 0.5f); // Kutu rengi (yarı şeffaf siyah)
    public TMP_Text nameTagText; // TMP Text component

    void Start()
    {
        // Karakterin ismini objenin ismi olarak alıyoruz
        objectName = gameObject.name;

        // Karakterin Renderer'ını alıyoruz
        objectRenderer = gameObject.GetComponent<Renderer>();

        // Eğer bir TextMeshPro bileşeni eklenmemişse, ekle
        if (nameTagText == null)
        {
            GameObject textObject = new GameObject("NameTag");
            textObject.transform.SetParent(transform); // Bu objeyi karakterin alt objesi yapıyoruz
            textObject.transform.localPosition = Vector3.up * (objectRenderer.bounds.extents.y + 1f); // Kafanın üst kısmına biraz daha yüksek yerleştiriyoruz
            nameTagText = textObject.AddComponent<TMP_Text>(); // TMP_Text bileşenini ekliyoruz

            // TMP_Text ayarları
            nameTagText.fontSize = 8; // Font boyutunu ayarlıyoruz
            nameTagText.color = Color.white; // Yazı rengini ayarlıyoruz
            nameTagText.alignment = TextAlignmentOptions.Center; // Ortalamak için
        }

        // Etiket metnini objenin adı olarak ayarlıyoruz
        nameTagText.text = objectName;
    }

    void Update()
    {
        if (nameTagText != null)
        {
            // Objeyi ekranda doğru pozisyona yerleştiriyoruz
            Vector3 objectPosition = transform.position + Vector3.up * (objectRenderer.bounds.extents.y);

            // Dünya koordinatından ekran koordinatına dönüştür
            Vector3 screenPos = Camera.main.WorldToScreenPoint(objectPosition);

            // Ekranda doğru pozisyonda gösterilmesi için
            nameTagText.transform.position = screenPos;
        }
    }
}
