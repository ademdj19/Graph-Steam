using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class logicPanel : MonoBehaviour
{
  public TMP_InputField name_field;
  public TMP_Dropdown type_field;
  public GameObject container;
  RectTransform RTContainer,RTPanel;
  int logic_instances_count = 0;
  [Header("___ Prefabs ___")]
  public GameObject[] logic_types;
  private void Start(){
    RTContainer = container.GetComponent<RectTransform>();
    RTPanel = GetComponent<RectTransform>();
    type_field.ClearOptions();
    foreach(GameObject lg in logic_types){
      LogicType logic_type = lg.GetComponent<LogicType>();
      type_field.options.Add (new TMP_Dropdown.OptionData(logic_type.show_name));
    }
  }
  private void Update(){
    logic_instances_count = container.transform.childCount;
  }
  public void create_logic(){
    if(name_field.text == "" || !Condition.is_valid_identifier(name_field.text)){
      name_field.placeholder.GetComponent<TextMeshProUGUI>().text = "'" + name_field.text + "'" + " is not identifier";
      name_field.text = "";
      return;
    }else if(Condition.reciptivities.ContainsKey(name_field.text)){
      name_field.placeholder.GetComponent<TextMeshProUGUI>().text = "'" + name_field.text + "'" + "is already defined";
      name_field.text = "";
      return;
    }
    GameObject logic_instance = Instantiate(logic_types[type_field.value]);
    logic_instance.GetComponent<LogicType>().set_name(name_field.text);
    logic_instance.transform.parent = container.transform;
    logic_instance.GetComponent<RectTransform>().position = Vector3.zero;
    name_field.text = "";
    name_field.placeholder.GetComponent<TextMeshProUGUI>().text = "variable name";
  }
  
}
