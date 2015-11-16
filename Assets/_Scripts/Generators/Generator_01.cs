using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator_01 : BaseGenerator
{
    #region Các đối tượng cần tham chiếu ở ngoài
    //Đường căn lề để làm chuẩn làn đường của đối tượng
    public Transform duongCanLe;

    #endregion

    public GameObject GhePrefap;
    public GameObject VoiCuuHoaPrefap;
    public GameObject BotDienThoaiPrefap;
    public GameObject FinishObject;

    private List<Vector3> viTriGhe = new List<Vector3>();
    private List<Vector3> viTriVoiCuuHoa = new List<Vector3>();
    private List<Vector3> viTriBotDienThoai = new List<Vector3>();

    private List<GameObject> listGhe = new List<GameObject>();
    private List<GameObject> listVoiCuuHoa = new List<GameObject>();
    private List<GameObject> listBotDienThoai = new List<GameObject>();

    // Use this for initialization
    protected override void Start()
    {
        //Đã sinh trước rồi đây
        for (int i = 0; i < soLuongMoiLoai; i++)
        {
            GameObject a = (GameObject)Instantiate(GhePrefap);
            a.SetActive(false);
            listGhe.Add(a);
            a.transform.parent = rootLevel.transform;

            a = (GameObject)Instantiate(VoiCuuHoaPrefap);
            a.SetActive(false);
            listVoiCuuHoa.Add(a);
            a.transform.parent = rootLevel.transform;

            a = (GameObject)Instantiate(BotDienThoaiPrefap);
            a.SetActive(false);
            listBotDienThoai.Add(a);
            a.transform.parent = rootLevel.transform;
        }

        Debug.Log("Generator_01");
        Reset();
        base.Start();
    }

    public override Vector3? GetNextPositionOfItem(EItemType type)
    {
        Vector3? nextPos = null;
        switch (type)
        {
            case EItemType.LV1_GheXeBuyt:
                if (viTriGhe.Count > 0)
                {
                    nextPos = viTriGhe[0];
                    viTriGhe.RemoveAt(0);
                }
                break;
            case EItemType.LV1_VoiCuuHoa:
                if (viTriVoiCuuHoa.Count > 0)
                {
                    nextPos = viTriVoiCuuHoa[0];
                    viTriVoiCuuHoa.RemoveAt(0);
                }
                break;
            case EItemType.LV1_BotDienThoai:
                if (viTriBotDienThoai.Count > 0)
                {
                    nextPos = viTriBotDienThoai[0];
                    viTriBotDienThoai.RemoveAt(0);
                }
                break;
            default:
                break;
        }
        return nextPos;
    }


    public override void Reset()
    {
        this.gameObject.transform.position = Vector3.zero;

        viTriGhe = new List<Vector3>();
        viTriVoiCuuHoa = new List<Vector3>();
        viTriBotDienThoai = new List<Vector3>();

        float addHeighGhe = GhePrefap.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        float addHeighVoiCuuHoa = VoiCuuHoaPrefap.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        float addHeighBotDienThoai = BotDienThoaiPrefap.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        foreach (Transform item in transform)
        {
            Vector3 p = item.transform.position;
            p.y = duongCanLe.position.y;

            if (item.gameObject.name.StartsWith("ghe", true, null))
            {
                p.y += addHeighGhe;
                viTriGhe.Add(p);
            }
            else if (item.gameObject.name.StartsWith("voi", true, null))
            {
                p.y += addHeighVoiCuuHoa;
                viTriVoiCuuHoa.Add(p);
            }
            else if (item.gameObject.name.StartsWith("bot", true, null))
            {
                p.y += addHeighBotDienThoai;
                viTriBotDienThoai.Add(p);
            }

            if (item.gameObject.name.CompareTo("FINISH") == 0)
            {
                p = FinishObject.transform.position;
                p.x = item.transform.position.x;
                FinishObject.transform.position = p;

                ItemRun x = FinishObject.GetComponent<ItemRun>();
                x.Setup(EItemType.LV1_FINISH, velocity, this);
            }
        }

        viTriGhe.Sort(new SapXepTheoChieuTangCuaX());
        viTriVoiCuuHoa.Sort(new SapXepTheoChieuTangCuaX());
        viTriBotDienThoai.Sort(new SapXepTheoChieuTangCuaX());

        Debug.Log("SL Vị trí ghe: " + viTriGhe.Count);
        Debug.Log("SL vị trí voi cứu hỏa: " + viTriVoiCuuHoa.Count);
        Debug.Log("SL vị trí bot điện thoại: " + viTriBotDienThoai.Count);


        //Xắp xếp vị trí ban đầu
        for (int i = 0; i < soLuongMoiLoai; i++)
        {
            if (i < viTriGhe.Count)
            {
                listGhe[i].transform.position = viTriGhe[i];
                ItemRun a = listGhe[i].GetComponent<ItemRun>();
                a.Setup(EItemType.LV1_GheXeBuyt, velocity, this);
                listGhe[i].gameObject.SetActive(true);
            }

            if (i < viTriVoiCuuHoa.Count)
            {
                listVoiCuuHoa[i].transform.position = viTriVoiCuuHoa[i];
                ItemRun b = listVoiCuuHoa[i].GetComponent<ItemRun>();
                b.Setup(EItemType.LV1_VoiCuuHoa, velocity, this);
                listVoiCuuHoa[i].gameObject.SetActive(true);
            }

            if (i < viTriBotDienThoai.Count)
            {
                listBotDienThoai[i].transform.position = viTriBotDienThoai[i];
                ItemRun c = listBotDienThoai[i].GetComponent<ItemRun>();
                c.Setup(EItemType.LV1_BotDienThoai, velocity, this);
                listBotDienThoai[i].gameObject.SetActive(true);
            }
        }

        viTriGhe.RemoveRange(0, soLuongMoiLoai);
        viTriVoiCuuHoa.RemoveRange(0, soLuongMoiLoai);
        viTriBotDienThoai.RemoveRange(0, soLuongMoiLoai);
        
        Debug.Log("SL Vị trí ghe: " + viTriGhe.Count);
        Debug.Log("SL vị trí voi cứu hỏa: " + viTriVoiCuuHoa.Count);
        Debug.Log("SL vị trí bot điện thoại: " + viTriBotDienThoai.Count);

      
    }
}
