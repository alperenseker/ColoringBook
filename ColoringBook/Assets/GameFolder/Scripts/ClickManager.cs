using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }/*

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(mousePos.x);
        int y = Mathf.FloorToInt(mousePos.y);

        Pixel hoveredPixel = null;
        if (x >= 0 && x < Pixels.GetLength(0) && y >= 0 && y < Pixels.GetLength(1))
        {
            if (Pixels[x, y] != null)
            {
                hoveredPixel = Pixels[x, y];
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Check if we clicked on a color swatch
            int hitCount = Physics2D.RaycastNonAlloc(mousePos, Vector2.zero, Hits);
            for (int n = 0; n < hitCount; n++)
            {
                if (Hits[n].collider.CompareTag("ColorSwatch"))
                {
                    SelectColorSwatch(Hits[n].collider.GetComponent<ColorSwatch>());
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (hoveredPixel != null && !hoveredPixel.IsFilledIn)
            {
                if (SelectedColorSwatch != null && SelectedColorSwatch.ID == hoveredPixel.ID)
                {
                    hoveredPixel.Fill();
                    if (CheckIfSelectedComplete())
                    {
                        SelectedColorSwatch.SetCompleted();
                    }
                }
                else
                {
                    hoveredPixel.FillWrong();
                }
            }
        }
    }*/
}
