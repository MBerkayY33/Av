using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvciOzellikleri : MonoBehaviour
{
    [SerializeField] private float maxCan;
    [SerializeField] private float beklemeSuresi;
    [SerializeField] private float hasarAlmaSuresi;
    private float mevcutCan;
    private float hasarAlmaZamani;
    private int olmeSayaci;

    public CanBari canBari;
    public bool olduMu;
    private bool hasarAldiMi;

    private OyunYoneticisi oyunYoneticisi;
    private Animator animator;

    private GameObject avci;

    private void Awake()
    {
        oyunYoneticisi = GameObject.Find("OyunYoneticisi").GetComponent<OyunYoneticisi>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        avci = this.gameObject;
        olduMu = false;
        olmeSayaci = 1;
        mevcutCan = maxCan;
        canBari.MaxCaniAyarla(maxCan);
    }

    private void Update()
    {
        animator.SetBool("olduMu", olduMu);
        HasarAlmaKontrolEt();
    }

    private void HasarAlmaKontrolEt()
    {
        if (Time.time >= hasarAlmaZamani + hasarAlmaSuresi)
        {
            hasarAldiMi = false;
            animator.SetBool("hasarAldiMi", hasarAldiMi);
        }
    }

    public void CanAzalt(float deger)//*** HasarAl dan çaðýrýlýr karakterin caný azalýr
    {
        hasarAlmaZamani = Time.time;
        mevcutCan -= deger;
        hasarAldiMi = true;
        animator.SetBool("hasarAldiMi", hasarAldiMi);
        canBari.CaniAyarla(mevcutCan);

        if (mevcutCan <= 0f)//***caný sýfýrdan küçükse öl coroutini baþlar
        {
            olduMu = true;
            StartCoroutine(Ol());
        }
    }

    public IEnumerator Ol()// *** karakterin kodlarý devre dýþý býrakýlýr ve yeniden doð metodu çaðýrýlýr
    {
        if (olduMu && olmeSayaci > 0)
        {
            olmeSayaci--;
            animator.SetTrigger("ol");
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.avciOlmeSesi);
            avci.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            avci.GetComponent<AvciHareketKodu>().enabled = false;
            avci.GetComponent<AvciYakinSaldiri>().enabled = false;
            avci.GetComponent<AtesEtmeKodu>().enabled = false;
            yield return StartCoroutine(oyunYoneticisi.YenidenDog(beklemeSuresi));
        }
    }

    public void CaniSifirla()
    {
        mevcutCan = maxCan;
        olduMu = false;
        canBari.CaniAyarla(mevcutCan);
        olmeSayaci = 1;
    }
}