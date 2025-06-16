using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyikHareketKodu : MonoBehaviour
{
    private enum Durumlar
    {
        Yurume,
        GeriItme,
        Olme,
    }
    private Durumlar mevcutDurum;

    private int dusmaninYonu, hasarAlmaYonu;
    private bool zeminTespitEdildiMi, duvarTespitEdildiMi;
    [SerializeField] private float zeminKontrolMesafesi, duvarKontrolMesafesi;
    [SerializeField] private float hareketHizi, maxCan, geriItmeSuresi, olmeSuresi;
    [SerializeField] private float hasarVermeBeklemeSuresi, dokunmaHasari, dokunmaHasarAlaniYuksekligi, dokunmaHasarAlaniGenisligi;
    [SerializeField] private Transform zeminKontrol, duvarKontrol, dokunmaHasariKontrol;
    [SerializeField] private LayerMask zemin, oyuncu;
    [SerializeField] private GameObject dusman;
    [SerializeField] private Vector2 geriItmeHizi;

    private float mevcutCan, geriItmeBaslamaZamani, hasarVermeZamani, olmeZamani;

    private float[] saldiriBilgileri = new float[2];
    private Rigidbody2D dusmanRB;
    private Animator animator;
    private Vector2 hareket, dokunmaHasariAsagiSol, dokunmaHasariYukariSag;

    private void Start()
    {
        mevcutCan = maxCan;
        dusmanRB = dusman.GetComponent<Rigidbody2D>();
        dusmaninYonu = 1;
        animator = dusman.GetComponent<Animator>();
    }
    private void Update()
    {
        switch (mevcutDurum)
        {
            case Durumlar.Yurume:
                YurumeDurumunuGuncelle();
                break;
            case Durumlar.GeriItme:
                GeriItmeDurumunuGuncelle();
                break;
            case Durumlar.Olme:
                OlmeDurumunuGuncelle();
                break;
        }
    }

    //Yürüme durumlarý

    private void YurumeDurumunuGuncelle()
    {
        zeminTespitEdildiMi = Physics2D.Raycast(zeminKontrol.position, Vector2.down, zeminKontrolMesafesi, zemin);
        duvarTespitEdildiMi = Physics2D.Raycast(duvarKontrol.position, transform.right, duvarKontrolMesafesi, zemin);

        DokunmaHasariniKontrolEt();
        if (!zeminTespitEdildiMi || duvarTespitEdildiMi)
        {
            Cevir();
        }
        else
        {
            hareket.Set(hareketHizi * dusmaninYonu, dusmanRB.velocity.y);
            dusmanRB.velocity = hareket;
        }
    }

    //Geri itme durumlarý

    private void GeriItmeDurumunaGir()
    {
        geriItmeBaslamaZamani = Time.time;
        hareket.Set(geriItmeHizi.x * hasarAlmaYonu, geriItmeHizi.y);
        dusmanRB.velocity = hareket;
        animator.SetBool("geriItiyorMu", true);
    }

    private void GeriItmeDurumunuGuncelle()
    {
        if (Time.time >= geriItmeBaslamaZamani + geriItmeSuresi)
        {
            SwitchFonksiyonu(Durumlar.Yurume);
        }
    }

    private void GeriItmeDurumundanCik()
    {
        animator.SetBool("geriItiyorMu", false);
    }

    //Olme durumlarý

    private void OlmeDurumunaGir()
    {
        animator.SetTrigger("ol");
        olmeZamani = Time.time;

    }

    private void OlmeDurumunuGuncelle()
    {
        if (Time.time >= (olmeZamani + olmeSuresi))
        {
            SwitchFonksiyonu(Durumlar.Olme);
        }
    }

    private void OlmeDurumundanCik()
    {
        Destroy(gameObject);
    }

    //Diðer Fonksiyonlar

    private void SwitchFonksiyonu(Durumlar durum)
    {
        switch (mevcutDurum)
        {
            case Durumlar.GeriItme:
                GeriItmeDurumundanCik();
                break;
            case Durumlar.Olme:
                OlmeDurumundanCik();
                break;
        }

        switch (durum)
        {
            case Durumlar.GeriItme:
                GeriItmeDurumunaGir();
                break;
            case Durumlar.Olme:
                OlmeDurumunaGir();
                break;
        }
        mevcutDurum = durum;
    }

    private void DokunmaHasariniKontrolEt()
    {
        if (Time.time >= hasarVermeZamani + hasarVermeBeklemeSuresi)
        {
            dokunmaHasariAsagiSol.Set(dokunmaHasariKontrol.position.x - (dokunmaHasarAlaniGenisligi / 2), dokunmaHasariKontrol.position.y - (dokunmaHasarAlaniYuksekligi / 2));
            dokunmaHasariYukariSag.Set(dokunmaHasariKontrol.position.x + (dokunmaHasarAlaniGenisligi / 2), dokunmaHasariKontrol.position.y + (dokunmaHasarAlaniYuksekligi / 2));

            Collider2D hasarAlan = Physics2D.OverlapArea(dokunmaHasariAsagiSol, dokunmaHasariYukariSag, oyuncu);
            if (hasarAlan != null)
            {
                hasarVermeZamani = Time.time;
                saldiriBilgileri[0] = dokunmaHasari;
                saldiriBilgileri[1] = dusman.transform.position.x;
                hasarAlan.SendMessage("HasarAl", saldiriBilgileri);
            }
        }
    }
    private void Cevir()
    {
        dusmaninYonu *= -1;
        dusman.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public void HasarAl(float[] saldiriDetaylari)
    {
        SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.DusmanHasarAlmaSesi);
        mevcutCan -= saldiriDetaylari[0];
        if (saldiriDetaylari[1] > dusman.transform.position.x)
        {
            hasarAlmaYonu = -1;
        }
        else
        {
            hasarAlmaYonu = 1;
        }

        if (mevcutCan > 0)
        {
            SwitchFonksiyonu(Durumlar.GeriItme);
        }
        else if (mevcutCan <= 0)
        {
            SwitchFonksiyonu(Durumlar.Olme);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(zeminKontrol.position, new Vector2(zeminKontrol.position.x, zeminKontrol.position.y - zeminKontrolMesafesi));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(duvarKontrol.position, new Vector2(duvarKontrol.position.x + duvarKontrolMesafesi, duvarKontrol.position.y));
        Gizmos.color = Color.green;

        Vector2 solAsagi = new Vector2(dokunmaHasariKontrol.position.x - (dokunmaHasarAlaniGenisligi / 2), dokunmaHasariKontrol.position.y - (dokunmaHasarAlaniYuksekligi / 2));
        Vector2 sagAsagi = new Vector2(dokunmaHasariKontrol.position.x + (dokunmaHasarAlaniGenisligi / 2), dokunmaHasariKontrol.position.y - (dokunmaHasarAlaniYuksekligi / 2));
        Vector2 solYukari = new Vector2(dokunmaHasariKontrol.position.x - (dokunmaHasarAlaniGenisligi / 2), dokunmaHasariKontrol.position.y + (dokunmaHasarAlaniYuksekligi / 2));
        Vector2 sagYukari = new Vector2(dokunmaHasariKontrol.position.x + (dokunmaHasarAlaniGenisligi / 2), dokunmaHasariKontrol.position.y + (dokunmaHasarAlaniYuksekligi / 2));
        Gizmos.DrawLine(solAsagi, sagAsagi);
        Gizmos.DrawLine(sagAsagi, sagYukari);
        Gizmos.DrawLine(sagYukari, solYukari);
        Gizmos.DrawLine(solYukari, solAsagi);
    }
}