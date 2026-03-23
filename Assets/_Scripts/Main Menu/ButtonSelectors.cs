using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonSelectors : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private AudioClip buttonCicked, buttonSelect;
    [SerializeField] private float minPitchClick, maxPitchClick, clickVolume, minPitchSelect, maxPitchSelect, selectVolume;

    private bool isHighlighted;
    private bool isSelected;

    //private void Start()
    //{
    //    if (selectors != null)
    //    {
    //        selectors.SetActive(false);
    //    }
    //}
    private void Awake()
    {
        Button self = GetComponent<Button>();
        self.onClick.AddListener(() => OnButtonClick());

    }
    private void Update()
    {
        if (isHighlighted || isSelected)
        {


            //if (selectors != null)
            //{
            //    selectors.SetActive(true);
            //}
        }
        else
        {
            //if (selectors != null)
            //{
            //    selectors.SetActive(false);
            //}
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected) AudioManager.Instance.PlayEffect(buttonSelect, Vector3.zero, selectVolume, true, minPitchSelect, maxPitchSelect);

        isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHighlighted = false;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (!isHighlighted) AudioManager.Instance.PlayEffect(buttonSelect, Vector3.zero, selectVolume, true, minPitchSelect, maxPitchSelect);

        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
    }

    private void OnButtonClick()
    {
        AudioManager.Instance.PlayEffect(buttonCicked, Vector3.zero, clickVolume, true, minPitchClick, maxPitchClick);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}
