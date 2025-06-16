using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyiKosmaKodu : StateMachineBehaviour
{
    public float saldiriMenzili;
    public float hareketHizi;
    private Transform avci;
    private AvciOzellikleri avciOZ;
    private Rigidbody2D ayiRB;
    private Ayi ayi;
    private DusmanCanKodu DCK;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        avci = GameObject.FindGameObjectWithTag("Player").transform;
        avciOZ = GameObject.FindGameObjectWithTag("Player").GetComponent<AvciOzellikleri>();
        ayiRB = animator.GetComponent<Rigidbody2D>();
        ayi = animator.GetComponent<Ayi>();
        DCK = animator.GetComponent<DusmanCanKodu>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ayi.avciTespitEdildiMi && !avciOZ.olduMu)
        {
            if (!DCK.geriItiyorMu)
            {
                ayi.AvciyaBak();
            }

            Vector2 hedef = new Vector2(avci.position.x, ayiRB.position.y);
            Vector2 yeniKonum = Vector2.MoveTowards(ayiRB.position, hedef, hareketHizi * Time.fixedDeltaTime);
            ayiRB.MovePosition(yeniKonum);


            if (Vector2.Distance(avci.position, ayiRB.position) <= saldiriMenzili)
            {
                animator.SetTrigger("saldir");
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
