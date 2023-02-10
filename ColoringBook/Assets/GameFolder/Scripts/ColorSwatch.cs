using UnityEngine;
using UnityEngine.UI;

public class ColorSwatch : MonoBehaviour
{
    public int ID { get; private set; }
    public Color Color{ get; private set; }

    bool Completed;
    bool Selected;
    Text Text;
    Image Background;
    Image Border;

    void Awake()
    {
        Border = transform.Find("Border").GetComponent<Image>();
        Background = transform.Find("Background").GetComponent<Image>();
        Text = transform.Find("Text").GetComponent<Text>();
    }

    public void SetData(int id, Color color)
    {
        ID = id;
        Text.text = id.ToString();
        Background.color = color;
        Color = color;
    }

    public void SetCompleted()
    {
        Completed = true;
        Text.text = "";
    }

    public void SetSelected(bool selected)
    {
        if (!Completed)
        {
            Selected = selected;
            if (Selected)
            {
                Border.color = Color.yellow;
            }
            else
            {
                Border.color = Color.black;
            }
        }

        ChartColorData.Instance.SelectedColor = Color;
        ChartColorData.Instance.SelectedPixels(Color);
    }
}
