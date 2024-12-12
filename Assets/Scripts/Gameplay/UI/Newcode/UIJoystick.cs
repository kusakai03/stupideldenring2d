using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event EventHandler onHoldingJoystick;
    public event EventHandler onReleasingJoystick;
    [SerializeField] private Image baseJ;
    [SerializeField] private Image handle;
    private Vector3 inputVector;
    private Vector3 startPos;
    private void Start(){
        inputVector = Vector3.zero;
    }
    private void DragJoystick(PointerEventData e)
    {
        Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseJ.rectTransform, e.position, e.pressEventCamera, out pos))
            {
                pos.x = pos.x / (baseJ.rectTransform.sizeDelta.x / 2);
                pos.y = pos.y / (baseJ.rectTransform.sizeDelta.y / 2);
                inputVector = new Vector2(pos.x, pos.y);
                inputVector = inputVector.magnitude > 1 ? inputVector.normalized : inputVector;

                handle.rectTransform.anchoredPosition = new Vector2(
                    inputVector.x * (baseJ.rectTransform.sizeDelta.x / 2),
                    inputVector.y * (baseJ.rectTransform.sizeDelta.y / 2)
            );
        }
    }

    private void Hold(PointerEventData e){
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseJ.rectTransform, e.position, e.pressEventCamera, out pos)){
            startPos = pos;
        }
        DragJoystick(e);
        onHoldingJoystick?.Invoke(this, EventArgs.Empty);
    }
    private void Release(PointerEventData e){
        inputVector = Vector3.zero;
        handle.rectTransform.anchoredPosition = inputVector;
        onReleasingJoystick?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerDown(PointerEventData e)
    {
        Hold(e);
    }

    public void OnPointerUp(PointerEventData e)
    {
        Release(e);
    }

    void IDragHandler.OnDrag(PointerEventData e)
    {
        DragJoystick(e);
    }
    private void Update(){
        GameSetting.Instance.joystickDir = inputVector;
    }
    public float getInputX(){
        return inputVector.x;
    }
    public float getInputY(){
        return inputVector.y;
    }
}
