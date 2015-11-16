using UnityEngine;
using System.Collections;
using DG.Tweening;


/// <summary>
/// Màn nhảy nhảy
/// </summary>
public class Player_01 : BasePlayerController
{
    [SerializeField]
    private float timeToJump = 0.4f;
    public float jumpHight = 2;
    private bool _isCanJump = true;

    protected override void Start()
    {
        timer = 0;
        Debug.Log("Player_01.Start");
        base.Start();
        ResetPlayer();
    }

    void Update()
    {
        DieuKhienNhanVat();

        if (MainController.Instance.IsPlaying)
        {
            timer += Time.deltaTime;
        }
    }


    #region Trạng thái trò chơi và tương tác với nhân vật
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (this.enabled)
        {
            Debug.Log("Player_01.OnCollisionEnter2D");

            if (this.enabled && mainPlayer.IsAlive)
            {
                mainPlayer.Anim.state.SetAnimation(0, AnimationNames.NhayNhay_di, true).timeScale = 2f;
            }

            if (mainPlayer.IsAlive == false && other.gameObject.name == Tags.BASE)
            {
                mainPlayer.Rig.velocity = Vector2.zero;
                theCollider.enabled = false;
                mainPlayer.Rig.isKinematic = true;
            }
            _isCanJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (this.enabled)
        {
            _isCanJump = false;
            Debug.Log(other.gameObject.name);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (this.enabled)
        {
            if (other.gameObject.name.Contains(Tags.FINISH + "_1") && this.enabled)
                OnFinishLevel();
        }
    }

    public override void OnPlayerDieEvent()
    {
        Debug.Log("Phần trăm quãng đường đi được: " + PhanTramQuangDuongDaDiDuoc());
        Debug.Log("Player_01.mainPlayer_OnPlayerDieEvent");
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.NhayNhay_nga, false);
        Vector3 p = mainPlayer.transform.position;
        p.y = viTriBanDau.position.y;
        mainPlayer.transform.position = p;
    }

    public override void OnFinishLevel()
    {
        Debug.Log("OnFinishLevel - Phần trăm quãng đường đi được: " + PhanTramQuangDuongDaDiDuoc());
        base.OnFinishLevel();
        SaveManager.SaveStarLevel(1, currentStar);
    }

    #endregion




    #region Điều khiển nhân vật
    public override void ResetPlayer()
    {
        timer = 0;
        mainPlayer.transform.position = new Vector3(-12, -3.5f, 0);
        mainPlayer.transform.DOMoveX(viTriBanDau.position.x, thoiGianChuanBi);
        mainPlayer.Rig.isKinematic = false;
        theCollider.enabled = true;
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.NhayNhay_di, true).timeScale = 1.5f;
    }


    void Jump()
    {
        _isCanJump = false;
        mainPlayer.transform.DOMoveY(mainPlayer.transform.position.y + jumpHight, timeToJump);
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.NhayNhay_nhay, false).timeScale = 1.1f;
    }

    protected override void DieuKhienNhanVat()
    {
        if (mainPlayer.IsAlive)
        {
            if (Input.GetMouseButtonDown(0) && _isCanJump && mainPlayer.IsAlive)
            {
                Jump();
            }
        }
    }
    #endregion


    public float toaDoCuoiCung;
    public override float PhanTramQuangDuongDaDiDuoc()
    {
        float tongQuangDuong = toaDoCuoiCung - viTriBanDau.position.x;
        float diDuoc = timer * MainController.Instance.levelControllers[MainController.Instance.CurrentLevel].speed;
        return diDuoc / tongQuangDuong;
    }
}
