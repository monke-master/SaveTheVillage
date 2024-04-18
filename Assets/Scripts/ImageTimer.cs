using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    public float maxTime;
    public bool tick;
    
    private Image _image;
    private float _currentTime;
    
    void Start()
    {
        _image = GetComponent<Image>();
    }
    
    void Update()
    {
        tick = false;
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            tick = true;
            _currentTime = maxTime;
        }

        _image.fillAmount = _currentTime / maxTime;
    }

    public void SetMaxTime(float maxTime)
    {
        this.maxTime = maxTime;
        _currentTime = maxTime;
    }
    
}
