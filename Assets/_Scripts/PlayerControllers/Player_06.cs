using UnityEngine;
using System.Collections;

public class Player_06 : BasePlayerController {

    protected override void Start()
    {
        base.Start();

    }
	void Update () {
	
	}

    public override void OnPlayerDieEvent()
    {
    }

    public override void ResetPlayer()
    {
        mainPlayer.transform.position = new Vector3(-12, -3, 0);
        mainPlayer.Anim.enabled = true;
        mainPlayer.Rig.isKinematic = true;
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv6_moto, true);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
    }

    protected override void DieuKhienNhanVat()
    {
    }

    public override float PhanTramQuangDuongDaDiDuoc()
    {
        return 0;
    }

    public override void OnFinishLevel()
    {

    }
}
