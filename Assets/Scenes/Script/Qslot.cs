using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Qslot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    private bool isSelected = false;
    public Vector3 hoverScale = new Vector3(1.5f, 1.5f, 1f);
    private ClockWork Slot;

    [SerializeField] int slotnum;
    [SerializeField] TextMeshProUGUI nameTXT;
    [SerializeField] TextMeshProUGUI desTXT;
    [SerializeField] GameObject InfBG;


    void Start()
    {
        // ���� ������ ����
        originalScale = transform.localScale;
        Slot = ClockWorksController.UISlot[slotnum];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // ���콺�� ������Ʈ ���� ���� �� ������ ����
            transform.localScale = hoverScale;
            InfBG.SetActive(true);

            nameTXT.text = Slot.name;
            desTXT.text = Slot.des;

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
