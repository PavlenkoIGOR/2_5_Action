using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; //зависит от UGUI или UI

using static UnityEngine.Rendering.DebugUI;

public class Inventory : MonoBehaviour
{
    public Canvas canvas;
    public GameObject _inventoryPanel;
    [SerializeField] GameObject _scrollView;
    public bool isInventoryOpen = false;

    public InventoryObject inventoryObject;

    public List<InventorySlot> slots = new List<InventorySlot>();

    void Start()
    {
        // Убедимся, что панель не видима в начале
        if (_inventoryPanel != null)
        {
            _inventoryPanel.SetActive(isInventoryOpen);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_inventoryPanel != null)
            {
                // Переключаем видимость панели
                _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
                isInventoryOpen = !isInventoryOpen;
                if (inventoryObject.slots.Count != 0)
                {
                    // Получаем объект Content внутри Scroll View
                    Transform contentTransform = _scrollView.transform.Find("Viewport/Content");

                    if (contentTransform != null)
                    {
                        for (int i = 0; i < inventoryObject.slots.Count; i++)
                        {
                            var comp = contentTransform.GetChild(i).GetComponent<Image>();  //для UI
                            comp.sprite = inventoryObject.slots[i].itemData.icon;   //для UI
                        }
                        Debug.Log($"CellPanels - {contentTransform.childCount}");
                    }
                    else
                    {
                        Debug.Log("Content not found!");
                    }
                }
            }
        }
    }
}