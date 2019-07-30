using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image button;
    private Image background;
    private Vector3 playerPos;

    void Start()
    {
        background = GetComponent<Image>();
        button = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, ped.position , ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / background.rectTransform.sizeDelta.x);
            pos.y = (pos.y / background.rectTransform.sizeDelta.y);

            playerPos = new Vector3(pos.x , pos.y , 0);
            playerPos = (playerPos.magnitude > 1.0f) ? playerPos.normalized : playerPos;

            button.rectTransform.anchoredPosition = new Vector3(playerPos.x * (background.rectTransform.sizeDelta.x / 2), 
            playerPos.y * (background.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        playerPos = Vector3.zero;
        button.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetXValue()
    {
        return playerPos.x;
    }

    public float GetYValue()
    {
        return playerPos.y;
    }
}
