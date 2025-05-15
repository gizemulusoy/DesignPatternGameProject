using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KutuController : MonoBehaviour
{
    public GameObject kapaliKutu;
    public GameObject acikKutu;

    public GameObject kapaliKutu2;
    public GameObject acikKutu2;

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
    
    // İkinci Kutu
    public GameObject griArka;
    public GameObject griUst;
    public GameObject griSol;
    public GameObject griSag;
    public GameObject griKapakSag;
    public GameObject griKapakSol;
    public GameObject griAlt;
    public GameObject griAltOn;
    public GameObject griRafSol;
    public GameObject griRaf1;
    public GameObject griRaf2;

    public GameObject arrowObject;   // İlk kutu için ok
    public GameObject arrowObject2;  // İkinci kutu için ok

    private bool isOpened = false;
    private bool kutuyaTiklanabilir = false;

    private bool isOpened2 = false;
    private bool kutuyaTiklanabilir2 = false;

    public Vector3 kutuHedefPozisyon = new Vector3(-4.56f, -2.78f, 0f);
    public float kutuGirisSuresi = 2f;

    public Vector3 kutu2HedefPozisyon = new Vector3(4.56f, -2.78f, 0f);
    public float kutu2GirisSuresi = 2f;

    void Start()
    {
        // İlk kutu ayarları
        kapaliKutu.transform.position = new Vector3(-25f, -2.78f, 0f);
        kapaliKutu.SetActive(true);
        acikKutu.SetActive(false);

        // İkinci kutu ayarları
        kapaliKutu2.transform.position = new Vector3(-25f, -2.78f, 0f);
        kapaliKutu2.SetActive(true);
        acikKutu2.SetActive(false);

        // Okları gizle
        arrowObject.SetActive(false);
        arrowObject2.SetActive(false);

        // İlk kutuyu sahneye getir
        StartCoroutine(MoveToPosition(kapaliKutu, kutuHedefPozisyon, kutuGirisSuresi, OnKutuGeldiktenSonra));
        StartCoroutine(MoveToPosition(kapaliKutu2, kutu2HedefPozisyon, kutu2GirisSuresi, null));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (kutuyaTiklanabilir && !isOpened && hit != null && hit.gameObject == kapaliKutu)
            {
                arrowObject.SetActive(false);
                StartCoroutine(OpenBox());
            }

            if (kutuyaTiklanabilir2 && !isOpened2 && hit != null && hit.gameObject == kapaliKutu2)
            {
                arrowObject2.SetActive(false);
                StartCoroutine(OpenBox2());
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

        yield return StartCoroutine(MoveToPosition(kahveArka, new Vector3(-4.29f, 2.4f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveSag, new Vector3(-3.1f, 2.35f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveSol, new Vector3(-5.45f, 2.32f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveUst, new Vector3(-4.11f, 3.66f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveAlt, new Vector3(-4.13f, 1.1f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveAltOn, new Vector3(-3.98f, 0.89f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveRafSol, new Vector3(-3.8f, 1.6f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveRaf1, new Vector3(-3.38f, 2.12f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveRaf2, new Vector3(-3.38f, 1.54f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(kahveKapakSag, new Vector3(-2.53f, 2.32f, 0f), 1.5f));
        yield return StartCoroutine(MoveToPosition(kahveKapakSol, new Vector3(-4.89f, 2.35f, 0f), 1.5f));

        // İlk kutu animasyonları bitti, şimdi ikinci ok görünsün
        kutuyaTiklanabilir2 = true;
        arrowObject2.SetActive(true);
    }

    IEnumerator OpenBox2()
    {
        isOpened2 = true;
        kapaliKutu2.SetActive(false);
        acikKutu2.SetActive(true);

        griArka.SetActive(true);
        griUst.SetActive(true);
        griSol.SetActive(true);
        griSag.SetActive(true);
        griKapakSag.SetActive(true);
        griKapakSol.SetActive(true);
        griAlt.SetActive(true);
        griAltOn.SetActive(true);
        griRafSol.SetActive(true);
        griRaf1.SetActive(true);
        griRaf2.SetActive(true);

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(MoveToPosition(griArka, new Vector3(4.01f, 2.4f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griSag, new Vector3(5.2f, 2.35f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griSol, new Vector3(2.85f, 2.32f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griUst, new Vector3(4.19f, 3.66f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griAlt, new Vector3(4.17f, 1.1f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griAltOn, new Vector3(4.32f, 0.89f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griRafSol, new Vector3(4.5f, 1.6f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griRaf1, new Vector3(4.92f, 2.12f, 0f), 1.5f));
        StartCoroutine(MoveToPosition(griRaf2, new Vector3(4.92f, 1.54f, 0f), 1.5f));
    
        // YENİ EKLENEN KISIM: griKapak hareketleri
        yield return StartCoroutine(MoveToPosition(griKapakSag, new Vector3(5.77f, 2.32f, 0f), 1.5f));
        yield return StartCoroutine(MoveToPosition(griKapakSol, new Vector3(3.41f, 2.35f, 0f), 1.5f));
        
        yield return new WaitForSeconds(2f);  // istersen geçiş öncesi bekle

        SceneManager.LoadScene("Scene2");
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

    void OnKutuGeldiktenSonra()
    {
        kutuyaTiklanabilir = true;
        arrowObject.SetActive(true);
    }
}
