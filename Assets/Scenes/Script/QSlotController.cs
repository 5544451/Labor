using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QSlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private bool Over;
    private GameObject currentHoveredObject;
    [SerializeField] TextMeshProUGUI dialog;
    [SerializeField] GameObject InfBG;


    private void Start()
    {
        InfBG.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject hoveredObject = GetHoveredUIObject(eventData);
        if (hoveredObject != null)
        {
            currentHoveredObject = hoveredObject;
            currentHoveredObject.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            InfBG.SetActive(true);
            string typeTxt = "Selected" + currentHoveredObject.name;
            dialog.text = typeTxt;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentHoveredObject != null)
        {
            InfBG.SetActive(false);
            currentHoveredObject.transform.localScale = Vector3.one;
            currentHoveredObject = null;
        }
    }

    // 마우스 포인터가 UI 요소 위에 있는지 확인하는 메서드
    private bool IsPointerOverUIObject()
    {
        // EventSystem을 사용하여 마우스가 UI 요소 위에 있는지 체크
        return EventSystem.current.IsPointerOverGameObject();
    }

    // 현재 마우스가 올라간 UI 오브젝트를 Raycast를 통해 가져오는 메서드
    private GameObject GetHoveredUIObject(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // Raycast 결과 중 가장 위에 있는 오브젝트 반환
        if (results.Count > 0)
        {
            // Raycast된 오브젝트가 지정된 부모의 자식인지 확인
            if (results[0].gameObject.transform != gameObject.transform)
            {
                // 부모의 자식이면 반환
                return results[0].gameObject;
            }
        }

        return null;
    }


}
