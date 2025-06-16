using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaraUmayAtesEtme : MonoBehaviour
{
    public GameObject mermi;
    public float atisKuvveti;
    public Transform atesEtmeNoktasi;
    private Animator animator;
    public bool atesEdiyorMu;
    public bool atesEtmeyeHazir;

    private float mermiBeklemeSuresi = 1.2f;
    private float animasyonSuresi = 0.1f;
    private float SonAtesEtmeZamani;
    private float atesEtmeSayisi = 1;

    private AvciOzellikleri AvciOZ;

    private void Start()
    {
        SonAtesEtmeZamani = Time.time;
        animator = GetComponent<Animator>();
        atesEtmeyeHazir = false;
        atesEdiyorMu = false;
        AvciOZ = GameObject.FindGameObjectWithTag("Player").GetComponent<AvciOzellikleri>();
    }

    private void Update()
    {
        AtesGirdisiniKontrolEt();
    }

    private void AtesGirdisiniKontrolEt()
    {
        if (atesEtmeyeHazir && Time.time > (SonAtesEtmeZamani + mermiBeklemeSuresi) && atesEtmeSayisi > 0 && !AvciOZ.olduMu)
        {
            atesEdiyorMu = true;
            animator.SetBool("atesEdiyorMu", atesEdiyorMu);
            atesEtmeSayisi--;
            Invoke("AtesEt", animasyonSuresi);

        }
    }

    private void AtesEt()
    {
        SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.karaUmayBüyüSesi);
        GameObject yeniMermi = Instantiate(mermi, atesEtmeNoktasi.position, atesEtmeNoktasi.rotation);
        yeniMermi.GetComponent<Rigidbody2D>().velocity = -atesEtmeNoktasi.right * atisKuvveti;
        SonAtesEtmeZamani = Time.time;
        atesEdiyorMu = false;
        animator.SetBool("atesEdiyorMu", atesEdiyorMu);
        atesEtmeSayisi++;
    }
}