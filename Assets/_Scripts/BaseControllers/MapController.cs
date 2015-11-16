using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour
{

    void Start()
    {

    }

    public void Click_Level(int i)
    {
        i--;
        Debug.Log("Click level: " + i);
        MainController.SetCurrentLevel(i);
        Application.LoadLevel(SceneName.InGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(SceneName.Intro);
        }
    }
}
