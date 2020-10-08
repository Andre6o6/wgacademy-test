using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum PieceColor { red, green, blue };
    public PieceColor color;

    protected Slot originalSlot;

    public void SetupSlot()
    {
        //Set piece in a slot
        originalSlot = GetComponentInParent<Slot>();
        if (originalSlot != null)
        {
            originalSlot.piece = this;
            transform.localPosition = Vector3.zero;
        }
    }

    private void Start()
    {
        if (originalSlot == null)
        {
            SetupSlot();
        }
    }
}
