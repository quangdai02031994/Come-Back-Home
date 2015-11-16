using UnityEngine;
using System.Collections;

public class ItemRun : MonoBehaviour
{
    protected EItemType type;
    protected float velocity;
    protected BaseGenerator generator;
    protected Rigidbody2D rig;

    //Cài đặt object trước khi sử dụng
    public virtual void Setup(EItemType type, float velocity, BaseGenerator generator)
    {
        timerCounter = 0;
        this.type = type;
        this.velocity = velocity;
        this.generator = generator;
        generator.rootLevel.mainController.mainPlayer.OnPlayerDieEvent += player_OnLevelOver;
        this.gameObject.SetActive(true);
        if (rig != null)
            rig.velocity = Vector2.left * velocity;
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector2.left * velocity;
    }

    void player_OnLevelOver()
    {
        Debug.Log("ItemRun.player_OnLevelOver");
        rig.velocity = Vector2.zero;
    }


    float timerCounter = 0;
    void Update()
    {
        timerCounter += Time.deltaTime;

        if (MainPlayer.Instance.IsAlive == false)
        {
            rig.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Chú ý ở ngoài kia ko được đổi tên GameObject này
        if (other.gameObject.name == "Destroy")
        {
            //Chuyển sang tuyến sau, nếu ko có thì hủy đi
            Vector3? nextPos = generator.GetNextPositionOfItem(type);
            if (nextPos == null)
            {
                Debug.Log(this.gameObject.name + ": không tìm được vị trí tiếp theo");
                this.gameObject.SetActive(false);
            }
            else
            {
                //Dịch chuyển đến vị trí tiếp theo
                Debug.Log(this.gameObject.name+": đang dịch chuyển đến vị trí tiếp theo!");
                this.gameObject.transform.position = (Vector3)nextPos + Vector3.left * velocity * timerCounter;

            }
        }
    }

}
