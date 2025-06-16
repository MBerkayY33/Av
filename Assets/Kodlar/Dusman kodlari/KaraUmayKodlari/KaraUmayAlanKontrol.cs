using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaraUmayAlanKontrol : MonoBehaviour
{
    private KaraUmay KU;
    private KaraUmayAtesEtme KUAtes;
    private void Start()
    {
        KU = GetComponentInParent<KaraUmay>();
        KUAtes = GetComponentInParent<KaraUmayAtesEtme>();
        KU.avciTespitEdildiMi = true;
        KU.KosmayiAyarla(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KUAtes.atesEtmeyeHazir = false;
            KU.avciTespitEdildiMi = true;
            KU.KosmayiAyarla(true);
            Debug.Log("avci tespit edildi");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {           

            KUAtes.atesEtmeyeHazir = true;
        }
    }
}
