using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    public int width;
    public int height;
    private Slot[,] slots;

    public Slot slotPrefab;
    public DraggablePiece piecePrefab;
    public Piece blockerPrefab;

    private void Awake()
    {
        slots = new Slot[width, height];
        SpawnSlots();
        SetUpNeighbours();
        SpawnPieces();
        SpawnBlockers();

        //TODO rescale slots area
    }

    private void SpawnSlots()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var slot = Instantiate(slotPrefab, this.transform);
                slots[i, j] = slot;
            }
        }
    }

    private void SetUpNeighbours()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i - 1 >= 0)
                {
                    slots[i, j].neighbours.left = slots[i - 1, j];
                }
                if (i + 1 < width)
                {
                    slots[i, j].neighbours.right = slots[i + 1, j];
                }
                if (j - 1 >= 0)
                {
                    slots[i, j].neighbours.up = slots[i, j - 1];
                }
                if (j + 1 < height)
                {
                    slots[i, j].neighbours.down = slots[i, j + 1];
                }
            }
        }
    }

    private void SpawnPieces()
    {
        for (int i = 0; i < height; i++)
        {
            var piece = Instantiate(piecePrefab, slots[i, 0].transform);
            piece.color = DraggablePiece.PieceColor.red;
            piece.transform.localPosition = Vector3.zero;
        }

        for (int i = 0; i < height; i++)
        {
            var piece = Instantiate(piecePrefab, slots[i, 2].transform);
            piece.color = DraggablePiece.PieceColor.green;
            piece.transform.localPosition = Vector3.zero;
        }

        for (int i = 0; i < height; i++)
        {
            var piece = Instantiate(piecePrefab, slots[i, 4].transform);
            piece.color = DraggablePiece.PieceColor.blue;
            piece.transform.localPosition = Vector3.zero;
        }
    }

    private void SpawnBlockers()
    {
        Instantiate(blockerPrefab, slots[1, 1].transform);
        Instantiate(blockerPrefab, slots[1, 3].transform);
        Instantiate(blockerPrefab, slots[3, 1].transform);
        Instantiate(blockerPrefab, slots[3, 3].transform);
    }
}
