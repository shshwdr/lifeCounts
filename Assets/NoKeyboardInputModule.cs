//using UnityEngine;
//using UnityEngine.EventSystems;

//public class NoKeyboardInputModule : StandaloneInputModule {

//    public override void Process()
//    {
//        bool usedEvent = SendUpdateEventToSelectedObject();

//        ProcessMouseEvent();
//    }

//    private bool SendUpdateEventToSelectedObject()
//    {
//        if (eventSystem.currentSelectedObject == null)
//            return false;

//        var data = GetBaseEventData();
//        ExecuteEvents.Execute(eventSystem.currentSelectedObject, data, ExecuteEvents.updateSelectedHandler);
//        return data.used;
//    }

//    private void ProcessMouseEvent()
//    {
//        bool pressed = Input.GetMouseButtonDown(0);
//        bool released = Input.GetMouseButtonUp(0);

//        var pointerData = GetMousePointerEventData();

//        // Take care of the scroll wheel
//        float scroll = Input.GetAxis("Mouse ScrollWheel");
//        pointerData.scrollDelta.x = 0f;
//        pointerData.scrollDelta.y = scroll;

//        // Process the first mouse button fully
//        ProcessMousePress(pointerData, pressed, released);
//        ProcessMove(pointerData);

//        if (!Mathf.Approximately(scroll, 0.0f))
//        {
//            var scrollHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(pointerData.pointerCurrentRaycast.go);
//            ExecuteEvents.ExecuteHierarchy(scrollHandler, pointerData, ExecuteEvents.scrollHandler);
//        }
//    }

//    private void ProcessMousePress(PointerEventData pointerEvent, bool pressed, bool released)
//    {
//        var currentOverGo = pointerEvent.pointerCurrentRaycast.go;

//        // PointerDown notification
//        if (pressed)
//        {
//            pointerEvent.eligibleForClick = true;
//            pointerEvent.delta = Vector2.zero;
//            pointerEvent.pressPosition = pointerEvent.position;
//            pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;

//            // search for the control that will receive the press
//            // if we can't find a press handler set the press 
//            // handler to be what would receive a click.
//            var newPressed = ExecuteEvents.ExecuteHierarchy(currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler);

//            // didnt find a press handler... search for a click handler
//            if (newPressed == null)
//                newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

//            if (newPressed != pointerEvent.pointerPress)
//            {
//                pointerEvent.pointerPress = newPressed;
//                pointerEvent.rawPointerPress = currentOverGo;
//                pointerEvent.clickCount = 0;
//            }

//            // Save the drag handler as well
//            pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(currentOverGo);

//            if (pointerEvent.pointerDrag != null)
//                ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);

//            // Selection tracking
//            var selectHandlerGO = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
//            eventSystem.SetSelectedGameObject(selectHandlerGO, pointerEvent);
//        }

//        // PointerUp notification
//        if (released)
//        {
//            ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);

//            // see if we mouse up on the same element that we clicked on...
//            var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentOverGo);

//            // PointerClick and Drop events
//            if (pointerEvent.pointerPress == pointerUpHandler && pointerEvent.eligibleForClick)
//            {
//                float time = Time.unscaledTime;

//                if (time - pointerEvent.clickTime < 0.3f)
//                    ++pointerEvent.clickCount;
//                else
//                    pointerEvent.clickCount = 1;
//                pointerEvent.clickTime = time;

//                ExecuteEvents.Execute(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
//            }
//            else if (pointerEvent.pointerDrag != null)
//            {
//                ExecuteEvents.ExecuteHierarchy(currentOverGo, pointerEvent, ExecuteEvents.dropHandler);
//            }

//            pointerEvent.eligibleForClick = false;
//            pointerEvent.pointerPress = null;
//            pointerEvent.rawPointerPress = null;

//            if (pointerEvent.pointerDrag != null)
//                ExecuteEvents.Execute(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);

//            pointerEvent.pointerDrag = null;

//            // redo pointer enter / exit to refresh state
//            // so that if we moused over somethign that ignored it before
//            // due to having pressed on something else
//            // it now gets it.
//            HandlePointerExitAndEnter(pointerEvent, null);
//            HandlePointerExitAndEnter(pointerEvent, currentOverGo);
//        }
//    }
//}
