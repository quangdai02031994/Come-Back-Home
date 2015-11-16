using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BackGroundController : MonoBehaviour {


    public GameObject fireworks;
    public GameObject mama;
    public GameObject house_door;

    public Transform endPosition;
    public Transform endCua;

    public float speed = 2;

    private bool isMove = true;
    private bool isWait = false;
    private SkeletonAnimation mama_Anim;

    public static BackGroundController Inst;
	void Start () {

        mama_Anim = mama.GetComponent<SkeletonAnimation>();

        Mama_ChangeAnimaton(AnimationNames.lv_finish_mama_walk, true);
        house_door.transform.DOMove(endCua.position, 1).OnComplete(change_isMove);
        Inst = this;
	}
	
	
	void Update () {

        if (isMove == false)
        {
            mama.transform.Translate(Vector2.left * Time.deltaTime * speed);
            if (mama.transform.localPosition.x < endPosition.localPosition.x)
            {
                change_isMove();
                turnOffAnimations();
                isWait=true;
            }
        }
	}

    void OnDrawGizmos()
    {
        
    }

    public void TurnOnParticle()
    {
        fireworks.gameObject.SetActive(true);
        fireworks.GetComponent<ParticleSystem>().Play();
        fireworks.GetComponent<ParticleSystem>().loop = true;
    }

    public void Mama_ChangeAnimaton(string str, bool loop)
    {
        mama_Anim.enabled = true;
        mama_Anim.state.SetAnimation(0, str, loop);
    }

    private void turnOffAnimations()
    {
        mama_Anim.enabled = false;
    }

    private void change_isMove()
    {
        isMove = !isMove;
    }

    public bool get_waitState()
    {
        return this.isWait;
    }
}
