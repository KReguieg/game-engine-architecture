using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartButton : VRTK_InteractableObject {
	[Header("Button")]
	public UnityEngine.Events.UnityEvent ButtonUsed;
	public override void StartUsing(VRTK_InteractUse usingObject)
    {
		ButtonUsed.Invoke();
        base.StartUsing(usingObject);
		
    }
}
