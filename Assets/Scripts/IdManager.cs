using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IdManager 
{
    public static int id_counter = -1;
    public static void increment_id(){
        id_counter++;
    }
    public static string get_id(){
        IdManager.increment_id();
        return id_counter.ToString();
    }
}
