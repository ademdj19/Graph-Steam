using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Type block_type;
    public bool is_active = false;
    public Block[] next_blocks;
    string[] disactivation_conditions;
    public string Id = "";
    bool type_changed = false;
    private int used_o_pins = 1;
    private int used_i_pins = 1;
    public GameObject add_remove_i_pin;
    public GameObject add_remove_o_pin;
    bool o_count_changed = false;
    bool i_count_changed = false;
    public float space_between_pins = 2.56f;
    GameObject[] pins_o;
    public GameObject pin_o_primitive;
    GameObject[] pins_i;
    public GameObject pin_i_primitive;
    public SpriteRenderer sp_renderer;
    bool pass_condition;
    void Awake(){
        set_sprite();
        sp_renderer = GetComponent<SpriteRenderer>();
    }

    void Start(){
        next_blocks = new Block[1];
        disactivation_conditions = new string[1];
        pins_o = new GameObject[1];
        pins_o[0] = Instantiate(pin_o_primitive, transform.position, Quaternion.identity);
        pins_o[0].transform.SetParent(transform);
        pins_o[0].transform.localPosition = new Vector3(0, -6.5f, 0);
        pins_i = new GameObject[1];
        pins_i[0] = Instantiate(pin_i_primitive, transform.position, Quaternion.identity);
        pins_i[0].transform.SetParent(transform);
        pins_i[0].transform.localPosition = new Vector3(0, 6.5f, 0);


        if(block_type is ReturnArrowType){
          pins_o[0].transform.RotateAround(transform.position,Vector3.forward,180);
          pins_i[0].transform.RotateAround(transform.position,Vector3.forward,180);
        }else if(block_type is InitialType){
          is_active = true;
        }
        if(block_type.multiple_outputs){
            add_remove_o_pin.SetActive(true);
        }else{
            add_remove_o_pin.SetActive(false);
        }
        if(block_type.multiple_inputs){
            add_remove_i_pin.SetActive(true);
        }else{
            add_remove_i_pin.SetActive(false);
        }
        Id = IdManager.get_id();
    }

    void Update(){
        if(o_count_changed){
            refresh_outputs();
            o_count_changed = false;
        }
        if(i_count_changed){
            refresh_inputs();
            i_count_changed = false;
        }
        if(type_changed){
            set_sprite();
        }
        check_new_connections();
        if(is_active){sp_renderer.color = Color.green;}
        else{sp_renderer.color = Color.white;}
        if(!Condition.paused && is_active){
          check_transitions();
          foreach(Block b in next_blocks){
            if(b!= null && (b.block_type is DiverganceOrType || b.block_type is ConverganceAndType)){
              is_active = b.get_stat();
            }
          }
        }
        if(block_type.pass_condition){
          foreach(GameObject p in pins_o){
            p.transform.GetChild(0).gameObject.SetActive(false);
          }
        }else{
          for(int i = 0 ; i < pins_o.Length;i++){
            if(next_blocks[i]!=null && (  next_blocks[i].block_type is DiverganceOrType ||
                                          next_blocks[i].block_type is ConverganceAndType)
            ){
              pins_o[i].GetComponent<connectionPoint>().pass_condition = true;
              pins_o[i].transform.GetChild(0).gameObject.SetActive(true);
            }else{
              pins_o[i].transform.GetChild(0).gameObject.SetActive(true);
            }
          }
        }



    }
    void check_new_connections(){
      for(int i = 0 ; i < pins_o.Length;i++){
        connectionPoint connection_p = pins_o[i].GetComponent<connectionPoint>();
        conditionTextManager cp_condition = connection_p.transform.GetChild(0).GetComponent<conditionTextManager>();
        if(connection_p.other_end != null){
          next_blocks[i] = connection_p.other_end.transform.parent.gameObject.GetComponent<Block>();
        }
        disactivation_conditions[i] = cp_condition.get_condition_text();

      }
    }
    public void set_type(Type bt){
        block_type = bt;
        type_changed = true;
    }

    void set_sprite(){
        GetComponent<SpriteRenderer>().sprite = block_type.sprite;
    }
    // this function is used by inspector to update content
    public Block[] get_next(){return next_blocks;}

    public void add_o_pin(){
        used_o_pins += 1;
        o_count_changed = true;
    }

    public void add_i_pin(){
        used_i_pins += 1;
        i_count_changed = true;
    }
    public bool get_stat(){
      return is_active;
    }
    public void activate(){
      is_active = true;
    }
    public void disactivate(){
      is_active = false;
    }
    public void remove_o_pin(){
        used_o_pins = Mathf.Max(used_o_pins-1,1);
        o_count_changed = true;
    }

    public void remove_i_pin(){
        used_i_pins = Mathf.Max(used_i_pins-1,1);
        i_count_changed = true;
    }
    public void refresh_outputs(){
        next_blocks = new Block[used_o_pins];
        disactivation_conditions = new string[used_o_pins];
        foreach(GameObject p in pins_o){
            Destroy(p);
        }
        pins_o = new GameObject[used_o_pins];
        float width = space_between_pins * (1+used_o_pins);
        if(width != sp_renderer.size.x){
            sp_renderer.size = new Vector2(Mathf.Max(width,10.24f),10.24f);
        }
        for(int i = 0 ; i < used_o_pins ; i++){
            pins_o[i] = Instantiate(
                pin_o_primitive,
                Vector3.zero,
                Quaternion.identity
            );
            pins_o[i].transform.SetParent(transform.parent);
            pins_o[i].transform.localPosition = new Vector3(
                space_between_pins*i-(space_between_pins*(used_o_pins-1))/2,
                -6.5f,
                0
            );
            if(block_type.pass_condition){
              pins_o[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void set_pin_condition(int pin_index,string condition_text){
      pins_o[pin_index].transform.GetChild(0).GetComponent<conditionTextManager>()
      .set_condition_text(condition_text);
    }
    public string get_pin_condition(int pin_index){
      return pins_o[pin_index].transform.GetChild(0).GetComponent<conditionTextManager>()
      .get_condition_text();
    }
    public void refresh_inputs(){
        foreach(GameObject p in pins_i){
            Destroy(p);
        }
        pins_i = new GameObject[used_i_pins];
        float width = space_between_pins * (1+used_i_pins);
        if(width != sp_renderer.size.x){
            sp_renderer.size = new Vector2(Mathf.Max(width,10.24f),10.24f);
        }
        for(int i = 0 ; i < used_i_pins ; i++){
            pins_i[i] = Instantiate(
                pin_i_primitive,
                Vector3.zero,
                Quaternion.identity
            );
            Debug.Log("hey "+i);
            pins_i[i].transform.SetParent(transform);
            pins_i[i].transform.localPosition = new Vector3(
                space_between_pins*i-(space_between_pins*(used_i_pins-1))/2,
                6.5f,
                0
            );
        }
    }
    void check_transitions(){
      if(block_type.pass_condition){
        foreach(Block b in next_blocks){
          if(b!=null){
            b.activate();
            disactivate();
          }
        }

      }
      else{
        for(int i = 0; i < disactivation_conditions.Length; i++){
          bool pass;
          if(!pins_o[i].GetComponent<connectionPoint>().pass_condition){
            pass = (bool)Condition.eval_transition(disactivation_conditions[i]);
          }else{
            pass = true;
          }
          if(pass && next_blocks[i] != null){
            next_blocks[i].activate();
            disactivate();
          }
        }
      }
    }
    public void delete(){
      Destroy(transform.parent.gameObject);
    }

}
