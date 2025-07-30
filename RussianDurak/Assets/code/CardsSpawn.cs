using System.Collections.Generic;
using UnityEngine;

public class CardsSpawn : MonoBehaviour
{
    public void RenderCards(List<Card> cards, GameObject cardPrefab)
    {
        int i;
        for (i = 0; i < transform.childCount && i < cards.Count; i++)
        {
            Transform child_card = transform.GetChild(i);

            SpriteRenderer sr = child_card.GetComponent<SpriteRenderer>();
            Sprite cardSprite = Resources.Load<Sprite>($"cards/{cards[i].filename}");
            sr.sprite = cardSprite;

            child_card.localScale = new Vector3(1f, 1f, 1f);
        }
        if (i != transform.childCount)
        {
            for (;i < transform.childCount; i++)
            {
                Debug.Log(i);
                Debug.Log(transform.childCount);
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        else if (i != cards.Count)
        {
            int j;
            for (j = 0;j < cards.Count - i; j++)
            {
                GameObject newcard = Instantiate(cardPrefab, transform);

                newcard.name = $"Card ({j})";

                newcard.transform.localPosition = Vector3.zero;
                if (j != 0)
                {
                    Vector3 lastCardPos = transform.GetChild(j - 1).localPosition;

                    newcard.transform.localPosition = new Vector3((float)(lastCardPos.x + 2), 0, (float)(lastCardPos.z - 0.1));
                }
                newcard.transform.localScale = new Vector3(1f, 1f, 1f);

                Sprite cardSprite = Resources.Load<Sprite>($"cards/{cards[j].filename}");
                newcard.GetComponent<SpriteRenderer>().sprite = cardSprite;
            }
            Vector3 prev = transform.localPosition;
            float error = 1f;
            if (transform.name == "OpponentCards") error = 0.75f;
            transform.localPosition = new Vector3(prev.x - j * error + error, prev.y, prev.z);
        }
    }
}
