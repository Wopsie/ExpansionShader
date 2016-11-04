using UnityEngine;
using System.Collections;

public class DiscoBall : MonoBehaviour {

    [SerializeField]
    private Material fatMat;
    private AudioSource audio;

    private bool beatHappen;

    private float[] spectrum = new float[256];

    [SerializeField][Range(0,2)]
    public float fatness;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void SliderChange(float slide)
    {
        fatness = slide;
    }

    void LateUpdate()
    {
        fatMat.SetFloat("_Amount", fatness);
        audio.GetSpectrumData(spectrum, 0, FFTWindow.Triangle);
        float value = spectrum[0];

        if (spectrum[0] > 0.1 && beatHappen == false)
        {
            fatness = 2;
            beatHappen = true;
            StartCoroutine(LerpFatness());
        }
        else if(beatHappen == false)
        {
            fatness -= 0.08f;
        }

        if(fatness < 0)
        {
            fatness = 0;
        }
    }

    IEnumerator LerpFatness()
    {
        yield return new WaitForSeconds(0.50f);
        beatHappen = false;
    }
}
