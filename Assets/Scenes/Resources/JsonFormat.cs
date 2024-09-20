using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonLine
{
    public string line;
}
[Serializable]
public class JsonLines
{
    public JsonLine[] Lines;
}
[Serializable]
public class ClockWork
{
    public string name;
    public string des;
    public string[] param;
}
[Serializable]
public class ClockWorks 
{
    public ClockWork[] Works;
}
