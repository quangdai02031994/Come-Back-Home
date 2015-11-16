using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

/**
 * Chú ý các hành động đều nên switch sang từng level tương tứng để xử lý
 * Chức năng chính của nó là điều khiển script controller con 
 * và các sự kiện tương tác với main controler 
 **/
public class MainPlayer : MonoBehaviour
{
    #region các GameObject cần lấy từ ở ngoài Editor
    public MainController mainController;
    private static MainPlayer _instance;
    public static MainPlayer Instance { get { return _instance; } }

    #endregion

    #region Các events mà ở ngoài muốn bắt
    //Sự kiện người chới chết
    public event Action OnPlayerDieEvent;
    #endregion

    #region Các thuộc tính của nhân vật
    private bool _isAlive = true;
    public bool IsAlive { get { return _isAlive; } }

    private Rigidbody2D _rig;
    public Rigidbody2D Rig { get { return _rig; } }

    private SkeletonAnimation _anim;
    public SkeletonAnimation Anim { get { return _anim; } }



    /// <summary>
    /// Điểm ăn được trong lượt chơi lần này
    /// </summary>
    private int currentStar = 0;
    public int CurrentStar { get { return currentStar; } }

    #endregion

    private List<BasePlayerController> playerControllers = new List<BasePlayerController>();
    public BasePlayerController CurrentLevelController { get { return playerControllers[mainController.CurrentLevel]; } }

    void Awake()
    {

        _instance = this;

        _rig = GetComponent<Rigidbody2D>();
        _anim = GetComponent<SkeletonAnimation>();

        playerControllers.Add(GetComponent<Player_01>());
        playerControllers.Add(GetComponent<Player_02>());
        playerControllers.Add(GetComponent<Player_03>());
        playerControllers.Add(GetComponent<Player_04>());
        playerControllers.Add(GetComponent<Player_05>());

        foreach (var controller in playerControllers)
        {
            controller.OnFinishLevelEvent += OnLevelFinishEvent;
        }
    }

    void Start()
    {

    }


    bool check = true;
    void OnLevelFinishEvent()
    {
        if (check == true)
        {
            check = false;
            //Khi level kết thúc thực hiện thay PlayerController khác!
            Debug.Log("MainPlayer.controller_OnLevelFinishEvent");
            ActivePlayerControllerAtLevel(mainController.CurrentLevel + 1);
            mainController.OnLeveFinish();
            StartCoroutine(f());
        }
    }

    IEnumerator f()
    {
        yield return new WaitForEndOfFrame();
        check = true;
    }

    /// <summary>
    /// Bật player controller tương ứng với level được chọn
    /// </summary>
    /// <param name="lv"></param>
    public void ActivePlayerControllerAtLevel(int lv)
    {
        _isAlive = true;
        //Debug.Log("MainPlayer.ActiveLevel: " + lv);
        for (int i = 0; i < playerControllers.Count; i++)
        {
            if (i == lv)
            {
                //Debug.Log("Active level: " + i);
                playerControllers[i].enabled = true;
                playerControllers[i].ResetPlayer();
            }
            else
            {
                playerControllers[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// Cái này được gọi khi player fire event GameOver
    /// </summary>
    public void PlayerDie()
    {
        _isAlive = false;
        //Debug.Log("MainPlayer.PlayerDie");
        OnPlayerDieEvent();
    }


    public void AnHopQua()
    {
        currentStar++;
        playerControllers[mainController.CurrentLevel].AnHopQua();
    }

    public void DoiQuanAo()
    {
        //Debug.Log("DoiQuanAo");
        Anim.initialSkinName = UnityEngine.Random.Range(2, 5).ToString();
        //Debug.Log("Skin hiện tại: " + Anim.initialSkinName);
    }
}
