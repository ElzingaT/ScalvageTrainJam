using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatMeter : MonoBehaviour
{
    public int current = 0;
    public int max = 5;

    public Color defaultColor;
    public Color outlineColor;

	void Start ()
    {
        GameObject sectionPrefab = transform.GetChild(0).gameObject;
        defaultColor = sectionPrefab.GetComponent<Image>().color;

        for (int i = 1; i < max; i++)
        {
            Instantiate(sectionPrefab, transform);
        }
	}

    public void SetValue(int value)
    {
        for (int i = 0; i < max; i++)
        {
            Image section = transform.GetChild(i).gameObject.GetComponent<Image>();
            Outline outline = transform.GetChild(i).gameObject.GetComponent<Outline>();
            section.color = (i <= (value - 1)) ? defaultColor : Color.black;
            outline.effectColor = (i <= (value - 1)) ? outlineColor : Color.black;
        }

        current = value;
    }
}
