using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class İdmanKuklasi : MonoBehaviour
{
    private Animator animator;
    private GameObject avci;
    [SerializeField] private GameObject hasarParcacigi;
    [SerializeField] private Vector2 yenidenDogmaKonumu;
    private Rigidbody2D rb;
    private float maxCan;
    private float mevcutCan;
    private float canAzalmaMiktari;
    private float geriItmeBaslangici;
    private float olmeZamani;
    [SerializeField] private float olmeBeklemeSuresi;
    [SerializeField] private float geriItmeHiziX, geriItmeHiziY, geriItmeSuresi;

    private int donmeYonu;

    public bool hasarAliyorMu;

    private bool itmeUygulanıyorMu;
    private bool yasiyorMu;
    private bool oluHalde;

    void Start()
    {
        yasiyorMu = true;
        hasarAliyorMu = false;
        oluHalde = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        avci = GameObject.Find("Avci");
        canAzalmaMiktari = 30f;
        maxCan = 100f;
        mevcutCan = maxCan;
        olmeBeklemeSuresi = 3.55f;
    }

    void Update()
    {
        GeriyeItmeyiKontrolEt();
        HasarAlmaKontrolEt();
        OlmeKontrolEt();
    }

    public void HasarAlmaKontrolEt()
    {
        if (hasarAliyorMu && yasiyorMu)
        {
            HasarAl();
            hasarAliyorMu = false;
        }
    }

    private void HasarAl()//avcınınkine benzer çalışır
    {
        mevcutCan -= canAzalmaMiktari;
        donmeYonu = avci.GetComponent<AvciHareketKodu>().donmeYonu;
        animator.SetTrigger("hasarAl");
        Instantiate(hasarParcacigi, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360f)));
        if (mevcutCan > 0)
        {
            GeriyeIt();
        }
        if (mevcutCan <= 0)
        {
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.olumSesi);
            yasiyorMu = false;
            oluHalde = true;
            olmeZamani = Time.time;
            animator.SetTrigger("ol");
            Debug.Log("Kukla Öldü!!!");
        }
    }

    private void GeriyeIt()
    {
        itmeUygulanıyorMu = true;
        geriItmeBaslangici = Time.time;
        rb.velocity = new Vector2(geriItmeHiziX * donmeYonu, geriItmeHiziY);
    }

    private void GeriyeItmeyiKontrolEt()
    {
        if (Time.time > geriItmeBaslangici + geriItmeSuresi && itmeUygulanıyorMu)
        {
            itmeUygulanıyorMu = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    private void OlmeKontrolEt()//*** öldükten belli süre sonra yeniden doğ çalışır
    {
        if (oluHalde && Time.time > olmeZamani + olmeBeklemeSuresi)
        {
            YenidenDog();
        }
    }

    private void YenidenDog()// kukla belirlenen konumda özellikleri sıfırlanarak yeniden doğar
    {
        gameObject.transform.position = yenidenDogmaKonumu;
        Debug.Log("Kukla doğdu");
        mevcutCan = maxCan;
        yasiyorMu = true;
        oluHalde = false;
        animator.SetBool("yasiyorMu", yasiyorMu);
        animator.ResetTrigger("hasarAl");
        animator.enabled = false;
        animator.enabled = true;
    }
}