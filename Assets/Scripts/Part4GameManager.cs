using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Part4GameManager : MonoBehaviour
{
    [SerializeField] private Material[] abarrationMaterials;
    [SerializeField] private Color fadeAbarrationPrimaryColor;
    [SerializeField] private Color fadeAbarrationSecondaryColor;
    [SerializeField] private float fadeAbarrationVertexOffsetMult;

    private Camera mainCam;

    private Color initialPrimaryColor, initialSecondaryColor;
    private float initialVertexOffsetMult;
    int abarrationPrimaryColor, abarrationSecondaryColor, abarrationVertexOffsetMult;

    private void Start() {
        mainCam = Camera.main;

        abarrationPrimaryColor = Shader.PropertyToID("_Primary_Color");
        abarrationSecondaryColor = Shader.PropertyToID("_Secondary_Color");
        abarrationVertexOffsetMult = Shader.PropertyToID("_Vertex_Shift_Scale_Multiplier");

        initialPrimaryColor = abarrationMaterials[0].GetColor(abarrationPrimaryColor);
        initialSecondaryColor = abarrationMaterials[0].GetColor(abarrationSecondaryColor);
        initialVertexOffsetMult = abarrationMaterials[0].GetFloat(abarrationVertexOffsetMult);
    }

    public void FadeAbarritionColor(float time) {
        foreach (Material material in abarrationMaterials) {
            DOTween.To(() => material.GetColor(abarrationPrimaryColor), x => material.SetColor(abarrationPrimaryColor, x), fadeAbarrationPrimaryColor, time);
            DOTween.To(() => material.GetColor(abarrationSecondaryColor), x => material.SetColor(abarrationSecondaryColor, x), fadeAbarrationSecondaryColor, time);
        }
        
        DOTween.To(() => abarrationMaterials[0].GetFloat(abarrationVertexOffsetMult), x => abarrationMaterials[0].SetFloat(abarrationVertexOffsetMult, x), fadeAbarrationVertexOffsetMult, time);
    }

    private void OnDestroy() {
        DOTween.KillAll();
        foreach (Material material in abarrationMaterials) {
            material.SetColor(abarrationPrimaryColor, initialPrimaryColor);
            material.SetColor(abarrationSecondaryColor, initialSecondaryColor);
        }

        abarrationMaterials[0].SetFloat(abarrationVertexOffsetMult, initialVertexOffsetMult);
    }
}
