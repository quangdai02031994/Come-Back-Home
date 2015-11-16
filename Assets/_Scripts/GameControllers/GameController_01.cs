using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameController_01 : BaseGameController
{
    public List<GeneratorWithPath> generators;
    private float quangDuongDaDi = 0;
    private float x0 = 0;

    public override void Ready()
    {
        Debug.Log(this.gameObject.name + ".Ready");
        this.gameObject.SetActive(true);
    }

    public override void Play()
    {
    }

   

    public override void ResetLevel()
    {
        foreach (var g in generators)
            g.Reset();

    }
    public override void LevelFinish()
    {
        float x = background.transform.position.x - 20;
        background.transform.DOMoveX(x, 2);
        StartCoroutine(Clear());
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
