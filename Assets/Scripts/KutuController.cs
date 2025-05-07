using UnityEngine;
using System.Collections;

public class KutuController : MonoBehaviour
{
    public GameObject kapaliKutu;
    public GameObject acikKutu;

    public GameObject kahveArka;
    public GameObject kahveUst;
    public GameObject kahveSol;
    public GameObject kahveSag;
    public GameObject kahveKapakSag;
    public GameObject kahveKapakSol;
    public GameObject kahveAlt;
    public GameObject kahveAltOn;
    public GameObject kahveRafSol;
    public GameObject kahveRaf1;
    public GameObject kahveRaf2;

    public GameObject arrowObject; // Ok objesi

    private bool isOpened = false;
    private bool kutuyaTiklanabilir = false;

    // Kutu pozisyon verileri
    public Vector3 kutuHedefPozisyon = new Vector3(-4.89f, -2.78f, 0f);
    public float kutuGirisSuresi = 2f;

    void Start()
    {
        kapaliKutu.transform.position = new Vector3(-25f, -2.78f, 0f);
        kapaliKutu.SetActive(true);
        acikKutu.SetActive(false);

        arrowObject.SetActive(false); // Ok başlangıçta görünmesin

        StartCoroutine(MoveToPosition(kapaliKutu, kutuHedefPozisyon, kutuGirisSuresi, OnKutuGeldiktenSonra));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && kutuyaTiklanabilir && !isOpened)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit != null && hit.gameObject == kapaliKutu)
            {
                arrowObject.SetActive(false); // Ok gizlenir
                StartCoroutine(OpenBox());
            }
        }
    }

    IEnumerator OpenBox()
    {
        isOpened = true;
        kapaliKutu.SetActive(false);
        acikKutu.SetActive(true);

        kahveArka.SetActive(true);
        kahveUst.SetActive(true);
        kahveSol.SetActive(true);
        kahveSag.SetActive(true);
        kahveKapakSag.SetActive(true);
        kahveKapakSol.SetActive(true);
        kahveAlt.SetActive(true);
        kahveAltOn.SetActive(true);
        kahveRafSol.SetActive(true);
        kahveRaf1.SetActive(true);
        kahveRaf2.SetActive(true);

        yield return new WaitForSeconds(2f);

        StartCoroutine(MoveToPosition(kahveArka, new Vector3(5.56f, 0.21f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveSag, new Vector3(6.72f, 0.16f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveSol, new Vector3(4.42f, 0.12f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveUst, new Vector3(5.67f, 1.45f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveAlt, new Vector3(5.64f, -1.1f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveAltOn, new Vector3(5.84f, -1.33f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveRafSol, new Vector3(5.93f, -0.58f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveRaf1, new Vector3(6.4f, -0.06f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveRaf2, new Vector3(6.33f, -0.58f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveKapakSag, new Vector3(7.24f, 0.11f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveKapakSol, new Vector3(4.97f, 0.14f, 0f), 1.5f));
    }

    IEnumerator MoveToPosition(GameObject obj, Vector3 target, float duration, System.Action onComplete = null)
    {
        Vector3 start = obj.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(start, target, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = target;

        if (onComplete != null)
            onComplete();
    }

    // Kutu sahnedeki yerine ulaştıktan sonra yapılacaklar
    void OnKutuGeldiktenSonra()
    {
        kutuyaTiklanabilir = true;
        arrowObject.SetActive(true); // Oku göster
    }
}
