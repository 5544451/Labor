using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Qslot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private bool isSelected = false;
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1f);

    [SerializeField] TextMeshProUGUI dialog;
    [SerializeField] GameObject InfBG;

    void Start()
    {
        // ���� ������ ����
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // ���콺�� ������Ʈ ���� ���� �� ������ ����
            transform.localScale = hoverScale;
            InfBG.SetActive(true);
            string typeTxt = "Selected" + gameObject.name;
            dialog.text = typeTxt;
            isSelected = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isSelected)
        {
            // ���콺�� ������Ʈ�� ���� �� ������ ������� ����
            transform.localScale = originalScale;
            isSelected = false;
            InfBG.SetActive(false);
        }
    }
}
