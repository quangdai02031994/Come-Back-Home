using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

/// <summary>
/// 
/// </summary>
public class DongHo : MonoBehaviour
{
    public GameObject thanDongHo;
    public GameObject kimDai;
    public GameObject kimNgan;
    public float timeToRotate;
    public float tomeToRing;
    [Range(1, 10)]
    public int doRung;

    public GameObject tics;

    /// <summary>
    /// Subcribe sự kiện rung chuông xong
    /// </summary>
    public Action onComplete;
    public Vector3 nextPosition;

    // Use this for initialization
    private string sdfsdfsdf = "";
    void Start()
    {
        float t = -360 * 3;
        kimDai.transform.DORotate(new Vector3(0, 0, t), timeToRotate, RotateMode.LocalAxisAdd).OnComplete(LamRung);
        kimNgan.transform.DORotate(new Vector3(0, 0, -90), timeToRotate).SetEase(Ease.Linear);

    }

    /// <summary>
    /// Quay xong thì rung cái đồng hồ
    /// </summary>
    void LamRung()
    {
        thanDongHo.transform.DOPunchRotation(new Vector3(0, 0, 10), tomeToRing, doRung).OnComplete(OnComplete);
        tics.SetActive(true);
        //Làm nhấp nháy cái thanh
        if(this.gameObject.activeSelf)
            StartCoroutine(NhapNhayTics(3));
    }

    IEnumerator NhapNhayTics(float time)
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(0.5f);
            tics.SetActive(tics.activeSelf ? false : true);
            time -= 0.5f;
        }
    }

    /// <summary>
    /// Thực hiện xong thì di chuyển nó sang bên cạnh
    /// </summary>
    public void OnComplete()
    {
        if (onComplete != null)
            onComplete();
        this.transform.DOMove(nextPosition, 3).OnComplete(KetThuc);
        this.transform.DOScale(this.transform.localScale / 2, 3);
    }

    /// <summary>
    /// Kết thúc thì hủy nó đi
    /// </summary>
    void KetThuc()
    {
        this.gameObject.SetActive(false);
    }
}
