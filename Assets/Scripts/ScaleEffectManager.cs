using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffectManager : MonoBehaviour
{
    public ScaleEffect[] scaleEffects;

    public bool useEffect;

    // Start is called before the first frame update
    void Start()
    {
        scaleEffects = GetComponentsInChildren<ScaleEffect>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(useEffect)
    //    {
    //        StartCoroutine(ActiveEffect());

    //        useEffect = false;
    //    }
    //}

    public void StartEffect()
    {
        StartCoroutine(ActiveEffect());
    }

    public IEnumerator ActiveEffect()
    {
        WaitForSeconds wfs = new WaitForSeconds(scaleEffects[0].effectTime * 2);

        for (int i = 0; i < scaleEffects.Length; i++)
        {
            scaleEffects[i].StartEffect();
            yield return wfs;
        }

        yield return wfs;
    }
}
