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
        // 원래 스케일 저장
        originalScale = transform.localScale;
        Slot = ClockWorksController.UISlot[slotnum];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // 마우스가 오브젝트 위에 있을 때 스케일 조정
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
            // 마우스가 오브젝트를 떠날 때 스케일 원래대로 복원
            transform.localScale = originalScale;
            isSelected = false;
            InfBG.SetActive(false);
        }
    }
}
