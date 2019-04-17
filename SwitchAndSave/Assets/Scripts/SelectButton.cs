using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectButton : MonoBehaviour {

	public static event Action OnSelect = delegate {};

	private void OnMouseUpAsButton() {
		OnSelect();
	}
}
