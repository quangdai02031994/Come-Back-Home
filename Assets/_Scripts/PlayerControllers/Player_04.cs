using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Player_04 : BasePlayerController
{
    private bool _isCanJump = true;
    public float jumpHight = 3f;
    public float timeToJump = 0.5f;
    public float DiemCuoi;


    private float currTime;

    private float timeMove;
    private float quangduong;
    protected override void Start()
    {
        base.Start();
        ResetPlayer();
     
    }

    public override float PhanTramQuangDuongDaDiDuoc()
    {
        
        return quangduong / DiemCuoi;
    }

    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > 5)
        {
            MainController.Instance.CurrentGameController.speed += 1;
            currTime = 0;
        }

        //MainController.Instance.CurrentGameController.speed += Time.deltaTime * 0.15f;

        if (MainController.Instance.IsPlaying)
        {
            DieuKhienNhanVat();
            timeMove += Time.deltaTime;
            quangduong = timeMove * MainController.Instance.CurrentGameController.speed;
        }

        
    }

    public override void ResetPlayer()
    {
        mainPlayer.transform.position = new Vector3(-10, -4, 0);
        mainPlayer.transform.DOMove(viTriBanDau.position, thoiGianChuanBi);
        mainPlayer.Rig.isKinematic = false;
        theCollider.enabled = true;
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv4_di, true).timeScale = 1.5f;
        MainController.Instance.CurrentGameController.speed = 5;
        quangduong = 0;
        timeMove = 0;
    }

    public override void OnPlayerDieEvent()
    {
        MainPlayer.Instance.Anim.state.SetAnimation(0, AnimationNames.lv4_nga, false);
        mainPlayer.transform.DOPause();
        mainPlayer.Rig.velocity = Vector2.zero;
        theCollider.enabled = false;
        mainPlayer.Rig.isKinematic = true;
        mainPlayer.transform.position = viTriBanDau.position;
    }

    protected override void DieuKhienNhanVat()
    {
        if(Input.GetMouseButtonDown(0) && _isCanJump)
        {
            _isCanJump = false;
            mainPlayer.transform.DOMoveY(mainPlayer.transform.position.y + jumpHight, timeToJump);
            mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv4_nhay, false).TimeScale = 1.5f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (this.enabled)
        {
            _isCanJump = true;

            if (this.enabled && mainPlayer.IsAlive)
            {
                mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv4_di, true);
            }

            if (other.gameObject.tag == Tags.VatCan)
            {
                if (MainPlayer.Instance.IsAlive)
                {
                    MainPlayer.Instance.PlayerDie();
                }
            }
           

        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
       
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (this.enabled)
        {
            if (other.gameObject.tag == Tags.FINISH && this.enabled)
            {
                OnFinishLevel();
                Debug.Log(other.gameObject.name);
            }
        }
    }


    public override void OnFinishLevel()
    {
        Debug.Log(quangduong);
        mainPlayer.Rig.isKinematic = true;
        mainPlayer.Rig.velocity = Vector2.zero;
        base.OnFinishLevel();
    }
}
