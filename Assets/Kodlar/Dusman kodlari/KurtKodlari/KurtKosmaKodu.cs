using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtKosmaKodu : StateMachineBehaviour
{
    public float saldiriMenzili;
    public float hareketHizi;
    private Transform avci;
    private AvciOzellikleri avciOZ;
    private Rigidbody2D kurtRB;
    private Kurt kurt;
    private DusmanCanKodu DCK;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        avci = GameObject.FindGameObjectWithTag("Player").transform;
        avciOZ = GameObject.FindGameObjectWithTag("Player").GetComponent<AvciOzellikleri>();
        kurtRB = animator.GetComponent<Rigidbody2D>();
        kurt = animator.GetComponent<Kurt>();
        DCK = animator.GetComponent<DusmanCanKodu>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (kurt.avciTespitEdildiMi && !avciOZ.olduMu)
        {
            animator.SetBool("kosuyorMu", true);
            if (avci != null && kurtRB != null)
            {
                if (!DCK.geriItiyorMu)
                {
                    kurt.AvciyaBak();
                }

                Vector2 hedef = new Vector2(avci.position.x, kurtRB.position.y);
                Vector2 yeniKonum = Vector2.MoveTowards(kurtRB.position, hedef, hareketHizi * Time.fixedDeltaTime);
                kurtRB.MovePosition(yeniKonum);

                if (Vector2.Distance(avci.position, kurtRB.position) <= saldiriMenzili)
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
