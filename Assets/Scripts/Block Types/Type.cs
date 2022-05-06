using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Type : MonoBehaviour
{
    public Sprite sprite;
    public virtual bool multiple_inputs {get;}
    public virtual bool multiple_outputs {get;}
    public virtual bool do_action {get;}
    public virtual bool numurated {get;}
    public virtual bool pass_condition {get;}
}
