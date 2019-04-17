using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveButton : MonoBehaviour {

	public static event Action OnSaveRequested = delegate { };

  private void OnMouseUpAsButton()
  {
    OnSaveRequested();
  }
}
