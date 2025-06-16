using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyiAvciTakipKodu : MonoBehaviour
{
    private Ayi ayi;
    
    private void Start()
    {
        ayi = GetComponentInParent<Ayi>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ayi.avciTespitEdildiMi = true;
            ayi.KosmayiAyarla(true);
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.ayiTakipSesi);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ayi.avciTespitEdildiMi = false;
            ayi.KosmayiAyarla(false);
        }
    }
}
