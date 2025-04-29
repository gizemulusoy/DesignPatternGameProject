using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ItemPickUp : MonoBehaviour
{
    public Transform urunTasimaNoktasi;  // Ürünün taşınacağı nokta (Karakterin kafası)
    public TMP_Text questText;  // Görev mesajlarını gösterecek alan

    private GameObject tasinanUrun = null;  // Taşınan ürün

    // Layer Mask değişkeni (Deterjan katmanını kullanıyoruz)
    public LayerMask urunLayerMask;

    private void Update()
    {
        // Eğer UI üzerine tıklanıyorsa işlemi yapma
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Mouse'a tıklanırsa
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // Ekrandaki mouse pozisyonundan ray oluştur

            RaycastHit hit;  // Raycast sonucu çarpan objeyi alacağız

            // Layer Mask kullanarak sadece "Deterjan" katmanındaki objelere tıklanabilir
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, urunLayerMask))
            {
                Debug.Log("Tıkladığın obje: " + hit.collider.gameObject.name); // Tıklanan objeyi yazdır

                GameObject hedef = hit.collider.gameObject;

                if (hedef.CompareTag("Urun"))  // Eğer doğru objeye tıklandıysa
                {
                    Debug.Log("Doğru ürüne tıkladın!");

                    if (tasinanUrun == null)  // Eğer taşıdığımız bir ürün yoksa
                    {
                        tasinanUrun = hedef;  // Tıklanan objeyi taşıma işlemine al
                        tasinanUrun.transform.SetParent(urunTasimaNoktasi);  // Ürünü taşınacak noktaya (karakterin kafasına) yerleştir
                        tasinanUrun.transform.localPosition = Vector3.zero;  // Kafanın üstüne yerleştir (sıfırlama)
                        tasinanUrun.GetComponent<Rigidbody>().isKinematic = true;  // Fiziksel etkileşime girme (hareket etmesin)
                        Debug.Log("Ürün başarıyla alındı.");
                    }
                }
                else
                {
                    Debug.LogWarning("Yanlış objeye tıkladın!");  // Yanlış objeye tıklanırsa
                    questText.text = "Yanlış ürün!";
                }
            }
            else
            {
                Debug.Log("Hiçbir objeye tıklanmadı.");  // Hiçbir objeye tıklanmadıysa
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Eğer doğru alana girersek ve taşınan ürün varsa
        if (other.CompareTag("HedefAlan") && tasinanUrun != null)
        {
            Debug.Log("Doğru alana bırakıldı!");  // Alan doğruysa

            tasinanUrun.transform.SetParent(null);  // Ürünü alandan çıkart
            tasinanUrun.GetComponent<Rigidbody>().isKinematic = false;  // Fiziksel etkileşimi geri ver
            tasinanUrun = null;  // Taşınan ürünü sıfırla

            questText.text = "Görevi tamamladın, şimdi müdürün yanına dönmelisin.";  // Görev tamamlandığında mesaj göster
        }
    }
}
