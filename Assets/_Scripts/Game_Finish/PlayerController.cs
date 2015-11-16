using UnityEngine;
using System.Collections;
using DG.Tweening;
public class PlayerController : MonoBehaviour {

    public Vector3 endPositon = new Vector3(-2, -3.2f, 0);
    public float timeMove = 2;

    private bool isWait = true;
    private SkeletonAnimation Anim;


	void Start () {
        Anim = GetComponent<SkeletonAnimation>();

        Anim.state.SetAnimation(0, "boywalk", true);
        transform.DOMove(endPositon, timeMove).OnComplete(turnOffAnim);
	}
	
	void Update () {


        if (BackGroundController.Inst.get_waitState() == true && isWait)
        {
            DuaQua();
            BackGroundController.Inst.TurnOnParticle();
            BackGroundController.Inst.Mama_ChangeAnimaton(AnimationNames.lv_finish_mama_getGift, false);
            isWait = false;
        }
	}

    private void DuaQua()
    {
        Anim.enabled = true;
        Anim.state.SetAnimation(0, "boyjump", false);
    }

    private void turnOffAnim()
    {

        //Anim.AnimationName = "";
        Anim.state.SetAnimation(0, AnimationNames.lv2_nga, false);

        Debug.Log(transform.GetComponent<SkeletonAnimation>().AnimationName);
    }



}
