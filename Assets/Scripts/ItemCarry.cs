using UnityEngine;

public class ItemCarry : MonoBehaviour
{
    public Transform currentItem;    // Karakteri taşıyan obje
    public Transform correctItem;    // Tıklanacak doğru item
    public Transform dropPlace;      // Bırakılacak alan

    private bool isCarrying = false;
    private GameObject carriedObject;

    void Update()
    {
        // Mouse sol tuşu ile tıklama kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }

        // Taşıma işlemi aktifse
        if (isCarrying && carriedObject != null)
        {
            // Taşınan objeyi currentItem pozisyonuna yakınlaştırma
            carriedObject.transform.position = Vector3.Lerp(
                carriedObject.transform.position,
                currentItem.position + Vector3.up * 1f, 
                Time.deltaTime * 10f
            );
        }
    }

    void HandleClick()
    {
        // Raycast ile tıklanan objeyi buluyoruz
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (!isCarrying)
            {
                if (hit.transform == correctItem)
                {
                    // Doğru item'a tıklandıysa taşıma başlasın
                    isCarrying = true;
                    carriedObject = hit.transform.gameObject;
                    Debug.Log("Item alındı!");
                }
            }
            else
            {
                if (hit.transform == dropPlace)
                {
                    // Eğer bırakılacak yer seçildiyse, taşıma bitiyor
                    isCarrying = false;
                    carriedObject = null;
                    Debug.Log("Item bırakıldı!");
                }
            }
        }
    }
}