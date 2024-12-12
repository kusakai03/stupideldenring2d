using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISkillPageListSwipe : MonoBehaviour
{
    public float swipeThreshold = 50f; 
    private Vector2 startDragPosition;
    private float accumulatedSwipeDistance = 0f;
    [SerializeField] private UISkillEquip skillEquip;
    public void OnBeginDrag(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        startDragPosition = pointerData.position;
        accumulatedSwipeDistance = 0f;
    }

    public void OnDrag(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        float dragDistance = pointerData.position.x - startDragPosition.x;
        accumulatedSwipeDistance += dragDistance;

        if (Mathf.Abs(accumulatedSwipeDistance) >= swipeThreshold)
        {
            if (accumulatedSwipeDistance > 0)
            {
                skillEquip.SwipeRight();
            }
            else
            {
                skillEquip.SwipeLeft();
            }

            accumulatedSwipeDistance = 0f;
        }
        startDragPosition = pointerData.position;
    }
}