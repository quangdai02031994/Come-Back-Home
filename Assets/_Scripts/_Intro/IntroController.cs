using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

/**
 * Thực hiện quản lý toàn bộ màn hình intro
 * Đông hồ -> Đi bộ -> Menu
 * */
public class IntroController : MonoBehaviour
{

    public GameObject ui_DongHoPanel;
    public GameObject ui_MenuPanel;
    public GameObject walking;
    public GameObject nhanVat;

    public DongHo dongHo;

    public Transform p_Truoc;
    public Transform p_Sau;
    public Transform p_DiemDen;

    private static bool isTheFirstTime = true;

    void Start()
    {
        //Cài đặt màn hình

        if (isTheFirstTime)
        {
            ui_DongHoPanel.SetActive(true);
            ui_MenuPanel.SetActive(false);
            walking.SetActive(false);
            nhanVat.SetActive(false);

            ui_MenuPanel.transform.DOMoveX(p_Truoc.position.x, 0);
            walking.transform.DOMoveX(p_Truoc.position.x, 0);

            dongHo.onComplete += WakingScene;
        }
        else
        {
            ui_MenuPanel.SetActive(true);
        }

        isTheFirstTime = false;
    }


    void WakingScene()
    {
        dongHo.onComplete -= WakingScene;
        Debug.Log("WakingScene");
        walking.SetActive(true);
        walking.transform.DOMoveX(0, 2).OnComplete(NguoiDi);
    }

    void NguoiDi()
    {
        Debug.Log("Người đi");
        ui_DongHoPanel.SetActive(false);
        nhanVat.SetActive(true);
        nhanVat.transform.DOMoveX(p_DiemDen.position.x, 5).SetEase(Ease.Linear).OnComplete(ShowMenu);
    }

    public void ShowMenu()
    {
        nhanVat.SetActive(false);
        Debug.Log("ShowMenu");
        walking.transform.DOMoveX(p_Sau.position.x, 2).OnComplete(() => { walking.SetActive(false); });
        ui_MenuPanel.SetActive(true);
        ui_MenuPanel.transform.DOMoveX(0, 2);
    }

    public void Click_Play()
    {
        Application.LoadLevel(SceneName.InGame);
    }

    public void Click_Skip_DongHo()
    {
        if (dongHo != null)
            dongHo.OnComplete();
    }

    public void Click_Skip_Walking()
    {
        nhanVat.transform.DOPause();
        ShowMenu();
    }

    public void Click_Map()
    {
        Application.LoadLevel(SceneName.Map);
    }
}
