﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Script_SliderRescaleInputs : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public float onInput = 0.1f;
	private float lastValue;
	private Slider slider;
	private bool fromMouse = false;
	void Start () {
		slider = GetComponent<Slider>();
		lastValue = slider.value;
		slider.onValueChanged.AddListener(delegate {rescaleInput();});
	}
	
	private void rescaleInput() {
		if (slider.value == lastValue)
			return;
		if (!fromMouse) {
			slider.value = lastValue + ((slider.value < lastValue) ? -1 * onInput : onInput);
			lastValue = slider.value;
		} else {
			lastValue = slider.value;
		}
	}

	public void OnPointerDown(PointerEventData eventData) {
		fromMouse = true;
	}

	public void OnPointerUp(PointerEventData eventData) {
		fromMouse = false;
	}
}