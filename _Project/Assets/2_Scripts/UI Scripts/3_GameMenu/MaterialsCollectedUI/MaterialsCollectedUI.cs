using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class MaterialsCollectedUI : MonoBehaviour
{
    #region SERIALIZABLE FIELDS
    [SerializeField] CallOutElements[] callOuts;

    TextMeshProUGUI[] txts_callOutsTexts;
    Image[] imgs_callOutsImages;
    Image[] imgs_callOutsIcons;

    [System.Serializable]
    public struct CallOutElements
    {
        public TextMeshProUGUI txt_callOutText;
        public Image img_callOutImage;
        public Image img_callOutIcon;
    }
    #endregion


    int size;
    bool isInit = false;

    List<Message> messages = new List<Message>();

    public static Dictionary<MaterialName, MaterialsData.IconData> Icons
    {
        get => MaterialsData.Icons;
    }
    private void OnEnable()
    {
        Init();

        GameData.RunInventory.OnMaterialsEarned += OnMaterialAdd;
        StartCoroutine(ActualizeUI());
        
    }
    void Init()
    {
        if (isInit) return;

        size = callOuts.Length;
        txts_callOutsTexts = new TextMeshProUGUI[size];
        imgs_callOutsImages = new Image[size];
        imgs_callOutsIcons = new Image[size];

        for (int i = 0; i < size; i++)
        {
            txts_callOutsTexts[i] = callOuts[i].txt_callOutText;
            imgs_callOutsIcons[i] = callOuts[i].img_callOutIcon;
            imgs_callOutsImages[i] = callOuts[i].img_callOutImage;
        }
        callOuts = null;

        isInit = true;
    }
    private void OnDisable()
    {
        StopCoroutine(ActualizeUI());
        GameData.RunInventory.OnMaterialsEarned -= OnMaterialAdd;
    }

    void OnMaterialAdd(MaterialName materialName, int amount)
    {
        messages.Add(new Message(materialName, amount));
    }
    IEnumerator ActualizeUI()
    {
        float deltaTime = 0.2f;

        while (true)
        {
            messages.RemoveAll(message => message.Actualize(deltaTime));

            int idx = 0;
            for (int i = messages.Count - 1; i >= 0; i--)
            {
                float alpha = messages[i].GetAlpha();

                //Texto
                txts_callOutsTexts[idx].text = messages[i].GetAmountToString();
                txts_callOutsTexts[idx].alpha = alpha;

                //Fondo
                imgs_callOutsImages[idx].color = new Color(0,0,0,alpha*0.1f);

                //Icono
                MaterialsData.IconData iconData = Icons[messages[i].GetMaterialName()];

                imgs_callOutsIcons[idx].sprite = iconData.Sprite;
                Color materialColor = iconData.Color;
                materialColor.a = alpha;
                imgs_callOutsIcons[idx].color = materialColor;

                idx++;
                if (idx >= size) break;
            }
            for (; idx < size; idx++ )
            {
                txts_callOutsTexts[idx].alpha = 0;
                imgs_callOutsImages[idx].color = new Color(0,0,0,0);
                imgs_callOutsIcons[idx].color = new Color(0,0,0,0);
            }

            yield return new WaitForSeconds(deltaTime);
        }
    }
    

    class Message
    {
        MaterialName _name;
        int _amount;
        float _lifeTime;

        const float MAX_LIFE_TIME = 3;
        public Message(MaterialName name, int amount)
        {
            _name = name;
            _amount = amount;
            _lifeTime = 0;
        }
        public string GetAmountToString() => $"x{_amount}";
        public MaterialName GetMaterialName() => _name;

        public bool Actualize(float deltaTime)
        {
            _lifeTime += deltaTime;

            return _lifeTime > MAX_LIFE_TIME; //devuelve true si debe ser destruido
        }
        public float GetAlpha() //interpolacion lineal del tiempo de vida
        {
            float normalizedVal = _lifeTime / MAX_LIFE_TIME; // rango[0,1] :)

            return (1 - normalizedVal) + normalizedVal * 0.3f; // rango [1, 0´3]
        }
    }


}
