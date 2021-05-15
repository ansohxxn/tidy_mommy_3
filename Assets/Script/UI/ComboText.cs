using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ComboText : MonoBehaviour
{
    [SerializeField] AnimationClip animClip;
    TextMeshPro comboText;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public GameObject myGameObject;

    public void Show_ComboText(int combo, Vector2 pos)
    {
        if (myTransform == null) myTransform = GetComponent<Transform>();
        if (myGameObject == null) myGameObject = gameObject;
        if (comboText == null) comboText = GetComponent<TextMeshPro>();

        comboText.text = combo.ToString() + " Combo";
        myTransform.position = pos;

        StartCoroutine(Hide_ComboText());
    }

    IEnumerator Hide_ComboText()
    {
        yield return Managers.Co.WaitSeconds(animClip.length);
        myGameObject.SetActive(false);
    }
}
