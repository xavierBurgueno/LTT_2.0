using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldButtonDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color onButtonClickedColor;
    [Space(50)]

    private bool pointerDown;
    public UnityEvent onHoldClick;
    [Space]
    public UnityEvent onHoldUp;
    private Color startingColor;
    private Image img;

    void Start()
    {
        img = GetComponent<Image>();
        startingColor = GetComponent<Image>().color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        img.color = onButtonClickedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
        img.color = startingColor;

        if (onHoldUp != null)
        {
            onHoldUp.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            if (onHoldClick != null)
            {
                onHoldClick.Invoke();
            }
        }

    }


}
