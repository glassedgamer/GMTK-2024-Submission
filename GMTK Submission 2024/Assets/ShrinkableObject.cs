using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkableObject : MonoBehaviour
{
    public float shrinkTime = 1f;

    public bool shrunk = false;

    public void ShrinkGunShrinksTheThing()
    {
        shrunk = true;
        this.transform.localScale /= 2;

        StartCoroutine(RegrowTheThing());
    }

    IEnumerator RegrowTheThing()
    {
        yield return new WaitForSeconds(shrinkTime);

        shrunk = false;
        this.transform.localScale *= 2;
    }
}
