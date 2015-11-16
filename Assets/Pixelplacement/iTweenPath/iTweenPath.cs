
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AddComponentMenu("Pixelplacement/iTweenPath")]
public class iTweenPath : MonoBehaviour
{
    public bool pathVisible = true;
    public string tag = "";
    public Color pathColor = Color.cyan;

    public List<Vector3> nodes = new List<Vector3>();



#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (pathVisible)
        {
            if (nodes.Count > 0)
            {
                //iTween.DrawPath(nodes.ToArray(), pathColor);
                for (int i = 0; i < nodes.Count; i++)
                {
                    Gizmos.color = pathColor;
                    Gizmos.DrawWireSphere(nodes[i], 0.4f);
                    Handles.Label(nodes[i], tag + " " + (i).ToString());
                }
            }
        }
    }
#endif

}