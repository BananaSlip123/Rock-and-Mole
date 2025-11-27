using System.Collections.Generic;
using UnityEngine;

public class CartUI : MonoBehaviour
{
    //En la villa muestra si tienes materiales q recoger, y cuales son. Y boton de recoger
    //En partida muestra materiales conseguidos en mochila, y botón de enviar a la aldea

    [SerializeField] Scene scene;
    [SerializeField] MaterialInfoUI[] materialInfoUIs;
    [SerializeField] GameObject go_button;
    [SerializeField] GameObject go_noMaterialsSignal;
    enum Scene { village, game}
    private void OnEnable()
    {
        OnUpdate();
    }
    private void OnUpdate()
    {
        if (scene == Scene.village)
            MaterialsToShow = GameData.CartInventory.Objects;
        else if (scene == Scene.game)
            MaterialsToShow = GameData.RunInventory.Objects;
    }
    private void OnDisable()
    {
        
    }

    public void OnButtonPressed()
    {
        if (scene == Scene.village)
        {
            GameData.Inventory.AddObjects(GameData.CartInventory.Objects);
        } 
        else if (scene == Scene.game)
        {
            GameData.CartInventory.AddObjects(GameData.RunInventory.Objects);
        }
        OnUpdate();
    }

    public SortedDictionary<MaterialName, int> MaterialsToShow
    {
        set
        {
            bool isNull = value == null;
            bool isEmpty = true;

            if (isNull) return;

            int idx = 0;
            foreach (MaterialName key in value.Keys)
            {
                if (value.ContainsKey(key) && value[key] != 0)
                {
                    isEmpty = false;

                    if (idx >= materialInfoUIs.Length)
                        throw new System.Exception("Tiene que haber mas huecos en el array que posibles valores de material");

                    materialInfoUIs[idx].gameObject.SetActive(true);
                    materialInfoUIs[idx].Amount = value[key];
                    materialInfoUIs[idx].MaterialAssigned = key;

                    idx++;
                }
            }
            //desactivar el resto
            for (; idx < materialInfoUIs.Length; idx++)
            {
                materialInfoUIs[idx].gameObject.SetActive(false);
            }

            go_noMaterialsSignal.SetActive(isEmpty || isNull);
            go_button.SetActive(!isEmpty && !isNull);
        }
    }
}
