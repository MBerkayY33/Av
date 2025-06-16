using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyiSaldiriKodu : MonoBehaviour
{
    public float yakinsaldiriHasari = 20f;
    public Vector3 saldiriKonumAyari;
    public float saldiriMenzili;
    public LayerMask oyuncu;
    private float[] saldiriBilgileri = new float[2];

    private void Start()
    {
        saldiriBilgileri[0] = yakinsaldiriHasari;
    }

    public void Saldir()
    {
        saldiriBilgileri[1] = transform.position.x;
        Vector3 pos = transform.position;
        pos += transform.right * saldiriKonumAyari.x;
        pos += transform.up * saldiriKonumAyari.y;
        Collider2D avci = Physics2D.OverlapCircle(pos, saldiriMenzili, oyuncu);
        if (avci != null)
        {
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.ayiSaldiriSesi);

            avci.GetComponent<AvciYakinSaldiri>().HasarAl(saldiriBilgileri);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * saldiriKonumAyari.x;
        pos += transform.up * saldiriKonumAyari.y;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, saldiriMenzili);
    }
}