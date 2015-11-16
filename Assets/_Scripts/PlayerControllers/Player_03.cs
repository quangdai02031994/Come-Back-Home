using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Player_03 : BasePlayerController
{
    private bool _isCanJump = true;
    public float jumpHight = 1;
    public float timeToJump = 0.5f;
    void Update()
    {
        if (MainController.Instance.IsPlaying)
        {
            DieuKhienNhanVat();
        }
    }

    protected override void DieuKhienNhanVat()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
#else
        if(Input.touchCount > 0)
        {
            Jump();
        }
#endif
    }

    private void Jump()
    {
        _isCanJump = false;
        mainPlayer.transform.DOMoveY(mainPlayer.transform.position.y + jumpHight, timeToJump);
        Sequence s = DOTween.Sequence();
        s.Append(mainPlayer.transform.DORotate(new Vector3(0, 0, 30), timeToJump));
        s.Append(mainPlayer.transform.DORotate(Vector3.zero, timeToJump));
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        _isCanJump = true;
    }

    public override void OnPlayerDieEvent()
    {
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv3_nga, false);
    }

    public override void ResetPlayer()
    {
        timer = 0;
        mainPlayer.transform.position = new Vector3(-12, -3.5f, 0);

        mainPlayer.transform.DOMove(viTriBanDau.position, thoiGianChuanBi);

        mainPlayer.Rig.isKinematic = false;
        theCollider.enabled = true;

        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv3_di, true).timeScale = 1.5f;
    }

    public override float PhanTramQuangDuongDaDiDuoc()
    {
        return 0;
    }
}
