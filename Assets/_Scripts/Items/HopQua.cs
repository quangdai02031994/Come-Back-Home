using UnityEngine;
using System.Collections;

public class HopQua : MonoBehaviour {
    
    public Sprite[] colors;
    private SpriteRenderer render;
    private ItemRunWithPath itemRun;
    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        itemRun = GetComponent<ItemRunWithPath>();
    }

	void Start () {
        render.sprite = colors[Random.Range(0, colors.Length)];
    }

    public void ChangeColor()
    {
        render.sprite = colors[Random.Range(0, colors.Length)];
    }

    void OnEnable()
    {
        ChangeColor();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(this.gameObject.name + " va chạm với " + other.gameObject.name);
        if(other.gameObject.name.ToLower().Contains("player"))
        {
            itemRun.GoToNextPosition();
            MainPlayer.Instance.AnHopQua();
        }
    }
}
