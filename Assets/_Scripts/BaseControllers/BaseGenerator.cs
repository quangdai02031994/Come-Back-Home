using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseGenerator : MonoBehaviour
{
    #region Các đối tượng cần tham chiếu ở ngoài
    public BaseGameController rootLevel;
    public int soLuongMoiLoai;
    public float velocity;
    #endregion


    protected virtual void Start()
    {
        this.GetComponent<Rigidbody2D>().isKinematic = true;
        if (rootLevel.mainController.isTesting)
            this.GetComponent<Rigidbody2D>().velocity = Vector2.left * velocity;
    }
    public abstract Vector3? GetNextPositionOfItem(EItemType type);

    public abstract void Reset();
}
