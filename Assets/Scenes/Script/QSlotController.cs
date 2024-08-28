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

    // ���콺 �����Ͱ� UI ��� ���� �ִ��� Ȯ���ϴ� �޼���
    private bool IsPointerOverUIObject()
    {
        // EventSystem�� ����Ͽ� ���콺�� UI ��� ���� �ִ��� üũ
        return EventSystem.current.IsPointerOverGameObject();
    }

    // ���� ���콺�� �ö� UI ������Ʈ�� Raycast�� ���� �������� �޼���
    private GameObject GetHoveredUIObject(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        // Raycast ��� �� ���� ���� �ִ� ������Ʈ ��ȯ
        if (results.Count > 0)
        {
            // Raycast�� ������Ʈ�� ������ �θ��� �ڽ����� Ȯ��
            if (results[0].gameObject.transform != gameObject.transform)
            {
                // �θ��� �ڽ��̸� ��ȯ
                return results[0].gameObject;
            }
        }

        return null;
    }


}
