using System.Collections;
using System.Collections.Generic;
using UnityEngine;








public abstract class LogicType : MonoBehaviour
{
  public virtual string show_name {get;}
  public abstract void delete();
  public abstract void set_name(string name_);
  public abstract string get_name();
}
