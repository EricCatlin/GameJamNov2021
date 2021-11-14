using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomePropAfterCollision : MonoBehaviour
{
    public void BecomeProp()
    {
        //  gameObject.tag = "Prop";
        //  gameObject.layer = LayerMask.NameToLayer("Prop");
        Destroy(gameObject, 10);
    }
}
