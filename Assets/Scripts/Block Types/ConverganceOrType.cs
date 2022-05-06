using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverganceOrType : Type
{
    public override bool multiple_inputs {get{return true;}}
    public override bool multiple_outputs {get{return false;}}
    public override bool do_action {get{return false;}}
    public override bool numurated {get{return false;}}
    public override bool pass_condition {get{return true;}}

}
