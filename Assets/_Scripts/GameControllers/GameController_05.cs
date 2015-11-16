using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController_05 : BaseGameController
{
    public List<Generator_05> generators;
    public override void Ready()
    {
        this.gameObject.SetActive(true);
    }

    public override void LevelFinish()
    {
        float x = background.transform.position.x - 30;
        background.transform.DOMoveX(x, 2);
        StartCoroutine(Clear());
    }

    public override void Play()
    {

    }

    
    public override void ResetLevel()
    {
        foreach (var g in generators)
            g.Reset();
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
