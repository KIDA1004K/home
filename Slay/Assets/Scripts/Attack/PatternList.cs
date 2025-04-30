using System.Collections.Generic;
using UnityEngine;

public class PatternList : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static List<Pattern> pattern_list;

    private void Start()
    {
       pattern_list = new List<Pattern>();
    }
}
