using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
public class ShowPositionInGizmos : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Handles.Label(this.transform.position, this.transform.position.ToString());
    }
}
#endif