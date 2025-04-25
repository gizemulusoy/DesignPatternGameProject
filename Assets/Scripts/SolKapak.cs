using UnityEngine;

public class SolKapak : MonoBehaviour
{
    public Vector3 hedefPozisyon = new Vector3(0.01f, -0.06f, 0f);
    public float hiz = 2f;
    private bool hareketEt = false;

    void Update()
    {
        if (hareketEt)
        {
            transform.position = Vector3.MoveTowards(transform.position, hedefPozisyon, hiz * Time.deltaTime);
        }
    }

    public void KapağıTak()
    {
        hareketEt = true;
    }
}