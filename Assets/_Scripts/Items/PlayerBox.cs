using UnityEngine;
using System.Collections;


/// <summary>
/// Dịch chuyển box collder theo vị trí của 1 bộ phận!
/// Chú ý chỉ bám theo trục y
/// </summary>
public class PlayerBox : MonoBehaviour {

    public GameObject trackObject;
    private Vector3 offset;
    
    void OnEnable()
    {
        offset = this.transform.position - trackObject.transform.position;
    }

    void Update()
    {
        this.transform.position = trackObject.transform.position + offset;
    }
}
