using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    public float parentInitialScale;
    public float parentMaxScale;

    public float childrenInitialScale;
    public float childrenMinScale;

    public float speed;
    public float effectTime;

    public bool scalechildren;
    public bool useEffect;

    public Transform[] objs;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(useEffect)
        {
            useEffect = false;
            StartCoroutine(EffectIn());
        }
    }
  
    public void StartEffect()
    {
        StartCoroutine(EffectIn());
    }

    IEnumerator EffectIn()
    {
        float time = effectTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(parentMaxScale, parentMaxScale), speed * Time.deltaTime);

            if (scalechildren)
            {
                foreach (Transform t in objs)
                {
                    t.localScale = Vector3.Lerp(t.transform.localScale, new Vector2(childrenMinScale, childrenMinScale), speed * Time.deltaTime);
                }
            }


            yield return null;
        }
        yield return null;

        transform.localScale = new Vector2(parentMaxScale, parentMaxScale);

        if (scalechildren)
        {
            foreach (Transform t in objs)
            {
                t.localScale = new Vector2(childrenMinScale, childrenMinScale);
            }
        }
        StartCoroutine(EffectOut());
    }

    IEnumerator EffectOut()
    {
        float time = effectTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector2(parentInitialScale, parentInitialScale), speed * Time.deltaTime);

            if (scalechildren)
            {
                foreach (Transform t in objs)
                {
                    t.localScale = Vector3.Lerp(t.transform.localScale, new Vector2(childrenInitialScale, childrenInitialScale), speed * Time.deltaTime);
                }
            }

            yield return null;
        }
        yield return null;

        transform.localScale = new Vector2(parentInitialScale, parentInitialScale);
        if (scalechildren)
        {
            foreach (Transform t in objs)
            {
                t.localScale = new Vector2(childrenInitialScale, childrenInitialScale);
            }
        }
    }
}
