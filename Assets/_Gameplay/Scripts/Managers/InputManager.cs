using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Hit");
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfor;
        if (Physics.Raycast(ray, out hitInfor))
        {
            Debug.Log("Hit1");
            var chosenEnemy = hitInfor.collider.GetComponent<Enemy>();
            if (chosenEnemy != null)
            {
                Debug.Log("Hit2");
                chosenEnemy.OnBeingChosen();
            }
        }
    }
}
