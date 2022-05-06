using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inspector : MonoBehaviour
{
    public Block selected_block;
    RectTransform rect_transform;
    public float title_height = 26;
    public float content_height = 100;
    public TMP_InputField id;
    public TMP_InputField condition_input_field;

    public TMP_Dropdown output_pins;
    bool uptodate = true;
    bool user_hide = false;
    void Start(){
        rect_transform = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(selected_block != null){
            if(!uptodate){
                update_inspector();
            }
            show();
            if(selected_block.get_next().Length != output_pins.options.Count) uptodate = false;
        }else{
            hide();
        }
        if(Input.GetKeyDown("p")) user_hide = !user_hide;
        if(user_hide)hide();

    }
    public void set_block(Block selected){
        selected_block = selected;
        uptodate = false;

    }
    public void unset_block(){
        selected_block = null;
        uptodate = false;
    }
    void show(){
        rect_transform.sizeDelta = new Vector2(rect_transform.sizeDelta.x,title_height + content_height);
        for(int i = 1;i<transform.childCount;i++){
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    void hide(){
        rect_transform.sizeDelta = new Vector2(rect_transform.sizeDelta.x,title_height);
        for(int i = 1;i<transform.childCount;i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void update_inspector(){
        id.text = selected_block.Id;
        output_pins.options.Clear();
        for(int i = 0; i < selected_block.get_next().Length; i++){
            output_pins.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
        output_pins.value = 0;
        output_pins.RefreshShownValue();
        uptodate = true;
    }
    public void update_text_inputs(){
      condition_input_field.text = selected_block.get_pin_condition(output_pins.value);
    }
    public void set_condition_to_pin(int pin_index,string condition_text){
      selected_block.set_pin_condition(pin_index,condition_text);
    }
    public void update_block_condition(){
      set_condition_to_pin(output_pins.value,condition_input_field.text);
    }
}
