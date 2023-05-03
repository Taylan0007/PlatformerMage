using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Extensions;

public class Signals : Singleton<Signals>
{
   public UnityAction<string> OnSkillUse = delegate {  }; 
}
