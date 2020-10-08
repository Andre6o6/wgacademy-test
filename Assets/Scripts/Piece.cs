using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    protected Slot originalSlot;

    private void Start()
    {
        //Set piece in a slot
        originalSlot = GetComponentInParent<Slot>();
        originalSlot.piece = this;
        transform.localPosition = Vector3.zero;
    }
}
