using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLine : MonoBehaviour
{
    public string coTag;
    public GameObject Dline;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == coTag)
        {
            Dline.GetComponent<Collider2D>().enabled = false;
        }
    }
}
