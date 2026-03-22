using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BlockSelector : MonoBehaviour
{
    public BlockPlacer placer;
    public Transform buttonContainer;
    public Button blockButtonPrefab;
    public LevelData levelData;

    private List<Button> buttons = new List<Button>();

    void Start()
    {
        GenerateButtons();
    }

    void GenerateButtons()
    {
        // Limpiar botones anteriores
        foreach (var btn in buttons)
        {
            Destroy(btn.gameObject);
        }
        buttons.Clear();

        // Crear un botón por cada BlockData disponible en el nivel
        foreach (var block in levelData.availableBlocks)
        {
            Button newButton = Instantiate(blockButtonPrefab, buttonContainer);

            RectTransform buttonRT = newButton.GetComponent<RectTransform>();
            buttonRT.sizeDelta = new Vector2(100, 100);

            TMP_Text txt = newButton.GetComponentInChildren<TMP_Text>();
            if (txt != null)
            {
                txt.text = $"${block.cost}";
            }

            Image img = newButton.transform.Find("Icon")?.GetComponent<Image>();
            if (img != null && block.icon != null)
            {
                img.sprite = block.icon;
                img.preserveAspect = true;
                img.rectTransform.anchoredPosition = Vector2.zero;
                img.rectTransform.sizeDelta = new Vector2(80, 80);
            }

            // Asignar función al botón
            BlockData capturedBlock = block;
            newButton.onClick.AddListener(() => placer.SetCurrentBlock(capturedBlock));

            buttons.Add(newButton);
        }
    }
}