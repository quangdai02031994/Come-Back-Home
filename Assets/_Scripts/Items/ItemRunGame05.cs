using UnityEngine;
using System.Collections;

public class ItemRunGame05 : MonoBehaviour {
    /// <summary>
    /// nếu index =1 thì vật sẽ di chuyển sang bên phải
    /// </summary>
    public int index = 1;

    /// <summary>
    /// Tốc độ di chuyển của vật thể. Dấu + là đi theo chiều dương.
    /// </summary>
    public float speed = 5;

    /// <summary>
    /// điểm kết thúc của vật thể
    /// </summary>
    public float endPosition;

    void Update()
    {
        if (MainController.Instance.IsPlaying)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (index == 1)
            {
                if (transform.localPosition.x > endPosition)
                    Destroy(this.gameObject);
            }
            else
            {
                if (transform.localPosition.x < endPosition)
                    Destroy(this.gameObject);
            }

            if (transform.localPosition.y >= 0)
            {
                transform.GetComponent<Renderer>().sortingOrder = 0;
            }
            else if (transform.localPosition.y == -1.5f)
            {
                transform.GetComponent<Renderer>().sortingOrder = 1;
            }
            else if (transform.localPosition.y <= -3)
            {
                transform.GetComponent<Renderer>().sortingOrder = 2;
            }

           
        }
        else
        {
            if (transform.GetComponent<SkeletonAnimation>() != null)
                transform.GetComponent<SkeletonAnimation>().enabled = false;
        }
    }
}
