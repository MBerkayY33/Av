using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvciHareketKodu : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    public Transform ZeminKontrol;
    public Transform DuvarKontrol;
    public Transform KoseKontrol;
    public LayerMask zemin;

    private float yatayHareketyonu;
    private float kalanKacmaSuresi;
    private float sonKacma = -100f;
    private float ziplamaZamani;
    private float ziplamaZamaniAyarla = 0.15f;
    private float geriItmeBaslamaZamani;
    private float donmeZamani;
    private float donmeZamaniAyarla = 0.1f;
    public float geriItmeSuresi;
    public float harekethizi;
    public float ziplamaKuvveti = 10f;
    public float zeminKontrolYaricapi;
    public float duvarKontrolMesafesi;
    public float kaymaHizi;
    public float havadaHareketKuvveti;
    public float havadaSuzulmeCarpani = 0.95f;
    public float ziplamaYuksekligiDegiskeni = 0.5f;
    public float duvarZiplamaKuvveti;
    public float kacmaHizi;
    public float kacmaZamani;
    public float kacmaBeklemeSuresi;
    public float kosedenZiplamaKuvvetiX;
    public float kosedenZiplamaKuvvetiY;
    public float baslangicYercekimi;
    public Vector2 duvardanZiplamaYonu;
    public Vector2 geriItmeHizi;

    private int kalanziplayabilmeSayisi;
    private int karakterinYonu = 1;
    public int ziplayabilmeSayisi = 1;
    public int donmeYonu;

    private bool sagaDonukMu;
    private bool yuruyorMu;
    private bool hareketEdebilirMi;
    private bool yonDegisebilirMi;
    private bool zemindeMi;
    private bool ziplayabilirMiNormal;
    private bool ziplayabilirMiDuvardan;
    private bool ziplamayaYelteniyorMu;
    private bool ziplamaGirdisiniKontrolEt;
    private bool duvardaMi;
    private bool duvarda2Mi;
    private bool kosedenTirmanabilirMi;
    private bool kayiyorMu;
    private bool kosedeMi;
    private bool geriItiyorMu;
    public bool kaciyorMu;

    void Start()
    {
        //Deðiþkenlerin baþlangýç deðerleri belirlenir
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sagaDonukMu = true;
        hareketEdebilirMi = true;
        yonDegisebilirMi = true;
        kalanziplayabilmeSayisi = ziplayabilmeSayisi;
        duvardanZiplamaYonu.Normalize();
        baslangicYercekimi = rb.gravityScale;
    }

    void Update()
    {
        //Sürekli kontrol edilmesi gereken metodlar çaðýrýlýr
        GirdiyiKontrolEt();
        YonuKontrolEt();
        AnimasyonlariGuncelle();
        ZiplayabilirMiKontrolEt();
        ZiplamaKontrolEt();
        KayiyorMuKontrolEt();
        KosedenTutunmayiKontrolEt();
        KacmayiKontrolEt();
        DonmeYonunuKontrolEt();
        GeriItmeyiKontrolEt();
    }
    void FixedUpdate()
    {
        //Sabit bir zaman aralýðýnda tekrar çalýþmasý istenen metodlar çaðýrýlýr.
        HareketEt();
        EtrafiKontrolEt();
    }

    private void GirdiyiKontrolEt()// *** unity ye atanan tuþlarýn girdilerinin kontrol edildiði metod
    {
        yatayHareketyonu = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (zemindeMi || (kalanziplayabilmeSayisi > 0 && duvarda2Mi))
            {
                SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.ziplamaSesi);
                NormalZiplama();
            }
            else
            {
                ziplamaZamani = ziplamaZamaniAyarla;
                ziplamayaYelteniyorMu = true;
            }
        }
        if (Input.GetButtonDown("Horizontal") && duvarda2Mi)
        {
            if (!zemindeMi && yatayHareketyonu != karakterinYonu)
            {
                hareketEdebilirMi = false;
                yonDegisebilirMi = false;

                donmeZamani = donmeZamaniAyarla;
            }
        }
        if (donmeZamani >= 0)
        {
            donmeZamani -= Time.deltaTime;
            if (donmeZamani <= 0)
            {
                hareketEdebilirMi = true;
                yonDegisebilirMi = true;
            }
        }
        if (ziplamaGirdisiniKontrolEt && !Input.GetButton("Jump"))
        {
            ziplamaGirdisiniKontrolEt = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * ziplamaYuksekligiDegiskeni);
        }
        if (Input.GetButtonDown("Dash"))
        {
            if (Time.time >= (sonKacma + kacmaBeklemeSuresi))
            {
                KacmaHareketi();
            }
        }

    }

    private void HareketEt()//*** düþme, normal hareket ve kayma hareketi
    {
        if (!zemindeMi && !kayiyorMu && yatayHareketyonu == 0 && !geriItiyorMu)
        {
            rb.velocity = new Vector2(rb.velocity.x * havadaSuzulmeCarpani, rb.velocity.y);
        }
        else if (hareketEdebilirMi && !geriItiyorMu)
        {
            rb.velocity = new Vector2(harekethizi * yatayHareketyonu, rb.velocity.y);
        }
        if (kayiyorMu && (rb.velocity.y < -kaymaHizi))
        {
            rb.velocity = new Vector2(rb.velocity.x, -kaymaHizi);
        }
    }
    private void YonuKontrolEt()//*** yön kontrolü ve çevirme
    {
        if (yonDegisebilirMi)
        {
            if (sagaDonukMu && yatayHareketyonu < 0f)
            {
                Cevir();
            }
            else if (!sagaDonukMu && yatayHareketyonu > 0)
            {
                Cevir();
            }
        }
        if (Mathf.Abs(rb.velocity.x) >= 0.001f)
        {
            yuruyorMu = true;
        }
        else
        {
            yuruyorMu = false;
        }
    }
    public void DonmeYonunuKontrolEt()
    {
        if (sagaDonukMu == true)
        {
            donmeYonu = 1;
        }
        else
        {
            donmeYonu = -1;
        }
    }
    private void EtrafiKontrolEt()//*** karakterin zemin ve duvar kontrolleri yapýlýr.gizmosu göster.Unity de göster.
    {
        zemindeMi = Physics2D.OverlapCircle(ZeminKontrol.position, zeminKontrolYaricapi, zemin);
        duvardaMi = Physics2D.Raycast(DuvarKontrol.position, transform.right, duvarKontrolMesafesi, zemin);
        duvarda2Mi = Physics2D.Raycast(KoseKontrol.position, transform.right, duvarKontrolMesafesi, zemin);
    }
    private void ZiplayabilirMiKontrolEt()
    {
        if (zemindeMi && rb.velocity.y <= 0.01f)
        {
            kalanziplayabilmeSayisi = ziplayabilmeSayisi;
        }
        if (duvarda2Mi && duvardaMi)
        {
            ziplayabilirMiDuvardan = true;
        }
        if (kalanziplayabilmeSayisi <= 0)
        {
            ziplayabilirMiNormal = false;
        }
        else
        {
            ziplayabilirMiNormal = true;
        }
    }
    private void KayiyorMuKontrolEt()
    {
        if (duvardaMi && duvarda2Mi && yatayHareketyonu == karakterinYonu && rb.velocity.y < 0 && !kosedenTirmanabilirMi)
        {
            kayiyorMu = true;
        }
        else
        {
            kayiyorMu = false;
        }
    }
    private void KosedenTutunmayiKontrolEt()
    {
        if (duvardaMi && !duvarda2Mi && !kosedeMi)
        {
            kosedeMi = true;
        }
        if (kosedeMi)
        {
            hareketEdebilirMi = false;
            yonDegisebilirMi = false;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
        }
        animator.SetBool("kosedenTirmaniyorMu", kosedeMi);
    }
    public void KosedenTirmanmaAnimasyonSonu()
    {
        rb.velocity = new Vector2(0f, 0f);
        transform.position = new Vector2(transform.position.x + (transform.localScale.x * kosedenZiplamaKuvvetiX * karakterinYonu), transform.position.y + kosedenZiplamaKuvvetiY);
        hareketEdebilirMi = true;
        yonDegisebilirMi = true;
        kosedeMi = false;
        animator.SetBool("kosedenTirmaniyorMu", kosedeMi);
        rb.gravityScale = baslangicYercekimi;
    }
    public void YonDegistirmeyiEtkinlestir()
    {
        yonDegisebilirMi = true;
    }
    public void YonDegistirmeyiDurdur()
    {
        yonDegisebilirMi = false;
    }
    private void Cevir()//*** yön deðiþtirme olayý
    {
        if (!kayiyorMu && yonDegisebilirMi && !geriItiyorMu)
        {
            karakterinYonu *= -1;
            sagaDonukMu = !sagaDonukMu;
            transform.Rotate(0, 180, 0);
        }
    }
    private void AnimasyonlariGuncelle()//*** animasyon parametrelerinin güncellenmesi
    {
        animator.SetBool("yuruyorMu", yuruyorMu);
        animator.SetBool("zemindeMi", zemindeMi);
        animator.SetFloat("yHizi", rb.velocity.y);
        animator.SetBool("kayiyorMu", kayiyorMu);
        animator.SetBool("kaciyorMu", kaciyorMu);
    }
    private void ZiplamaKontrolEt()
    {
        if (ziplamaZamani > 0)
        {
            if (!zemindeMi && duvarda2Mi && yatayHareketyonu != 0 && yatayHareketyonu != karakterinYonu)
            {
                SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.ziplamaSesi);

                DuvardanZiplama();
            }
            else if (zemindeMi)
            {
                SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.ziplamaSesi);
                NormalZiplama();
            }
        }
        if (ziplamayaYelteniyorMu)
        {
            ziplamaZamani -= Time.deltaTime;
        }
    }
    private void NormalZiplama()
    {
        if (ziplayabilirMiNormal)
        {
            rb.velocity = new Vector2(rb.velocity.x, ziplamaKuvveti);
            kalanziplayabilmeSayisi--;
            ziplamaZamani = 0;
            ziplamayaYelteniyorMu = false;
            ziplamaGirdisiniKontrolEt = true;
        }
    }
    private void DuvardanZiplama()
    {
        if (ziplayabilirMiDuvardan)
        {        
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            kayiyorMu = false;
            kalanziplayabilmeSayisi = ziplayabilmeSayisi;
            kalanziplayabilmeSayisi--;
            rb.AddForce(new Vector2(duvarZiplamaKuvveti * duvardanZiplamaYonu.x * yatayHareketyonu, duvarZiplamaKuvveti * duvardanZiplamaYonu.y), ForceMode2D.Impulse);
            ziplamaZamani = 0;
            ziplamayaYelteniyorMu = false;
            ziplamaGirdisiniKontrolEt = true;
            donmeZamani = 0;
            hareketEdebilirMi = true;
            yonDegisebilirMi = true;
        }
    }

    private void KacmaHareketi()
    {
        kaciyorMu = true;
        kalanKacmaSuresi = kacmaZamani;
        sonKacma = Time.time;
    }
    private void KacmayiKontrolEt()
    {
        if (kaciyorMu)
        {
            if (kalanKacmaSuresi > 0)
            {
                hareketEdebilirMi = false;
                yonDegisebilirMi = false;
                rb.velocity = new Vector2(kacmaHizi * karakterinYonu, 0f);
                kalanKacmaSuresi -= Time.deltaTime;
            }
            if (kalanKacmaSuresi <= 0 || duvardaMi)
            {
                kaciyorMu = false;
                hareketEdebilirMi = true;
                yonDegisebilirMi = true;
            }
        }
    }
    public void GeriyeIt(int yon)//*** avcinin HasarAl metodundan çaðýrýlýr. geriye seker.zaman ayarlanýr
    {
        geriItiyorMu = true;
        geriItmeBaslamaZamani = Time.time;
        rb.velocity = new Vector2(geriItmeHizi.x * yon, geriItmeHizi.y);
    }
    private void GeriItmeyiKontrolEt()//***ayarlanan zamanla bekleme süresi toplamý þu anki zamandan küçükse yani süre dolduysa geri itme durdurulur hýz sýdýrlanýr.
    {
        if ((Time.time >= geriItmeBaslamaZamani + geriItmeSuresi) && geriItiyorMu)
        {
            geriItiyorMu = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ZeminKontrol.position, zeminKontrolYaricapi);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(DuvarKontrol.position, new Vector3(DuvarKontrol.position.x + duvarKontrolMesafesi, DuvarKontrol.position.y, DuvarKontrol.position.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(KoseKontrol.position, new Vector3(KoseKontrol.position.x + duvarKontrolMesafesi, KoseKontrol.position.y, KoseKontrol.position.z));
    }
}