using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    private float zoom_level = 20f;
    public float zoom_speed = 2f;
    public float max_zoom = 5f;
    public float min_zoom = 100f;
    public float drag_speed = 50f;
    Vector3 pre_mouse_position;
    void Update(){
        zoom_level = zoom_level+Input.GetAxis("Mouse ScrollWheel")*zoom_speed*Time.deltaTime;
        zoom_level = Mathf.Min(Mathf.Max(max_zoom,zoom_level),min_zoom);
        GetComponent<Camera>().orthographicSize = zoom_level;
        Vector3 mpos = Input.mousePosition;
        if(Input.GetMouseButton(0) && Input.GetKey("left alt")){
            transform.position += (mpos - pre_mouse_position)*-drag_speed*Time.deltaTime;
        }
        pre_mouse_position = mpos;
    }
    
    

}
