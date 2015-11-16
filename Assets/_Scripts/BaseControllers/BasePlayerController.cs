using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;


/// <summary>
/// Cái này là lớp base của các thành phần controller của thằng player tương ứng tới từng level.
/// (Nó chỉ liên quan đến việc điều khiển level thôi)
/// Bao gồm các thành phần chung nhất.
/// </summary>

public abstract class BasePlayerController : MonoBehaviour
{

    #region Các sự kiện mà controler này cần báo với controller chính
    /// <summary>
    /// Sự kiện khi player cán đích
    /// Không subcribe sự kiện này mà phải gọi vào hàm OnFinishLevel()
    /// </summary>
    public event Action OnFinishLevelEvent;
    public BoxCollider2D theCollider;
    #endregion



    #region Các thuộc tính
    public Transform viTriBanDau;
    public Vector3 scaleBanDau;
    public float thoiGianChuanBi;
    protected int currentStar = 0;

    /// <summary>
    /// Thời gian dùng để tính quãng đường đi
    /// </summary>
    protected float timer = 0;
    #endregion

    protected MainPlayer mainPlayer { get { return MainPlayer.Instance; } }

    public void AnHopQua()
    {
        currentStar++;
    }

    protected virtual void Start()
    {
        //Debug.Log(this.gameObject.name + ": PlayerBaseLevelController.Start");

        this.transform.DOScale(scaleBanDau, thoiGianChuanBi);
        mainPlayer.OnPlayerDieEvent += OnPlayerDieEvent;

        theCollider.gameObject.SetActive(this.gameObject.activeSelf);

        mainPlayer.DoiQuanAo();
    }

    /// <summary>
    /// Hàm này được subcibe vào sự kiên OnPlayerDieEvent của mainPlayer
    /// </summary>
    public abstract void OnPlayerDieEvent();

    public abstract void ResetPlayer();


    /// <summary>
    /// Cái hàm này để Script vật cản nó gọi tới.
    /// Vì chủ yếu là chết sẽ do vật cản.
    /// </summary>

    #region Các sự kiện mà bắt buộc phải override để cho nó clear
    public virtual void OnFinishLevel()
    {
        if (OnFinishLevelEvent != null)
        {
            Debug.Log("BasePlayerController.OnFinishLevel.OnFinishLevelEvent");
            OnFinishLevelEvent();
        }
    }
    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void OnCollisionEnter2D(Collision2D other);
    protected abstract void DieuKhienNhanVat();
    public abstract float PhanTramQuangDuongDaDiDuoc();
    #endregion

    protected virtual void OnEnable()
    {
        if (theCollider != null)
            theCollider.gameObject.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        if (theCollider != null)
            theCollider.gameObject.SetActive(false);
    }
}
