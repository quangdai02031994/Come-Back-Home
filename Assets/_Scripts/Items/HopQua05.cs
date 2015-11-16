using UnityEngine;
using System.Collections;

public class HopQua05 : MonoBehaviour {

    public Sprite[] hopqua;
    private SpriteRenderer render;

	void Start () {

        render = GetComponent<SpriteRenderer>();
        render.sprite = hopqua[Random.Range(0, hopqua.Length)];
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.ToLower().Contains("player"))
        {
            MainPlayer.Instance.AnHopQua();
            Destroy(this.gameObject);
        }
    }
}
