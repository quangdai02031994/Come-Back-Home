using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

/*
 * Quản lý các cái chung nhất của các màn
 * 
 **/
public abstract class BaseGameController : MonoBehaviour
{
    public MainController mainController;
    protected float time = 0;

    /// <summary>
    /// Vận tốc dịch chuyển của các vật trong màn đó
    /// </summary>
    public float speed = 1;

    /// <summary>
    /// Backgound này không phải chỉ là 1 cái ảnh
    /// mà nó bao gồm tất cả các khung cảnh trước khi bắt đầu màn chơi đó!
    /// Khi gọi hàm ready các cái này sẽ được dần hiện ra trước!
    /// Thông thường trong này chỉ chứa các object tĩnh thôi!
    /// </summary>
    public GameObject background;



    /// <summary>
    /// Thời gian để cho nó dịch chuyển background sang cảnh này
    /// </summary>
    protected float _timeToReady = 3;


    public virtual void Start() { }


    /// <summary>
    /// Thực hiện các công việc chuẩn bị cho level mới
    /// Thực hiện chuyển dịch dần background vào, chuẩn bị cho màn chơi
    /// </summary>
    public abstract void Ready();

    /// <summary>
    /// Thực hiện chạy Gameplay với game logic của màn đó
    /// </summary>
    public abstract void Play();


    /// <summary>
    /// Hàm này gọi khi nó đi hết chặng đường của level đó.
    /// Thường là sẽ dừng mọi hoạt động lại
    /// Sau đó sẽ gọi đến hàm Go to next scene
    /// </summary>
    public abstract void LevelFinish();

    /// <summary>
    /// Cài đặt mọi thứ level này về như cũ
    /// </summary>
    public abstract void ResetLevel();

}
