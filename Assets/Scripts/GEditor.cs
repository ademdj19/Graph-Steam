using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




public class GEditor : MonoBehaviour
{
    public Inspector inspector;
    public GameObject selected_block_obj;

    public bool moving = false;
    void Update(){
        if(selected_block_obj != null && !EventSystem.current.IsPointerOverGameObject()){
            if(Input.GetButtonDown("Move") ) moving = true;
            if(Input.GetKeyDown("x")){
              selected_block_obj.transform.GetChild(0).gameObject.GetComponent<Block>().delete();
              selected_block_obj = null;
            }
        }
        if(moving){
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition.z = 0;
            worldPosition.x = Mathf.Round(worldPosition.x/0.5f)*0.5f;
            worldPosition.y = Mathf.Round(worldPosition.y/0.5f)*0.5f;
            selected_block_obj.transform.position = worldPosition;
        }
        if(Input.GetMouseButtonDown(0)) {
            check_selected();
            moving = false;
        }
        if(Input.GetKeyDown("s")){
          foreach (KeyValuePair<string, bool> kvp in Condition.reciptivities)
              Debug.Log (kvp.Key + "|" + kvp.Value);
        }
    }
    void check_selected(){
        if(EventSystem.current.IsPointerOverGameObject()) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000)) {
            if(hit.transform.gameObject.CompareTag("Block")){
                selected_block_obj = hit.transform.parent.gameObject;
                GameObject block_child_of_selected = selected_block_obj.transform.GetChild(0).gameObject;
                inspector.set_block(block_child_of_selected.GetComponent<Block>());
            }
        }else{
            selected_block_obj = null;
            inspector.unset_block();
        }
        Debug.DrawRay(ray.origin,ray.direction*1000, Color.green, 2, false);

    }
    public void set_block_as_new(GameObject block_obj){
      selected_block_obj = block_obj;
      moving = true;
    }
    public void pause_play(){
      Condition.paused = !Condition.paused;
    }
}
