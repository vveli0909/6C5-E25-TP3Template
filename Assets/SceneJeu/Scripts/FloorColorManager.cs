using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorColorManager : MonoBehaviour
{
    public Renderer floorRenderer;
    public Material successMat;
    public Material failMat;

    public void SetSuccess() => floorRenderer.material = successMat;
    public void SetFail() => floorRenderer.material = failMat;

}
