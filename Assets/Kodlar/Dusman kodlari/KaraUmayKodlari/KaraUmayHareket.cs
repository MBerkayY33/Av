using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaraUmayHareket : StateMachineBehaviour
{
    public float saldiriMenzili = 3f;
    public float hareketHizi = 2.5f;
    private Transform avci;
    private AvciOzellikleri avciOZ;
    private Rigidbody2D karaUmayRB;
    private KaraUmay KU;
    private KaraUmayCanKodu DCK;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        avci = GameObject.FindGameObjectWithTag("Player").transform;
        avciOZ = GameObject.FindGameObjectWithTag("Player").GetComponent<AvciOzellikleri>();
        karaUmayRB = animator.GetComponent<Rigidbody2D>();
        KU = animator.GetComponent<KaraUmay>();
        DCK =animator.GetComponent<KaraUmayCanKodu>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (KU.avciTespitEdildiMi && !avciOZ.olduMu)
        {
            animator.SetBool("kosuyorMu", true);
            if (avci != null && karaUmayRB != null)
            {
                if (!DCK.geriItiyorMu)
                {
                    KU.AvciyaBak();
                }

                Vector2 hedef = new Vector2(avci.position.x, karaUmayRB.position.y);
                Vector2 yeniKonum = Vector2.MoveTowards(karaUmayRB.position, hedef, hareketHizi * Time.fixedDeltaTime);
                karaUmayRB.MovePosition(yeniKonum);

                if (Vector2.Distance(avci.position, karaUmayRB.position) <= saldiriMenzili)
                {
                    animator.SetTrigger("saldir");
                }
            }
        }
        else
        {
            animator.SetBool("kosuyorMu", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("saldir");
    }

}
