using UnityEngine;
using System.Collections;

public class Helper {
    

    /// <summary>
    /// Dịch chuyển lên vị trí phía trên theo trục y
    /// </summary>
    public static void DatLenPhiaTren(Transform target,Transform origin)
    {
        float h = target.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Vector3 newPos = origin.position;
        newPos.y += h;
        target.position = newPos;
    }

    /// <summary>
    /// Dịch chuyển lên vị trí phía trên theo trục y
    /// </summary>
    public static void DatLenPhiaTren(Transform target, Vector3 pos)
    {
        float h = target.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Vector3 newPos = pos;
        newPos.y += h;
        target.position = newPos;
    }
}
