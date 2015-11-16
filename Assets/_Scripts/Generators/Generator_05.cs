using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generator_05 : MonoBehaviour {

    private List<Vector3> list_position = new List<Vector3>();

    private int index = 0;
    private bool _instantiate = true;

    public int countPref;
    public GameObject Prefabs;

    public float distan = 1;
    public float speed = 5;


	// Use this for initialization
	void Start ()
    {
        Reset();
    }

    void Update()
    {
        distan += Time.deltaTime * speed;
        
        if (MainController.Instance.IsPlaying && transform.childCount < countPref && _instantiate)
        {
            SpawnPref();
        }

        if (index >= list_position.Count && _instantiate)
        {
            Debug.Log("Het" + this.gameObject.name);
            _instantiate = false;
        }
    }
    public void Reset()
    {
        list_position = GetComponent<iTweenPath>().nodes;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        distan = 1;
        index = 0;
        _instantiate = true;
    }


    public void SpawnPref()
    {
        GameObject o = Instantiate(Prefabs, Vector2.zero, Quaternion.identity) as GameObject;
        o.transform.parent = this.gameObject.transform;
        o.transform.localPosition = new Vector3(list_position[index].x + distan, list_position[index].y, 0);
        index++;
    }
	
}
