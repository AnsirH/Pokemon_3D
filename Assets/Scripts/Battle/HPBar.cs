using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image img;
    // Start is called before the first frame update
    void Awake()
    {
        img = GetComponent<Image>();
    }
    
    public void SetHP (float hpNormalized)
    {
        img.fillAmount = hpNormalized;
    }

    public IEnumerator SetHPSmooth(float newHp)
    {
        float curHp = img.fillAmount;
        float changeAmt = curHp - newHp;

        while(curHp - newHp > Mathf.Epsilon)
        {
            curHp -= changeAmt * Time.deltaTime;
            img.fillAmount = curHp;
            yield return null;
        }

        img.fillAmount = newHp;
    }
}
