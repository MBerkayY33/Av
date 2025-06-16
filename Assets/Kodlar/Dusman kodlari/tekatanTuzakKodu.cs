using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tekatanTuzakKodu : MonoBehaviour
{
    private AvciOzellikleri avciOzellikleri;
    private float[] saldiriBilgileri = new float[2];

    private void Awake()
    {
        avciOzellikleri = GameObject.Find("Avci").GetComponent<AvciOzellikleri>();
    }
    private void Start()
    {
        saldiriBilgileri[0] = 500f;
        saldiriBilgileri[1] = transform.position.x;
    }
    private void OnCollisionEnter2D(Collision2D collision)//*** avcinin tuzaða deymesi durumunda ona 100 hasar verir.
    {
        if(collision.gameObject.tag == "Player")
        {
            avciOzellikleri.CanAzalt(100);
        }
        if(collision.gameObject.tag == "enemy")
        {
            collision.transform.SendMessage("HasarAl", saldiriBilgileri);
        }
    }
}
