using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class ClockWorksController : MonoBehaviour
{
    public static List<ClockWork> UISlot = new List<ClockWork>();

    // Start is called before the first frame update
    void Awake()
    {
        readJSON();
    }

    void readJSON() // 제이슨 파일을 읽는 함수
    {
        ClockWork clockWork = new ClockWork();

        TextAsset textAsset = Resources.Load<TextAsset>("clockworks");
        ClockWorks jsonData = JsonUtility.FromJson<ClockWorks>(textAsset.text);

        foreach (ClockWork lt in jsonData.Works)
        {
            UISlot.Add(lt);
        }
    }
}
