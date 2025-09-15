using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource music;
    public Slider volumeSlider;

    private float minVolume = 0.0f;
    private float maxVolume = 1.0f;


    void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        music = GameObject.Find("Music Player").GetComponent<AudioSource>();

        SetupSlider();
    }

    void SetupSlider()
    {
        volumeSlider.minValue = minVolume;
        volumeSlider.maxValue = maxVolume;
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
        volumeSlider.value = MainManager.volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateVolume(float amount)
    {
        MainManager.volume = amount;
        music.volume = MainManager.volume;
    }
}
