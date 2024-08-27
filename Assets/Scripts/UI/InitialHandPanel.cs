using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InitialHandPanel : MonoBehaviour
{
    public static InitialHandPanel Instance { get; private set; }

    [SerializeField] TextMeshProUGUI PlayerText;
    [SerializeField] TextMeshProUGUI Count;
    [SerializeField] GameObject CardsLayout;
    [SerializeField] Deck[] decks;
    [SerializeField] GameObject[] hands;
    CardInfo[] ChangedCards = new CardInfo[2];

    int player, cardCount, ReadyCount;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        this.gameObject.SetActive(true);
    }
    public void Start()
    {
        cardCount = ReadyCount = player = 0;
        PlayerText.text = "Jugador 1";
        DrawTenCards();
    }
    public void DrawTenCards()
    {
        for (int i = 0; i < 10; i++)
        {
            var drawnCard = decks[(int)player].Draw();
            drawnCard.transform.SetParent(CardsLayout.transform, false);
        }
    }

    public void ReadyButton()
    {
        ReadyCount++; Debug.Log(ReadyCount);

        // Transfer the cards from the panel to the respective hand 
        for (int i = 0; i < 10; i++)
            CardsLayout.transform.GetChild(0).SetParent(hands[player].transform, false);

        //ReAdd the changed cards to the deck
        decks[(int)player].ReAddCards(ChangedCards);

        if (ReadyCount == 2)
        {
            Debug.Log($"Final Count");
            GameManager.Instance.UpdateGameState(GameState.Round);
            Destroy(this.gameObject);
        }
        //Set Next Player
        else
        {
            player++;
            PlayerText.text = "Jugador 2";
            cardCount = 0;
            DrawTenCards();
        }
    }

    public void ChangeThisCard(Card card)
    {
        if (cardCount == 2) return;
        ChangedCards[cardCount++] = card.CardInfo;
        Destroy(card.gameObject);
        decks[(int)player].Draw().transform.SetParent(CardsLayout.transform, false);
    }

    void Update()
    {
        Count.text = $"{cardCount}/2";
    }
}
