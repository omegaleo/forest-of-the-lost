using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Theme
{
    public List<string> tagsToCheck = new List<string>()
        {
            "UI"
        };

    [Header("Text")]
    public Font font;
    public string textColorHex;
    public bool hasShadow;
    public string shadowColorHex;
    public Vector2 shadowDistance = new Vector2(1.0f, -1.0f);

    [Header("Button")]
    public Sprite btnBackground;
    public string btnColorNormalHex;
    public string btnColorHighlightedHex;
    public string btnColorPressedHex;
    public string btnColorSelectedHex;
    public string btnColorDisabledHex;

    [Header("Panel")]
    public Sprite panelBackground;
    public string panelColorHex;

    public ColorBlock GetButtonColors()
    {
        ColorBlock colors = new ColorBlock();

        colors.disabledColor = btnColorDisabledHex.ColorFromHexString();
        colors.highlightedColor = btnColorHighlightedHex.ColorFromHexString();
        colors.normalColor = btnColorNormalHex.ColorFromHexString();
        colors.pressedColor = btnColorPressedHex.ColorFromHexString();
        colors.selectedColor = btnColorSelectedHex.ColorFromHexString();

        return colors;
    }
}
