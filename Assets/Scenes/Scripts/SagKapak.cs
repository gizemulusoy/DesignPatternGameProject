using UnityEngine;

public class SagKapak : MonoBehaviour
{
    public Vector3 hedefPozisyon = new Vector3(3.32f, 0.59f, 0f);
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

