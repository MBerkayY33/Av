using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtAvciTespitKodu : MonoBehaviour
{
    private Kurt kurt;
    private bool avciAlandaMi;
    private void Start()
    {
        kurt = GetComponentInParent<Kurt>();
        avciAlandaMi = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && avciAlandaMi)
        {
            kurt.avciTespitEdildiMi = true;
            kurt.KosmayiAyarla(true);
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.kurtTakipSesi);
            avciAlandaMi = false;
        }
    }

}
