using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanCanKodu : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator animator;

    private int hasarAlmaYonu;

    public float mevcutCan;
    private float geriItmeBaslamaZamani;
    [SerializeField] private float geriItmeSuresi, olmeSuresi;
    [SerializeField] Vector2 geriItmeHizi;
    public bool olduMu;
    public bool geriItiyorMu { get; private set; }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GeriItmeyiKontrolEt();
    }

    public void HasarAl(float[] saldiriDetaylari)
    {
        SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.DusmanHasarAlmaSesi);
        mevcutCan -= saldiriDetaylari[0];
        if (saldiriDetaylari[1] > transform.position.x)
        {
            hasarAlmaYonu = -1;
        }
        else
        {
            hasarAlmaYonu = 1;
        }

        if (mevcutCan > 0)
        {
            GeriyeIt();
        }
        else if (mevcutCan <= 0 && !olduMu)
        {
            SesYoneticisi.sesYoneticisi.SesEfektiOynat(SesYoneticisi.sesYoneticisi.olumSesi);
            Ol();
        }
    }

    private void Ol()
    {
        olduMu = true;
        animator.SetTrigger("ol");
        rb.velocity = Vector2.zero;
        Destroy(gameObject, olmeSuresi);
    }

    private void GeriyeIt()
    {
        geriItiyorMu = true;
        geriItmeBaslamaZamani = Time.time;
        rb.velocity = new Vector2(geriItmeHizi.x * hasarAlmaYonu, geriItmeHizi.y);
    }

    private void GeriItmeyiKontrolEt()
    {
        if ((Time.time >= geriItmeBaslamaZamani + geriItmeSuresi) && geriItiyorMu)
        {
            geriItiyorMu = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        animator.SetBool("geriItiyorMu", geriItiyorMu);
    }
}