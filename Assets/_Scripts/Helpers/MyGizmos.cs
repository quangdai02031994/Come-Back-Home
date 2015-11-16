using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GizmosType
{
    LINE,
    CameraArea,
    BOX_COLLIDER
}

public class MyGizmos : MonoBehaviour
{
    public GizmosType type;
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        switch (type)
        {
            case GizmosType.LINE:
                Vector3 p = this.transform.position;
                Vector3 dau = new Vector3(-500, p.y, p.z);
                Vector3 cuoi = new Vector3(500, p.y, p.z);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(dau, cuoi);
                break;
            case GizmosType.CameraArea:
                Gizmos.color = new Color32(0, 255, 0, 50);
                Gizmos.DrawCube(this.transform.position, new Vector3(18, 10));
                break;
            case GizmosType.BOX_COLLIDER:
                List<BoxCollider2D> list = new List<BoxCollider2D>();
                var a = GetComponents<BoxCollider2D>();
                var b = GetComponentsInChildren<BoxCollider2D>();
                if (a != null && a.Length > 0)
                    list.AddRange(a);
                if (b != null && b.Length > 0)
                    list.AddRange(b);

                foreach (var box in list)
                {
                    Gizmos.color = new Color32(0, 255, 0, 50);
                    Gizmos.DrawCube(this.transform.position, new Vector3(18, 10));

                }
                break;
            default:
                break;
        }
    }
#endif
}