using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Piece))]
public class RowChecker : MonoBehaviour
{
    private Piece.PieceColor color;

    public List<Slot> slots;
    public Slot startingSlot;

    public bool rightColor;

    public UnityEvent onCheck;

    private void Start()
    {
        color = GetComponent<Piece>().color;

        if (startingSlot != null)
        {
            slots = new List<Slot>();
            slots.Add(startingSlot);
            Slot currSlot = startingSlot;

            while (currSlot.neighbours.down != null)
            {
                currSlot = currSlot.neighbours.down;
                slots.Add(currSlot);
            }
        }
    }

    public void Check()
    {
        foreach (var slot in slots)
        {
            //Piece is absent from the slot or wrong color
            if (slot.piece == null || slot.piece.color != color)
            {
                rightColor = false;
                return;
            }
        }
        rightColor = true;

        onCheck?.Invoke();
    }

}
