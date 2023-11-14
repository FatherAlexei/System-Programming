using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : MonoBehaviour
{
    Unit unit;
    void Start()
    {
        Unit unit = new Unit();
    }

    public void ReceiveHealing()
    {
        StopCoroutine(MyCoroutine());
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        for (int i = 0; i < 6; i++)
        {
            if (unit.hp >= unit.maxHp)
               yield break;
            unit.hp += 5;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
