using UnityEngine;
using System.Collections;

public class ActiveWithObject : MonoBehaviour
{
    public GameObject trackObject;
    // Use this for initialization
    void Start()
    {
        this.gameObject.SetActive(trackObject.activeSelf);
    }
}
