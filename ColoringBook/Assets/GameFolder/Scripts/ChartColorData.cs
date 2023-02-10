using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class ChartColorData : Singleton<ChartColorData>
{
    int ID = 1;
    
    public Color SelectedColor;
    public float fillDelay = 0f;
    public RawImage TeddyImg;

    Texture2D provinceMap;

    List<Pixel> PixelsData = new List<Pixel>();
    Dictionary<Color, int> Colors = new Dictionary<Color, int>();

    public ColorSwatch colorSwatch;

    List<ColorSwatch> ColorSwatches = new List<ColorSwatch>();

    Vector2 mousePos = new Vector2();

    RectTransform rect;
    int Width = 0, Height = 0;

    void Start()
    {
        provinceMap = TeddyImg.texture as Texture2D;
        rect = TeddyImg.GetComponent<RectTransform>();
        Width = (int) rect.rect.width;
        Height = (int) rect.rect.height;

        GenerateProvinces();
        CreateColorSwatches();
    }
    void GenerateProvinces()
    {
        Color[] pixels = provinceMap.GetPixels();

        for (int x = 0; x < provinceMap.width; x++)
        {
            for (int y = 0; y < provinceMap.height; y++)
            {
                if (pixels[x + y * provinceMap.width].a != 0)
                {
                    int id = ID;
                    if (Colors.ContainsKey(pixels[x + y * provinceMap.width]))
                    {
                        id = Colors[pixels[x + y * provinceMap.width]];
                    }
                    else if (provinceMap.GetPixel(x, y) != new Color32(0, 6, 45, 255))
                    {
                        Colors.Add(pixels[x + y * provinceMap.width], ID);
                        ID++;
                    }

                    var pixel = new Pixel();
                    pixel.PixelColor = provinceMap.GetPixel(x, y);
                    pixel.posX = x;
                    pixel.posY = y;
                    PixelsData.Add(pixel);

                    if (provinceMap.GetPixel(x, y) != new Color32(0,6,45,255))//(0, 0.02353f, 0.17647f, 1))
                    {
                        provinceMap.SetPixel(x, y, Color.white);
                    }

                }
            }
        }
        provinceMap.Apply();
    }
    void CreateColorSwatches()
    {
        foreach (KeyValuePair<Color, int> kvp in Colors)
        {
            ColorSwatch colorswatch = GameObject.Instantiate(colorSwatch, GameObject.Find("Canvas/ScrollPalet/Items").transform);

            colorswatch.SetData(kvp.Value, kvp.Key);

            ColorSwatches.Add(colorswatch);
        }
    }
    public void SelectedPixels(Color color)
    {
        Color[] pixels = provinceMap.GetPixels();

        for (int x = 0; x < provinceMap.width; x++)
        {
            for (int y = 0; y < provinceMap.height; y++)
            {
                if (pixels[x + y * provinceMap.width].a != 0)
                {
                    if (provinceMap.GetPixel(x, y) != new Color(0, 0.02353f, 0.17647f, 1))
                    {
                        int id = PixelsData.FindIndex(enty => enty.posX == x && enty.posY == y);
                        if (PixelsData[id].PixelColor == color)
                        {
                            provinceMap.SetPixel(x, y, new Color(0.70196f, 0.70196f, 0.80000f));
                        }
                    }
                }
            }
        }
        provinceMap.Apply();
    }
    IEnumerator Flood(int x, int y, Color newColor)
    {
        WaitForSeconds wait = new WaitForSeconds(fillDelay);
        if (x >= 0 && x < provinceMap.width && y >= 0 && y < provinceMap.height)
        {
            yield return wait;
            if (provinceMap.GetPixel(x, y) == new Color(0.70196f, 0.70196f, 0.80000f))
            {
                int id = PixelsData.FindIndex(enty => enty.posX == x && enty.posY == y);
                if (PixelsData[id].PixelColor == newColor)
                {
                    provinceMap.SetPixel(x, y, newColor);
                    StartCoroutine(Flood(x + 1, y, newColor));
                    StartCoroutine(Flood(x - 1, y, newColor));
                    StartCoroutine(Flood(x, y + 1, newColor));
                    StartCoroutine(Flood(x, y - 1, newColor));
                }
            }

            provinceMap.Apply();
        }
    }
    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, Camera.main, out mousePos);

        mousePos.x = Width - (Width / 2 - mousePos.x);
        mousePos.y = Mathf.Abs((Height / 2 - mousePos.y) - Height);

        if (Input.GetMouseButton(0))
        {
            if(mousePos.x > 0 && mousePos.y > 0)
            {
                int id = PixelsData.FindIndex(x => x.posX == (int)mousePos.x && x.posY == (int)mousePos.y);
                if(id == -1) { return; }

                if (PixelsData[id].PixelColor == SelectedColor)
                StartCoroutine(Flood((int)mousePos.x, (int)mousePos.y, SelectedColor));
            }
        }
    }
    public struct Pixel
    {
        public Color PixelColor;
        public int posX;
        public int posY;
    }
}
