using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class toggleButton : LogicType
{
  public override string show_name {get{return "Toggle";}}
  string name;
  bool value;
  public TextMeshProUGUI name_label;
  public TextMeshProUGUI value_label;
  public void Start(){
    value = false;
    Condition.reciptivities[name] = value;
  }
  public void Update(){
    Condition.reciptivities[name] = value;
  }
  public override void set_name(string name_){
    name = name_;
    name_label.text = name;
  }

  public override string get_name(){
    return name;
  }
  public void toggle(){
    value = !value;
    if(value) value_label.text = "1";
    else value_label.text = "0";
  }
  public override void delete(){
    Condition.reciptivities.Remove(name);
    Destroy(gameObject);
  }
}
