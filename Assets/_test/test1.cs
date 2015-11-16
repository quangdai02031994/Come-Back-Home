using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class test1 : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Wake");
    }

    void Start()
    {
        Debug.Log("Start");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
    }


    public void Click()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    void Update()
    {
        // Debug.Log(Time.time);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.PositionHandle(this.transform.position, Quaternion.identity);
    }
#endif
}
