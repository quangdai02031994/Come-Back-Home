using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Player_02 : BasePlayerController
{

    #region Các thuộc tính của player tại màn này
    public Transform lanTren;
    public Transform lanDuoi;
    private bool dangOduoi = true;

    public Vector3 scaleNho;
    public Vector3 scaleTo;

    private Renderer renderer;
    #endregion
    private Tweener dichuyenTheoChieuY = null;
    protected override void Start()
    {
        timer = 0;
        renderer = GetComponent<Renderer>();

        base.Start();
        //Đổi skin nhân vật
        mainPlayer.Anim.initialSkinName = Random.Range(2, 5).ToString();
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv2_di, true).timeScale = 3f;
        mainPlayer.transform.DOMoveX(viTriBanDau.position.x, thoiGianChuanBi);
        dichuyenTheoChieuY = mainPlayer.transform.DOMoveY(lanDuoi.position.y, thoiGianChuanBi);
        mainPlayer.transform.DOScale(scaleTo, 0);
        mainPlayer.Rig.isKinematic = true;
    }



    public override void ResetPlayer()
    {
        timer = 0;
        mainPlayer.Rig.isKinematic = true;
        dangOduoi = true;
        mainPlayer.transform.position = new Vector3(-12, -3.5f, 0);
        mainPlayer.transform.DOMoveX(viTriBanDau.position.x, thoiGianChuanBi);
        dichuyenTheoChieuY = mainPlayer.transform.DOMoveY(lanDuoi.position.y, thoiGianChuanBi);
        mainPlayer.transform.DOScale(scaleTo, 0);

        // xCollider.enabled = true;
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv2_di, true).timeScale = 3f;
    }
    public override void OnFinishLevel()
    {
        base.OnFinishLevel();
    }

    public override void OnPlayerDieEvent()
    {
        int k = mainPlayer.transform.DOPause();
        Debug.Log("Số lượng tween được dừng: " + k);
        mainPlayer.Anim.state.SetAnimation(0, AnimationNames.lv2_nga, false);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (this.enabled)
        {
            if (other.gameObject.name.Equals("Layer_A"))
            {
                Debug.Log("Layer_A");
                renderer.sortingOrder = -1;
            }
            else if (other.gameObject.name.Equals("Layer_B"))
            {
                renderer.sortingOrder = 1;
                Debug.Log("Layer_B");
            }
            if (other.gameObject.name.Contains(Tags.FINISH + "_2") && this.enabled)
                OnFinishLevel();
        }
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {

    }

    protected override void DieuKhienNhanVat()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dichuyenTheoChieuY != null)
                dichuyenTheoChieuY.Pause();

            if (dangOduoi)
                DichLenTren();
            else DichXuongDuoi();
        }

        //Cập nhật order
        //     mainPlayer.Anim.r .sortingOrder = -(int)this.transform.position.y;
    }

    private void DichLenTren()
    {
        dangOduoi = false;
        //Dịch về tọa độ y và scale
        mainPlayer.transform.DOMoveY(lanTren.position.y, 1);
        mainPlayer.transform.DOScale(scaleNho, 1);

        Sequence s = DOTween.Sequence();
        s.Append(mainPlayer.transform.DORotate(new Vector3(0, 30, 20), 0.5f));
        s.Append(mainPlayer.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
    }

    private void DichXuongDuoi()
    {
        dangOduoi = true;
        //Dịch về tọa độ y và scale
        mainPlayer.transform.DOMoveY(lanDuoi.position.y, 1);
        mainPlayer.transform.DOScale(scaleTo, 1);

        Sequence s = DOTween.Sequence();
        s.Append(mainPlayer.transform.DORotate(new Vector3(0, -30, -20), 0.5f));
        s.Append(mainPlayer.transform.DORotate(new Vector3(0, 0, 0), 0.5f));
    }

    void Update()
    {
        if (MainController.Instance.IsPlaying)
        {
            timer += Time.deltaTime;
            mainPlayer.Anim.zSpacing = mainPlayer.transform.position.y;
            DieuKhienNhanVat();
        }
    }

    public float toaDoCuoiCung;
    public override float PhanTramQuangDuongDaDiDuoc()
    {
        float tongQuangDuong = toaDoCuoiCung - viTriBanDau.position.x;
        float diDuoc = timer * MainController.Instance.levelControllers[MainController.Instance.CurrentLevel].speed;
        return diDuoc / tongQuangDuong;
    }
}
