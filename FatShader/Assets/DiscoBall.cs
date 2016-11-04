using UnityEngine;
using System.Collections;

public class DiscoBall : MonoBehaviour {

    [SerializeField]
    private Material fatMat;
    [SerializeField]
    private AudioSource audio;

    private bool beatHappen;

    private float[] spectrum = new float[256];

    [SerializeField][Range(0,5)]
    private float fatness;

    void Update()
    {
        audio.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        float value = spectrum[0];

        fatMat.SetFloat("_Amount", fatness);


        if (spectrum[0] > 0.1 && beatHappen == false)
        {
            fatMat.SetFloat("_Amount", 0.5f);
            StartCoroutine(LerpFatness());
            beatHappen = true;
        }
        else if(beatHappen == false)
        {
            beatHappen = false;

            fatMat.SetFloat("_Amount", Mathf.Lerp(0.5f, 0, 0.3f));
        }
    }

    IEnumerator LerpFatness()
    {
        yield return new WaitForSeconds(0.45f);
        beatHappen = false;
    }


}
