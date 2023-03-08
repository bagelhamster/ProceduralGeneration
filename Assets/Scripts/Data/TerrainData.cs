using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class TerrainData : Updatable
{
    public float uniformScale = 2.5f;

    public bool useFalloff;
    public float meshHeightMultip;
    public AnimationCurve meshHeightCurve;

    public float minHeight
    {
        get
        {
            return uniformScale*meshHeightMultip * meshHeightCurve.Evaluate(0);

        }
    }
    public float maxHeight
    {
        get
        {
            return uniformScale*meshHeightMultip * meshHeightCurve.Evaluate(1);

        }
    }
}
