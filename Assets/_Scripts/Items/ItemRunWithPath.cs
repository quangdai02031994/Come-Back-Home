using UnityEngine;
using System.Collections;

public class ItemRunWithPath : MonoBehaviour
{
    private GeneratorWithPath _generator;
    private Rigidbody2D Rig;
    private bool isChangeOk = true;

    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Hàm này được gọi trong thằng cha của nó chính là thằng Generate chúng
    /// </summary>
    public void Setup(GeneratorWithPath generator)
    {
        _generator = generator;
    }

    // Update is called once per frame
    void Update()
    {

        if (MainController.Instance.IsPlaying)
        {
            if (_generator != null)
                Rig.velocity = _generator.speed * Vector2.left;
        }
        else
        {
            Rig.velocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.ToLower().Equals("destroy") && isChangeOk)
        {
            GoToNextPosition();
        }
    }



    public void GoToNextPosition()
    {
        if (this.gameObject.name.ToLower().Contains("bac"))
        {
            int k;
        }
        Vector3? nextPos = _generator.GetNextPosition();
        if (nextPos != null)
        {
            //this.transform.position = (Vector3)nextPos;

            Helper.DatLenPhiaTren(this.transform, (Vector3)nextPos);
            this.gameObject.SetActive(true);

            //Ngăn chặn việc bắt 2 va chạm trong cùng 1 frame
            isChangeOk = false;
            StartCoroutine(Reset());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        isChangeOk = true;
    }
}
