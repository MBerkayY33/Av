using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKodu : MonoBehaviour
{
    private Rigidbody2D rb;
    public float mermiHasari;

    private float[] saldiriBilgileri = new float[2];
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        saldiriBilgileri[0] = mermiHasari;
        saldiriBilgileri[1] = transform.position.x;
    }
    private void OnCollisionEnter2D(Collision2D collision)//***merminin colliderýnýn çarptýðý nesnenin adýna veya etikeine göre hasarAl metodu tetiklenir
    {
        rb.velocity = new Vector2(0, 0);
        if (collision.gameObject.name == "idmanKuklasi")
        {
            collision.gameObject.GetComponent<ÝdmanKuklasi>().hasarAliyorMu = true;
        }
        else if (collision.gameObject.tag == "enemy")
        {
            collision.transform.SendMessage("HasarAl", saldiriBilgileri);
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.transform.SendMessage("HasarAl", saldiriBilgileri);
        }

        DestroyBullet();//mermiyi yok eder
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
