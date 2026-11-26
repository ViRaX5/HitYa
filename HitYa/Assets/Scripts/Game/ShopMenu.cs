using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Updates the bat cards and the buy or equip button in the Shop scene.
public class ShopMenu : MonoBehaviour
{
    [SerializeField] TMP_Text balanceLabel;
    [SerializeField] TMP_Text selectedRarityLabel;
    [SerializeField] TMP_Text batNameLabel;
    [SerializeField] TMP_Text descriptionLabel;
    [SerializeField] TMP_Text actionLabel;
    [SerializeField] TMP_Text dealOriginalPriceLabel;
    [SerializeField] TMP_Text dealPriceLabel;
    [SerializeField] Button actionButton;
    [SerializeField] Image actionFace;
    [SerializeField] Image[] cardHighlights;
    [SerializeField] Image[] cardFaces;
    [SerializeField] Image[] rarityBadges;
    [SerializeField] Image[] priceFaces;
    [SerializeField] Image[] priceIcons;
    [SerializeField] TMP_Text[] priceLabels;

    private int selectedBat;
    private Vector2[] priceLabelPositions;
    private static readonly Color32[] rarityColors =
    {
        new Color32(231, 237, 246, 255),
        new Color32(201, 233, 255, 255),
        new Color32(255, 212, 94, 255)
    };

    void Start()
    {
        priceLabelPositions = new Vector2[BatCatalog.Count];
        for (int bat = 0; bat < BatCatalog.Count; bat++)
        {
            priceLabelPositions[bat] = priceLabels[bat].rectTransform.anchoredPosition;
            BatRarity rarity = BatCatalog.Rarity(bat);
            Image badge = rarityBadges[bat];
            badge.color = rarityColors[(int)rarity];
            badge.GetComponentInChildren<TMP_Text>().text = rarity.ToString().ToUpperInvariant();
        }

        int equipped = BatCatalog.Equipped;
        SelectBat(equipped >= 0 && equipped < BatCatalog.Count && BatCatalog.Owns(equipped)
            ? equipped : BatCatalog.ClassicWood);
    }

    public void SelectBat(int bat)
    {
        if (bat < 0 || bat >= BatCatalog.Count) return;

        selectedBat = bat;
        Show();
    }

    public void UseSelectedBat()
    {
        if (Services.Save == null || selectedBat == BatCatalog.Equipped) return;

        if (BatCatalog.Owns(selectedBat))
        {
            Services.Save.SetInt(BatCatalog.EquippedKey, selectedBat);
        }
        else
        {
            Services.Coins?.TryPurchase(BatCatalog.Price(selectedBat), BatCatalog.OwnershipKey(selectedBat));
        }

        Show();
    }

    void Show()
    {
        ICoinService coins = Services.Coins;
        int balance = coins?.Balance ?? 0;
        int equipped = BatCatalog.Equipped;
        balanceLabel.text = balance.ToString("N0");
        selectedRarityLabel.text = BatCatalog.Rarity(selectedBat).ToString().ToUpperInvariant() + " BAT";
        batNameLabel.text = BatCatalog.Name(selectedBat);
        descriptionLabel.text = BatCatalog.Description(selectedBat);
        int dealPrice = BatCatalog.Price(BatCatalog.GoldenSlugger);
        dealOriginalPriceLabel.text = dealPrice > 0
            ? "<s>" + BatCatalog.RegularPrice(BatCatalog.GoldenSlugger).ToString("N0") + "</s>"
            : "";
        dealPriceLabel.text = dealPrice > 0 ? dealPrice.ToString("N0") : "-";

        for (int bat = 0; bat < BatCatalog.Count; bat++)
        {
            bool owns = BatCatalog.Owns(bat);
            bool selected = bat == selectedBat;
            int batPrice = BatCatalog.Price(bat);
            cardHighlights[bat].enabled = selected;
            cardFaces[bat].color = selected ? new Color32(255, 249, 222, 255) : Color.white;
            priceIcons[bat].enabled = !owns && batPrice > 0;
            TMP_Text priceLabel = priceLabels[bat];
            priceLabel.alignment = owns ? TextAlignmentOptions.Center : TextAlignmentOptions.MidlineRight;
            priceLabel.rectTransform.anchoredPosition = priceLabelPositions[owns ? BatCatalog.ClassicWood : bat];
            priceLabel.text = bat == equipped ? "EQUIPPED" : owns ? "OWNED" : batPrice > 0 ? batPrice.ToString("N0") : "-";
            priceFaces[bat].color = owns ? new Color32(28, 149, 221, 255)
                : new Color32(101, 203, 18, 255);
        }

        bool selectedIsOwned = BatCatalog.Owns(selectedBat);
        int price = BatCatalog.Price(selectedBat);
        if (selectedBat == equipped)
            actionLabel.text = "EQUIPPED";
        else if (selectedIsOwned)
            actionLabel.text = "EQUIP";
        else if (price <= 0 || coins == null)
            actionLabel.text = "UNAVAILABLE";
        else if (balance < price)
            actionLabel.text = "NEED " + (price - balance).ToString("N0") + " COINS";
        else
            actionLabel.text = "BUY " + price.ToString("N0");

        actionButton.interactable = Services.Save != null && selectedBat != equipped &&
            (selectedIsOwned || (price > 0 && coins != null && balance >= price));
        actionFace.color = actionButton.interactable ? new Color32(101, 203, 18, 255) : new Color32(164, 169, 188, 255);
    }
}
