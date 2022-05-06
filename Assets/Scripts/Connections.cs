using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public static class Connections
{
    public static connectionPoint connection_point_held;
    public static void start_connection(connectionPoint cp){
      connection_point_held = cp;
    }
    public static void end_connection(connectionPoint cp){
      connection_point_held.other_end = cp;
      cp.other_end = connection_point_held;
      connection_point_held = null;
    }
    public static void handle_connection_request(connectionPoint cp){
      if(connection_point_held == cp){
        if(connection_point_held.other_end!=null)connection_point_held.other_end.other_end = null;
        connection_point_held.other_end = null;
        connection_point_held = null;
        return;
      }
      if(connection_point_held != null && connection_point_held.is_down != cp.is_down){
        end_connection(cp);
      }else{
        start_connection(cp);
      }
    }
    public static void request_connection(connectionPoint cp){
      handle_connection_request(cp);
    }



}
