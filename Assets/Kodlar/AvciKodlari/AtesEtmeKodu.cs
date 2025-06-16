using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtesEtmeKodu : MonoBehaviour
{
    public GameObject mermi;
    public float atisKuvveti;
    public Transform atesEtmeNoktasi;
    private Animator animator;
    public bool atesEdiyorMu;
    private bool atesEtmeyeHazir = true;

    private float mermiBeklemeSuresi = 1.2f;
    private float animasyonSuresi = 0.1f;
    private float SonAtesEtmeZamani;

    private void Start()
    {
        SonAtesEtmeZamani = Time.time;
        animator = GetComponent<Animator>();
        atesEdiyorMu = false;
    }

    private void Update()
    {
        AtesGirdisiniKontrolEt();
    }

    private void AtesGirdisiniKontrolEt()
    {
        if (Input.GetButtonDown("FireBullet"))
        {
            if (atesEtmeyeHazir && Time.time > (SonAtesEtmeZamani + mermiBeklemeSuresi))
            {
                atesEdiyorMu = true;
                animator.SetBool("atesEdiyorMu", atesEdiyorMu);
                Invoke("AtesEt", animasyonSuresi);
                atesEtmeyeHazir = false;
            }
        }
    }

    private void AtesEt()//** yukarýda kontrolü yapýlan c tuþuna basýldýðýnda mermi nesnesi oluþturulur ve karakterin baktýðý yönde hýz uygulanýr
    {
        SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.atesEtmeSesi);
        GameObject yeniMermi = Instantiate(mermi, atesEtmeNoktasi.position, atesEtmeNoktasi.rotation);
        yeniMermi.GetComponent<Rigidbody2D>().velocity = atesEtmeNoktasi.right * atisKuvveti;
        SonAtesEtmeZamani = Time.time;
        atesEdiyorMu = false;
        animator.SetBool("atesEdiyorMu", atesEdiyorMu);
        atesEtmeyeHazir = true;
    }
}