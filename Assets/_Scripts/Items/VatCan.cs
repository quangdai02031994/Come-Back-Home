using UnityEngine;
using System.Collections;

public class VatCan : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        Check(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Check(other.gameObject);
    }

    private void Check(GameObject other)
    {
        if (other.name == "Player" || other.name.ToLower().Contains("player"))
        {
            Debug.Log("VatCan.OnCollisionEnter2D: Player vs VatCan");
            if (MainPlayer.Instance.IsAlive)
                MainPlayer.Instance.PlayerDie();
        }
    }
}
