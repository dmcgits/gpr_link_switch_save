using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadButton : MonoBehaviour {

	public static event Action OnLoadRequested = delegate { };

  private void OnMouseUpAsButton()
  {
    OnLoadRequested();
  }
}
