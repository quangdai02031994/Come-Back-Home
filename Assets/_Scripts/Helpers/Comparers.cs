using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SapXepTheoChieuTangCuaX : IComparer<Vector3>
{
    public int Compare(Vector3 truoc, Vector3 sau)
    {
        return truoc.x.CompareTo(sau.x);
    }
}
