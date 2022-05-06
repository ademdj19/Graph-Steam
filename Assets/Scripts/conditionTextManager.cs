using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class conditionTextManager : MonoBehaviour
{
    string condition_text = "";
    TextMesh text_dp;
    void Start(){
      text_dp = transform.GetChild(0).GetComponent<TextMesh>();
    }
    public void set_condition_text(string text){
      condition_text = text;
      text_dp.text = text;
    }
    public string get_condition_text(){
      return condition_text;
    }
}
