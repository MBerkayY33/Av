using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bayrakkodu : MonoBehaviour
{
    private OyunYoneticisi OY;
    private SpriteRenderer SR;
    private Collider2D Collider;
    public Transform yenidenDogmaNoktasi;
    public Sprite pasif, aktif;

    private void Awake()
    {
        OY = GameObject.FindGameObjectWithTag("OyunYoneticisi").GetComponent<OyunYoneticisi>();
        SR = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.kontrolNoktasiSesi);
            OY.KontrolNoktasiniGuncelle(yenidenDogmaNoktasi.position);
            SR.sprite = aktif;
            Collider.enabled = false;
        }
    }
}