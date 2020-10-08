using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(DraggablePiece))]
public class Colored : MonoBehaviour
{
    public Color[] colors;

    private void Start()
    {
        var piece = GetComponent<DraggablePiece>();
        var image = GetComponent<Image>();

        int idx = (int)piece.color;     //Just hope it casts correctly
        image.color = colors[idx];
    }
}
