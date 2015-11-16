using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class Player_05 : BasePlayerController
{
    public GameObject player_box_05;

    private float _rayDistance = 2;
    private float _rayOrigin_x = -1f;
    private float _rayOrigin_y = 0.33f;
    private bool _moveUp = false;
    private float _timePlay = 0;

    private RaycastHit2D hit;
    private Ray2D ray;

    
    private float _timeMove = 1;
    private float _totalTime = 42;


    protected override void Start()
    {
        base.Start();
        ResetPlayer();


    }

    void Update()
    {
        if (MainPlayer.Instance.IsAlive)
            _timePlay += Time.deltaTime;
        ChangeSortingLayer();
        DieuKhienNhanVat();
    }

    public override void OnPlayerDieEvent()
    {
        if (MainPlayer.Instance.IsAlive)
        {
            mainPlayer.transform.DOPause();
            player_box_05.GetComponent<SkeletonAnimation>().enabled = false;
            MainPlayer.Instance.PlayerDie();
        }
    }

    public override float PhanTramQuangDuongDaDiDuoc()
    {
        return _timePlay / _totalTime;
    }

    public override void ResetPlayer()
    {

        mainPlayer.Anim.enabled = false;
        mainPlayer.GetComponent<MeshRenderer>().enabled = false;
        player_box_05.GetComponent<SkeletonAnimation>().enabled = true;
        mainPlayer.Rig.isKinematic = true;
        theCollider.enabled = true;
        //mainPlayer.transform.position = new Vector3(-12, -3.5f, 0);
        //mainPlayer.transform.DOMove(viTriBanDau.position, thoiGianChuanBi);
        _moveUp = false;
        _timePlay = 0;
    }

    protected override void DieuKhienNhanVat()
    {
        if (MainPlayer.Instance.IsAlive)
        {

            if (Input.GetMouseButtonDown(0))
                _moveUp = !_moveUp;
            MovePlayer();
            _RayPlayer();
        }

    }


    public void _RayPlayer()
    {
        ray = new Ray2D(new Vector2(transform.position.x - _rayOrigin_x, transform.position.y + _rayOrigin_y), Vector2.left * _rayDistance);

        hit = Physics2D.Raycast(ray.origin, ray.direction, _rayDistance);

        Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);

        if (hit != null && hit.collider != null)
        {
            if (hit.collider.gameObject.tag == Tags.VatCan)
            {
                Debug.Log(hit.collider.gameObject.name);
                OnPlayerDieEvent();
            }

        }

    }

    void OnDrawGizmos()
    {
        _RayPlayer();
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == Tags.FINISH)
        {
            OnFinishLevel();
        }
    }

    private void MovePlayer()
    {
        if (_moveUp)
        {
            mainPlayer.transform.DOMoveY(0, _timeMove);
            mainPlayer.transform.DOScale(new Vector2(0.003f, 0.003f), _timeMove);
        }
        else
        {
            mainPlayer.transform.DOMoveY(-3, _timeMove);
            mainPlayer.transform.DOScale(new Vector2(0.0037f, 0.0037f), _timeMove);
        }
    }

    private void ChangeSortingLayer()
    {
        if (mainPlayer.transform.localPosition.y > -1.5f && mainPlayer.transform.localPosition.y <= 0)
        {
            player_box_05.GetComponent<Renderer>().sortingOrder = 1;
        }
        else if (mainPlayer.transform.localPosition.y > -2.8f && mainPlayer.transform.localPosition.y <= -1.5f)
        {
            player_box_05.GetComponent<Renderer>().sortingOrder = 2;
        }
        else if (mainPlayer.transform.localPosition.y < -2.8f)
        {
            player_box_05.GetComponent<Renderer>().sortingOrder = 3;
        }
    }

    public override void OnFinishLevel()
    {
        Debug.Log(_timePlay);
        base.OnFinishLevel();
    }
}
