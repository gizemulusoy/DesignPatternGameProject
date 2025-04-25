using System.Collections.Generic;
using UnityEngine;

public class acikKahveDolap : MonoBehaviour
{
    public float hiz = 2f;
    private bool hareketEt = false;

    private List<GameObject> oncelikliObjeler = new List<GameObject>();
    private List<Vector3> oncelikliPozisyonlar = new List<Vector3>();

    private List<GameObject> ortaObjeler = new List<GameObject>();
    private List<Vector3> ortaPozisyonlar = new List<Vector3>();

    private List<GameObject> sonObjeler = new List<GameObject>();
    private List<Vector3> sonPozisyonlar = new List<Vector3>();

    private int oncelikIndex = 0;
    private int ortaIndex = 0;
    private int sonIndex = 0;

    private bool onceliklerBitti = false;
    private bool ortalarBitti = false;

    private void Start()
    {
        // Objeleri buluyoruz
        GameObject obj1 = GameObject.Find("dolapArka1");
        GameObject obj2 = GameObject.Find("dolapUstu2");
        GameObject obj3 = GameObject.Find("sagDuvar3");
        GameObject obj4 = GameObject.Find("solDuvar4");
        GameObject obj5 = GameObject.Find("solKapak5");
        GameObject obj6 = GameObject.Find("dolapAlt6");
        GameObject obj7 = GameObject.Find("rafSol7");
        GameObject obj8 = GameObject.Find("sagKapak8");
        GameObject obj9 = GameObject.Find("ustRaf9");
        GameObject obj10 = GameObject.Find("ortaRaf10");
        GameObject obj11 = GameObject.Find("altKapak11");

        // Öncelikli objeler: 7, 9, 10
        oncelikliObjeler.Add(obj7);  // rafSol7
        oncelikliPozisyonlar.Add(new Vector3(3.4455f, -1.11f, 0f));

        oncelikliObjeler.Add(obj9);  // ustRaf9
        oncelikliPozisyonlar.Add(new Vector3(4.21f, -0.32f, 0f));

        oncelikliObjeler.Add(obj10); // ortaRaf10
        oncelikliPozisyonlar.Add(new Vector3(4.03f, -1.08f, 0f));

        // Orta sırada gelen objeler: 1, 2, 3, 4, 6, 11
        ortaObjeler.Add(obj1);
        ortaPozisyonlar.Add(new Vector3(2.97f, 0.1345f, 0f));

        ortaObjeler.Add(obj2);
        ortaPozisyonlar.Add(new Vector3(3.1362f, 2.2597f, 0f));

        ortaObjeler.Add(obj3);
        ortaPozisyonlar.Add(new Vector3(4.64f, 0.16f, 0f));

        ortaObjeler.Add(obj4);
        ortaPozisyonlar.Add(new Vector3(1.48f, 0.05f, 0f));

        ortaObjeler.Add(obj6);
        ortaPozisyonlar.Add(new Vector3(3.12f, -1.9f, 0f));

        ortaObjeler.Add(obj11);
        ortaPozisyonlar.Add(new Vector3(3.37f, -2.23f, 0f));

        // En son gelen objeler: 5, 8
        sonObjeler.Add(obj5); // solKapak5
        sonPozisyonlar.Add(new Vector3(2.37f, 0.1597f, 0f));

        sonObjeler.Add(obj8); // sagKapak8
        sonPozisyonlar.Add(new Vector3(5.52f, 0.13f, 0f));
    }

    void Update()
    {
        if (!hareketEt) return;

        // Öncelikli objeleri sırayla hareket ettir
        if (!onceliklerBitti)
        {
            if (oncelikIndex < oncelikliObjeler.Count)
            {
                HareketEttir(oncelikliObjeler, oncelikliPozisyonlar, ref oncelikIndex);
            }
            else
            {
                onceliklerBitti = true;
            }
        }
        // Orta gruptaki objeleri sırayla hareket ettir
        else if (!ortalarBitti)
        {
            if (ortaIndex < ortaObjeler.Count)
            {
                HareketEttir(ortaObjeler, ortaPozisyonlar, ref ortaIndex);
            }
            else
            {
                ortalarBitti = true;
            }
        }
        // En son grup
        else
        {
            if (sonIndex < sonObjeler.Count)
            {
                HareketEttir(sonObjeler, sonPozisyonlar, ref sonIndex);
            }
        }
    }

    private void HareketEttir(List<GameObject> objeler, List<Vector3> hedefler, ref int index)
    {
        GameObject aktif = objeler[index];
        Vector3 hedef = hedefler[index];
        aktif.transform.position = Vector3.MoveTowards(aktif.transform.position, hedef, hiz * Time.deltaTime);

        if (Vector3.Distance(aktif.transform.position, hedef) < 0.01f)
        {
            index++;
        }
    }

    public void KapağıTak()
    {
        hareketEt = true;
    }
}
