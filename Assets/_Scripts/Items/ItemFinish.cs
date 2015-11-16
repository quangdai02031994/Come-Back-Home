using UnityEngine;
using System.Collections;

public class ItemFinish : MonoBehaviour
{

    public BaseGameController theController;
    private Rigidbody2D Rig;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = this.transform.position;
        Rig = GetComponent<Rigidbody2D>();
    }

    void OnDisable()
    {
        this.transform.position = originalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainController.Instance.IsPlaying)
        {
            Rig.velocity = theController.speed * Vector2.left;
        }
        else
        {
            Rig.velocity = Vector2.zero;
        }
    }
}
