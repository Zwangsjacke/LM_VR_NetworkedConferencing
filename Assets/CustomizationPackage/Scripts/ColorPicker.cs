using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Transform colorWheel;
    [SerializeField] private Transform colorPicker;

    [SerializeField] private CustomizationManager customizationManager;

    [SerializeField] private int scaleDown = 5;

    public GameObject[] presetButtons;

    private Image colorWheelImage;
    private MeshRenderer colorPickerRenderer;

    private Vector3 lastPos;

    private Color[] texture = new Color[255 * 255];

    private void Start()
    {
        colorPickerRenderer = colorPicker.GetComponent<MeshRenderer>();
        colorWheelImage = colorWheel.GetComponent<Image>();
        GenerateTexture();
    }

    void Update()
    {
        if(lastPos != colorPicker.position)
        {
            lastPos = colorPicker.position;
            UpdatePointer();
        }
    }

    void UpdatePointer()
    {
        Color selectedColor = Color.black;

        for (int x = 0; x < 255; x++)
        {
            for (int y = 0; y < 255; y++)
            {
                if (x == (int)(colorPicker.localPosition.y * 255 + 128) && y == (int)(colorPicker.localPosition.x * 255 + 128))
                {
                    selectedColor = texture[x * 255 + y];
                    selectedColor.a = 1;
                }
            }
        }
        colorPickerRenderer.material.SetColor("_BaseColor", selectedColor);
        colorPickerRenderer.material.SetColor("_EmissionColor", selectedColor);

        customizationManager.onColorChanged(selectedColor);
    }

    void GenerateTexture()
    {
        Texture2D tex = new Texture2D(255, 255, TextureFormat.RGBA32, false);

        for (int x = 0; x < 255; x++)
        {
            for (int y = 0; y < 255; y++)
            {
                float distance = Vector2.Distance(Vector2.zero, new Vector2(128 - x, 128 - y))/128F;
                texture[x * 255 + y] = 
                    Color.HSVToRGB(
                        Remap(
                            Mathf.Atan2(128-x, 128 - y),
                            -Mathf.PI,
                            Mathf.PI,
                            0,
                            1),
                        distance*2,
                        Mathf.Clamp01(2-distance*2)
                        );

                //texture[x * 255 + y].a = (distance<1)?1:0;
            }
        }

        //tex.alphaIsTransparency = true;
        tex.SetPixels(texture);
        tex.Apply();

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 1);

        colorWheelImage.sprite = sprite;
    }

    public void SetPresetButtons(Color[] presets = null)
    {
        for (int i = 0; i < presetButtons.Length; i++)
        {
            if (i < presets.Length)
            {
                presetButtons[i].SetActive(true);
                presetButtons[i].transform.GetChild(0).GetComponent<MeshRenderer>().material.color = presets[i];
            }
            else
            {
                presetButtons[i].SetActive(false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 2)
        {
            colorPicker.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z);
            colorPicker.localPosition = new Vector3(colorPicker.localPosition.x, colorPicker.localPosition.y, 0);
        }
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

[System.Serializable]
public class UnityColorEvent : UnityEvent<Color>
{
}
