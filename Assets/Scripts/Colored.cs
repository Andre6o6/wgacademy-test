using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Colored : MonoBehaviour
{
    public Color[] colors;

    private void Start()
    {
        var image = GetComponent<Image>();

        var piece = GetComponent<Piece>();
        if (piece != null)
        {
            int idx = (int)piece.color;     //Just hope it casts correctly
            image.color = colors[idx];
        }
    }
}
