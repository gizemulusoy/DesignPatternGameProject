using UnityEngine;

public class SimpleRaycastTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Debug.Log("Tıkladığın obje: " + hit.transform.name);
            }
            else
            {
                Debug.Log("Hiçbir şeye tıklanmadı!");
            }
        }
    }
}