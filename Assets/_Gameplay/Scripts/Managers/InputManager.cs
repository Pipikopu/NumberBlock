using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("HitInterface");
        if (Input.touchCount > 0)
        {
            var ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hitInfor;
            if (Physics.Raycast(ray, out hitInfor))
            {
                Debug.Log("HitInterface1");
                var chosenEnemy = hitInfor.collider.GetComponent<Enemy>();
                if (chosenEnemy != null)
                {
                    Debug.Log("HitInterface2");
                    chosenEnemy.OnBeingChosen();
                }
            }
        }
        else
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfor;
            if (Physics.Raycast(ray, out hitInfor))
            {
                Debug.Log("HitInterface11");
                var chosenEnemy = hitInfor.collider.GetComponent<Enemy>();
                if (chosenEnemy != null)
                {
                    Debug.Log("HitInterface21");
                    chosenEnemy.OnBeingChosen();
                }
            }
        }
    }
}
