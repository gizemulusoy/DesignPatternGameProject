using UnityEngine;

public class CarrySystem : MonoBehaviour
{
    private GameObject carryingObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carryingObject == null)
            {
                // Yakındaki objeleri ara
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);

                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Pickup"))
                    {
                        carryingObject = hit.gameObject;
                        carryingObject.transform.SetParent(transform); // Karakterin altına al
                        carryingObject.transform.localPosition = new Vector3(0, 0.5f, 0); // Omuz üstü gibi konumla
                        if (carryingObject.TryGetComponent<Rigidbody2D>(out var rb))
                        {
                            rb.simulated = false;
                        }

                        if (carryingObject.TryGetComponent<Collider2D>(out var col))
                        {
                            col.enabled = false;
                        }
                        break;
                    }
                }
            }
            else
            {
                // Bırak
                carryingObject.transform.SetParent(null); // Sahneden ayır
                carryingObject.transform.position = transform.position + new Vector3(0, -1f, 0); // Ayak dibine koy
                if (carryingObject.TryGetComponent<Rigidbody2D>(out var rb))
                {
                    rb.simulated = true; // Fizik geri gelsin
                }
                carryingObject = null;
            }
        }
    }
}