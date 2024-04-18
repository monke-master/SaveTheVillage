using UnityEngine;
using UnityEngine.UI;

public class ImageButtonTimer : MonoBehaviour
{
    public OnClickTried OnClickTried;
    public OnTimerComplete OnTimerComplete;

    private bool _enable;
    private Image _image;
    private float _currentTime;
    private float _maxTime;
    public AudioSource _audio;

    private void Start()
    {
        _enable = true;
        _image = GetComponent<Image>();
    }

    public void OnButtonClicked()
    {
        if (_enable)
        {
            _audio.PlayOneShot((AudioClip)Resources.Load("Sounds/button", typeof(AudioClip)));
            _enable = !OnClickTried();
        }
    }
    
    public void SetMaxTime(float maxTime)
    {
        _maxTime = maxTime;
        _currentTime = maxTime;
    }
    
    void Update()
    {
        if (_enable) return;
        
        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = _maxTime;
            _enable = true;
            OnTimerComplete();
        }

        _image.fillAmount = _currentTime / _maxTime;
    }
}


public delegate bool OnClickTried();
public delegate void OnTimerComplete();