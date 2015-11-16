using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MoveTo : MonoBehaviour
{
    public Transform position;
    void Start()
    {
        this.transform.DOMove(position.position, 3);
    }
}
