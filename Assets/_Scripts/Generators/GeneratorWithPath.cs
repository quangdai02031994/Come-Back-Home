using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GeneratorWithPath : MonoBehaviour
{
    public BaseGameController theController;

    /// <summary>
    /// số lượng sinh ra lớn nhất trên màn hình
    /// </summary>
    public int soLuongkhoiTao;
    /// <summary>
    /// Đối tượng áp dụng pooling
    /// </summary>
    public GameObject prefap;

    /// <summary>
    /// Vận tốc dịch chuyển của các vật
    /// </summary>
    public float speed { get { return theController.speed; } }

    /// <summary>
    /// Thời gian để tính lại quãng đường dịch chuyển
    /// </summary>
    private float time = 0;
    private Vector3 quanDuongDaDiDuoc = Vector3.zero;

    private List<GameObject> list;
    private List<bool> isAvaiable;
    private List<Vector3> positions;

    void Start()
    {
        list = new List<GameObject>();
        for (int i = 0; i < soLuongkhoiTao; i++)
        {
            GameObject o = (GameObject)Instantiate(prefap);
            o.transform.parent = this.transform;
            o.GetComponent<ItemRunWithPath>().Setup(this);
            o.SetActive(false);
            list.Add(o);
        }


        positions = GetComponent<iTweenPath>().nodes;
        positions.Sort(new SapXepTheoChieuTangCuaX());
        isAvaiable = new List<bool>();
        for (int i = 0; i < positions.Count; i++)
            isAvaiable.Add(true);
        Reset();


    }

    void Update()
    {
        if (MainController.Instance.IsPlaying)
        {
            time += Time.deltaTime;
            //Tính quãng đường đã được dịch chuyển trong thời gian hiện tại
            quanDuongDaDiDuoc += (Time.deltaTime * Vector3.left * speed);
        }
    }


    public Vector3? GetNextPosition()
    {
        for (int i = 0; i < isAvaiable.Count; i++)
        {
            if (isAvaiable[i])
            {
                Vector3? nextPos = null;
                nextPos = positions[i] + quanDuongDaDiDuoc;
                isAvaiable[i] = false;
                return nextPos;
            }
        }
        return null;
    }

    public void Reset()
    {
        time = 0;
        quanDuongDaDiDuoc = Vector3.zero;
        //Làm cho tất cả mọi vị trí đều free
        for (int i = 0; i < positions.Count; i++)
        {
            isAvaiable[i] = true;
        }

        //Gán các vị trí đầu tiên cho nó
        for (int i = 0; i < soLuongkhoiTao; i++)
        {
            if (i < positions.Count)
            {
                //list[i].transform.position = positions[i];
                Helper.DatLenPhiaTren(list[i].transform, positions[i]);
                
                list[i].SetActive(true);
                isAvaiable[i] = false;
            }
        }
    }
}
