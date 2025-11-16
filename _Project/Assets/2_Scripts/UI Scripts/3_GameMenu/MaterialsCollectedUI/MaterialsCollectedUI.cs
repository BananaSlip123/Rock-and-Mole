using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class MaterialsCollectedUI : MonoBehaviour
{
    [SerializeField] GameObject[] gos_callOuts;

    TextMeshProUGUI[] txts_callOutsTexts;
    Image[] imgs_callOutsImages;
    int size;
    bool isInit = false;

    List<Message> messages = new List<Message>();

    private void OnEnable()
    {
        Init();

        GameData.RunInventory.OnMaterialsEarned += OnMaterialAdd;
        StartCoroutine(ActualizeUI());
        
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
                txts_callOutsTexts[idx].text = messages[i].GetMessage();
                float alpha = messages[i].GetAlpha();
                txts_callOutsTexts[idx].alpha = alpha;
                imgs_callOutsImages[idx].color = new Color(0,0,0,alpha*0.1f);

                idx++;
                if (idx >= size) break;
            }
            for (; idx < size; idx++ )
            {
                txts_callOutsTexts[idx].alpha = 0;
                imgs_callOutsImages[idx].color = new Color(0,0,0,0);
            }

            yield return new WaitForSeconds(deltaTime);
        }
    }
    void Init()
    {
        if (isInit) return;

        size = gos_callOuts.Length;
        txts_callOutsTexts = new TextMeshProUGUI[size];
        imgs_callOutsImages = new Image[size];

        for (int i = 0; i < size; i++)
        {
            txts_callOutsTexts[i] = gos_callOuts[i].GetComponentInChildren<TextMeshProUGUI>();
            imgs_callOutsImages[i] = gos_callOuts[i].GetComponent<Image>();
        }

        isInit = true;
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
        public string GetMessage()
        {
            return $"+{_amount} de {GameData.MaterialName2String(_name)}";
        }
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
