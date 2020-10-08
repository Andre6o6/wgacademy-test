using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    //To set difficulty
    public static int shuffleIterations = 20;

    public int width;
    public int height;
    private Slot[,] slots;

    public Slot slotPrefab;
    public DraggablePiece piecePrefab;
    public Piece blockerPrefab;

    public RowChecker[] checkers;

    private DraggablePiece[] allPieces;

    private void Awake()
    {
        //TODO rescale slots area
        
        slots = new Slot[width, height];
        SpawnSlots();
        SetUpNeighbours();

        allPieces = new DraggablePiece[height * (width / 2 + width % 2)];
        SpawnPieces();
        SpawnBlockers();

        Shuffle();
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

        //Set up checkers
        for (int i = 0; i < height; i++)
        {
            for (int k = 0; k < checkers.Length; k++)
            {
                checkers[k].slots.Add(slots[i, 2*k]);
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
        //TODO no hardcoding
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j += 2)
            {
                var piece = Instantiate(piecePrefab, slots[i, j].transform);

                switch (j)
                {
                    case 0: piece.color = Piece.PieceColor.red; break;
                    case 2: piece.color = Piece.PieceColor.green; break;
                    case 4: piece.color = Piece.PieceColor.blue; break;
                }

                piece.transform.localPosition = Vector3.zero;

                //Add checking for win condition
                piece.onMove.AddListener(() => {
                    foreach (var checker in checkers)
                    {
                        checker.Check();
                    }
                });

                piece.SetupSlot();

                allPieces[(j / 2) * width + i] = piece;
            }
        }
    }

    private void SpawnBlockers()
    {
        for (int i = 1; i < width; i += 2)
        {
            for (int j = 0; j < height; j += 2)
            {
                var obj = Instantiate(blockerPrefab, slots[j, i].transform);
                obj.SetupSlot();
            }
        }
    }

    private void Shuffle()
    {
        for (int iter = 0; iter < shuffleIterations; iter++)
        {
            for (int i = 0; i < allPieces.Length; i++)
            {
                if (Random.value < 0.5f)
                    allPieces[i].Shuffle();
            }
        }

        for (int i = 0; i < allPieces.Length; i++)
        {
            allPieces[i].Lineup();
        }
    }
}
