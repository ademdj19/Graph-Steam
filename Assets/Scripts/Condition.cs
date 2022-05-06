using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Text.RegularExpressions;

public static class Condition
{
    public static Dictionary<string, bool> reciptivities = new Dictionary<string, bool>();
    private static DataTable dt = new DataTable();
    public static bool paused = true;
    public static bool eval_transition(string transition_string){
        foreach (var name_value_pair in reciptivities){
            string my_key = name_value_pair.Key;
            bool my_value = name_value_pair.Value;
            if(transition_string.Contains(my_key)){
              if(my_value)transition_string = transition_string.Replace(my_key,"1");
              else transition_string = transition_string.Replace(my_key,"0");
            }
        }
        transition_string = transition_string.Replace("1","true");
        transition_string = transition_string.Replace("0","false");
        transition_string = transition_string.Replace("."," AND ");
        transition_string = transition_string.Replace("+"," OR ");
        transition_string = transition_string.Replace("!"," NOT ");
        Debug.Log(transition_string);
        bool result = (bool)dt.Compute(transition_string,"");
        return result;
    }
    public static bool is_valid_identifier(string id){
      const string start = @"^(?:((?!\d)\w+(?:\.(?!\d)\w+)*)\.)?((?!\d)\w+)$";
      const string extend = @"(\p{Mn}|\p{Mc}|\p{Nd}|\p{Pc}|\p{Cf})";
      Regex ident = new Regex(string.Format("{0}({0}|{1})*", start, extend));
      id = id.Normalize();
      return ident.IsMatch(id);
    }

}
