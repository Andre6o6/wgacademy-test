using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public struct Neighbours
    {
        public Slot up, down, left, right;

        public bool Contains(Slot slot)
        {
            return up == slot || down == slot || left == slot || right == slot;
        }

        public IEnumerable GetAll()
        {
            yield return up;
            yield return down;
            yield return left;
            yield return right;
        }
    }
    public Neighbours neighbours;

    public Piece piece;     //Piece that occupies current slot

    public Color highlightedColor;
    private Color defaultColor;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
    }

    public void Highlight()
    {
        image.color = highlightedColor;
    }

    public void Dehighlight()
    {
        image.color = defaultColor;
    }
}
