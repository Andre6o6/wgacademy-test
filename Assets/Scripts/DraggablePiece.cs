using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class DraggablePiece : Piece, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEvent onMove;

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
        Lineup();

        onMove?.Invoke();
    }

    public void Shuffle()
    {
        int possibilities = 0;
        foreach (Slot s in originalSlot.neighbours.GetAll())
        {
            if (s != null && s.piece == null)
                possibilities += 1;
        }

        if (possibilities == 0)
            return;

        float p = Random.value;
        foreach (Slot s in originalSlot.neighbours.GetAll())
        {
            if (s != null && s.piece == null)
            {
                //Basically discrete probability
                p -= 1f / possibilities;
                if (p <= 0)
                {
                    originalSlot.piece = null;
                    originalSlot = s;
                    originalSlot.piece = this;
                    break;
                }
            }
        }
    }

    public void Lineup()
    {
        transform.SetParent(originalSlot.transform);
        transform.localPosition = Vector3.zero;
    }
}
