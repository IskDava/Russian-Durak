using UnityEngine;

public class PlayersCardsManagment : MonoBehaviour
{
    public GameObject cardPrefab, opponentCardPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            CardsSpawn cards_script = transform.GetChild(i).GetComponent<CardsSpawn>();
            cards_script.RenderCards(Player.players[i].cards, cardPrefab, opponentCardPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
