using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UScore : MonoBehaviour
{
    TextMeshProUGUI txt;

   
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "" + GetPlayerModel.Instance.pm.scoreCount;
    }
}
