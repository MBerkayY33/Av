using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvciYakinSaldiri : MonoBehaviour
{
    [SerializeField] private bool saldirabilirMi;
    [SerializeField] private float saldiriZamanAraligi, saldiri1Yaricapi, saldiri1Hasari;
    [SerializeField] private Transform saldiriVurusAlaniKonumu;
    [SerializeField] private LayerMask dusman;

    public bool saldiriyorMu;
    private bool girdiAlindiMi, ilkSaldiriMi;
    private float sonGirdiZamani = Mathf.NegativeInfinity;
    private Animator animator;
    private AvciHareketKodu AvciHK;
    private AvciOzellikleri AvciOz;

    private float[] saldiriBilgileri = new float[2];

    private void Start()
    {
        saldiriyorMu = false;
        ilkSaldiriMi = false;
        saldirabilirMi = true;
        animator = GetComponent<Animator>();
        animator.SetBool("saldirabilirMi", saldirabilirMi);
        AvciHK = GetComponent<AvciHareketKodu>();
        AvciOz = GetComponent<AvciOzellikleri>();
    }

    private void Update()
    {
        SaldiriGirdisiniKontolEt();
        SaldirilariKontrolEt();
    }

    private void SaldiriGirdisiniKontolEt()
    {
        if (Input.GetButtonDown("Saldiri") && saldirabilirMi)
        {
            girdiAlindiMi = true;
        }
    }
    private void SaldirilariKontrolEt()
    {
        if (girdiAlindiMi && saldiriyorMu == false)
        {
            sonGirdiZamani = Time.time;
            girdiAlindiMi = false;
            saldiriyorMu = true;
            ilkSaldiriMi = !ilkSaldiriMi;

            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.yakinVurusSesi);

            animator.SetBool("saldiri1", true);
        }
        animator.SetBool("ilkSaldiriMi", ilkSaldiriMi);
        animator.SetBool("saldiriyorMu", saldiriyorMu);
        if (Time.time >= sonGirdiZamani + saldiriZamanAraligi)
        {
            girdiAlindiMi = false;
            saldiriyorMu = false;
        }

    }
    private void SaldiriVurusAlaniniKontrolEt()//***karakterin animasyon içerisinde vurmasý gerçekleþtiði anda tetiklernir
    {
        Collider2D[] tespitEdilenNesneler = Physics2D.OverlapCircleAll(saldiriVurusAlaniKonumu.position, saldiri1Yaricapi, dusman);//belirtilen konumdaki alana giren düþmanlar tespit edilir
        saldiriBilgileri[0] = saldiri1Hasari;
        saldiriBilgileri[1] = transform.position.x;

        foreach (Collider2D collider in tespitEdilenNesneler)//her bir düþmana hasar verilir
        {
            if (collider.gameObject.name == "idmanKuklasi")
            {
                collider.GetComponent<ÝdmanKuklasi>().hasarAliyorMu = true;
            }
            else
            {
                collider.transform.SendMessage("HasarAl", saldiriBilgileri);
            }

        }
    }
    private void IlkSaldiriyiBitir()
    {
        saldiriyorMu = false;
        animator.SetBool("saldiriyorMu", saldiriyorMu);
        animator.SetBool("saldiri1", false);
    }
    public void HasarAl(float[] saldiriBilgileri)//*** avci hasar aldiginda çalýþýr
    {

        if (!AvciHK.kaciyorMu)
        {
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.hasarAlmaSesi);

            int yon;

            AvciOz.CanAzalt(saldiriBilgileri[0]);//***can azaltý tetikler

            if (saldiriBilgileri[1] < transform.position.x)//hangi yöne itileceði ayarlanýr
            {
                yon = 1;
            }
            else
            {
                yon = -1;
            }

            AvciHK.GeriyeIt(yon);//***geriye itilmeyi tetikler
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(saldiriVurusAlaniKonumu.position, saldiri1Yaricapi);
    }
}