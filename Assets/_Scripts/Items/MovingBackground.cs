using UnityEngine;
using System.Collections;

/**
 * Cái này dùng để quản lý việc lặp background bằng tay, sử dụng cho level 03 để dịch tuyển nền tuyết!
 * Về cơ bản là lặp 2 background, do mỗi background đều chứa thuộc tính vật lý 
 **/
public class MovingBackground : MonoBehaviour
{


    public BaseGameController theCurrentController;
    public float Speed { get { return theCurrentController.speed; } }

    void Update()
    {
        this.transform.Translate(Vector3.left * Time.deltaTime * Speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("OnCollisionEnter2D: " + other.gameObject.name);
    }
}
