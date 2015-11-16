using UnityEngine;
using System.Collections;

/// <summary>
/// Cái này dùng để lặp background
/// </summary>

public class ReShowBackground : MonoBehaviour {	
	public Transform showUpPos;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == Tags.BG)
		{
			//Nếu là đối tượng BG thì chuyển về vị trí kia
			Vector3 newPos = other.gameObject.transform.position;
			newPos.x = showUpPos.position.x;
			other.gameObject.transform.position = newPos;
			Debug.Log("Reshow up lại backgorund: "+other.gameObject.name);
		}else{
			Debug.Log("Va phải cái gì đó!");
		}
	}
}
