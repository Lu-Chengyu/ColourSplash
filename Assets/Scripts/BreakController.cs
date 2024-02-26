using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakController : MonoBehaviour
{
    public int attackTimes = 0;

    public void Break()
    {
        Destroy(this.gameObject);
    }
}
