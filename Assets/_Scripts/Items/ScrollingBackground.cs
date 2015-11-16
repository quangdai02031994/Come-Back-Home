using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 1;
    private Renderer renderer;
    private float time = 0;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainController.Instance.IsPlaying)
        {
            time += Time.deltaTime;
            float x = Mathf.Repeat(time * speed, 1);
            Vector2 offset = new Vector2(x, 0);
            renderer.material.mainTextureOffset = offset;
        }
    }
}
