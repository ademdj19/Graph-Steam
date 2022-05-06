using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class connectionPoint : MonoBehaviour
{
    public bool is_down;
    public Transform line_hook;
    public connectionPoint other_end;
    public Material line_mat;
    LineRenderer line;
    public bool pass_condition;
    public void request_connection_from_this(){
        Connections.request_connection(this);
    }
    private void Start(){
      if(is_down){
        line = gameObject.AddComponent<LineRenderer>();
        line.material = line_mat;
      }
    }
    private void Update(){
      if(is_down){
        line.SetPosition(0, line_hook.position);
        if(other_end != null){
          line.SetPosition(1, other_end.line_hook.position) ;
        }else{
          line.SetPosition(1,line_hook.position);
        }
      }
    }
}
