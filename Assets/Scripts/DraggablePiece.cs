using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggablePiece : Piece, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum PieceColor { red, green, blue };
    public PieceColor color;

    private GraphicRaycaster graphicRaycaster;
    private Transform canvasTransform;

    private void Awake()
    {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        canvasTransform = GetComponentInParent<Canvas>().transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvasTransform);   // Parent ourselves to canvas
        transform.SetAsLastSibling();           // put us at the end to render on top of everything

        HighlightNeighbours();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DehighlightNeighbours();

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            Slot slot = result.gameObject.GetComponentInParent<Slot>();
            if (slot != null && slot.piece == null && originalSlot.neighbours.Contains(slot))
            {
                //Get into new slot
                PutIntoSlot(slot);
                return;
            }
        }

        //Return to original slot
        PutIntoSlot(originalSlot);
    }

    private void HighlightNeighbours()
    {
        foreach (Slot n in originalSlot.neighbours.GetAll())
        {
            if (n != null && n.piece == null)
            {
                n.Highlight();
            }
        }
    }

    private void DehighlightNeighbours()
    {
        foreach (Slot n in originalSlot.neighbours.GetAll())
        {
            if (n != null && n.piece == null)
            {
                n.Dehighlight();
            }
        }
    }

    private void PutIntoSlot(Slot slot)
    {
        //Switch slots
        originalSlot.piece = null;  //FIXME call methods
        originalSlot = slot;
        originalSlot.piece = this;

        //Place in center
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
    }
}
