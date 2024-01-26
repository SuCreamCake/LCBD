using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro; // TextMeshPro�� ����ϱ� ���� �߰�
using System.IO;

public class ShopManager : MonoBehaviour
{
    List<string>[] Equipment = new List<string>[5]; // 장비 템 저장
    List<string>[] Consumable = new List<string>[5]; // 소비템 저장
    List<string> SellList = new List<string>();

    public TMP_Text CountNumText; // 수량 텍스트
    public Button increaseButton; // 수량 증가 버튼
    public Button decreaseButton; // 수량 감소 버튼
    public TMP_Text TotalPriceText; // 총 가격 텍스트
    public GameObject ItemInfoPrefab; // 프리팹을 저장할 변수
    public GameObject ItemImage; //미리보기 이미지
    public GameObject ItemDetail; //미리보기 세부사항

    private int Count = 1; // 선택한 물품 갯수
    private int CurrentMoney = 0; //현재 선택 중인 물건의 가격
    private int MaxCount = 55; // 최대 갯수
    private GameObject[] shopItemPoints;
    private GameObject inventoryObject;

    private WeaponInventory weaponInventory;
    private ItemInventory itemInventory;
    private Body_Inventory bodyInventory;

    private string[] BodyName;
    private string[] WeaponName;
    private string[] UseName;
    private int[] UseCount;
    private int UseItemCount;
    private int CurrentItemPoint;

    int BodyCounts = 0;
    int WeaponCounts = 0;
    int UseCounts = 0;

    // 클래스 내부에 멤버 변수로 선언될 전역 변수들
    GameObject[] prefabs1;
    GameObject[] prefabs2;
    GameObject[] prefabs3;

    public void LoadResources(GameObject[] prefabs)
    {
        // 프리팹의 이름을 디버그 로그에 작성합니다.
        foreach (GameObject prefab in prefabs)
        {
            // 프리팹 컴포넌트에서 스크립트를 가져옵니다.
            MonoBehaviour[] scripts = prefab.GetComponents<Item>();

            // 각 스크립트에 대해 isRank 변수를 0으로 초기화합니다.
            bool isEquipment = false;
            int whatAge = 0;
            // 스크립트에 해당 랭크 단어가 포함되어 있는지 확인합니다.
            foreach (Item script in scripts)
            {
                Debug.Log(script.Rank + " " + script.name);

                if (script.item_type.ToString().Contains("Hand_Parts") || script.item_type.ToString().Contains("Body_Parts"))
                {
                    isEquipment = true;
                    if (script.item_type.ToString().Contains("Baby"))
                    {
                        whatAge = 0;
                    }
                    else if (script.item_type.ToString().Contains("Child"))
                    {
                        whatAge = 1;
                    }
                    else if (script.item_type.ToString().Contains("Young"))
                    {
                        whatAge = 2;
                    }
                    else if (script.item_type.ToString().Contains("Adult"))
                    {
                        whatAge = 3;
                    }
                    else if (script.item_type.ToString().Contains("Old"))
                    {
                        whatAge = 4;
                    }
                    else if (script.item_type.ToString().Contains("All"))
                    {
                        whatAge = 5;
                    }
                    break;
                }
                if (script.item_type.ToString().Contains("Potion_Parts"))
                {
                    isEquipment = false;
                    if (script.item_type.ToString().Contains("Baby"))
                    {
                        whatAge = 0;
                    }
                    else if (script.item_type.ToString().Contains("Child"))
                    {
                        whatAge = 1;
                    }
                    else if (script.item_type.ToString().Contains("Young"))
                    {
                        whatAge = 2;
                    }
                    else if (script.item_type.ToString().Contains("Adult"))
                    {
                        whatAge = 3;
                    }
                    else if (script.item_type.ToString().Contains("Old"))
                    {
                        whatAge = 4;
                    }
                    else if (script.item_type.ToString().Contains("All"))
                    {
                        whatAge = 5;
                    }
                    break;
                }
            }

            // 해당 프리팹의 랭크에 따라 ItemList에 파일 이름을 추가합니다.
            string fileName = prefab.name; // 파일 이름 예시
            if (isEquipment)
            {
                Equipment[whatAge].Add(fileName);
            }
            else
            {
                Consumable[whatAge].Add(fileName);
            }
        }
    }

    private void OnDisable()
    {
        foreach (GameObject itemPoint in shopItemPoints)
        {
            foreach (Transform child in itemPoint.transform)
            {
                Destroy(child.gameObject); // 자식 오브젝트 삭제
            }
        }
        CurrentItemPoint = 999;
        Image image = ItemImage.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = null;
        }
        //Image image = ItemImage.GetComponent<Image>();
        //image.sprite = sprite 가져오기;
        //아이템 세부사항 글 추가
        //TextMeshProUGUI ItemDetailText = ItemDetail.GetComponent<TextMeshProUGUI>();
        //ItemDetailText.text = string;
        MaxCount = 99;
        Debug.Log("초기화");
        int Money = 0;
        CurrentItem(Money);
    }


    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            Equipment[i] = new List<string>();
            Consumable[i] = new List<string>();
        }
        prefabs1 = null;
        prefabs2 = null;
        prefabs3 = null;
        // 리소스 폴더의 모든 프리팹을 로드합니다.
        prefabs1 = Resources.LoadAll<GameObject>("ItemPrefab/Body_Parts_Prefab");
        // 리소스 폴더의 모든 프리팹을 로드합니다.
        prefabs2 = Resources.LoadAll<GameObject>("ItemPrefab/Hands_Parts_Prefab");
        // 리소스 폴더의 모든 프리팹을 로드합니다.
        prefabs3 = Resources.LoadAll<GameObject>("ItemPrefab/Postion_Parts_Prefab");
        LoadResources(prefabs1);
        LoadResources(prefabs2);
        LoadResources(prefabs3);
    }

    private void Start()
    {
        // TextMeshPro Text 컴포넌트가 실행될 때 화면에 표시되도록 설정
        UpdateNumberText();
        // 버튼 이벤트 등록
        increaseButton.onClick.AddListener(IncreaseNumber);
        decreaseButton.onClick.AddListener(DecreaseNumber);
    }

    public void IncreaseNumber()
    {
        if (Count < MaxCount)
        {
            // 수량 증가 버튼을 누를 때마다 1씩 증가
            Count++;
            UpdateNumberText();
        }
    }

    public void DecreaseNumber()
    {
        // 수량 감소 버튼을 누를 때마다 1씩 감소 (0 미만으로는 내려가지 않도록 조건 처리)
        if (Count > 1)
        {
            Count--;
            UpdateNumberText();
        }
    }

    private void UpdateNumberText()
    {
        // 수량 텍스트 업데이트
        CountNumText.text = Count.ToString();
        Total();
    }

    private void Total()
    {
        if (TotalPriceText != null)
        {
            int a = CurrentMoney * Count;
            // 텍스트를 변경합니다.
            TotalPriceText.text = a.ToString() + "G";
        }
    }

    public void TotalButton()
    {
        int money = CurrentMoney * Count;
        bool BuyOrSell = false;
        if (BuyOrSell)
        {
            //가격 만큼 플레이어의 스크립트에서 돈 빼기
            // 플레이어의 주위에 물건 소환
            // 새로운 오브젝트를 만들어서 SpriteRenderer 스크립트 넣고
            // 해당 물건의 이름으로 스크립트를 찾아서 새로운 오브젝트에게 넣기
            // 그래서 Sprite를 변경
            // 프리팹 만들어주면 에셋 폴더에서 프리팹 위치 지정해서 그 이름 가지면 생성
        }
        else
        {
            //가격 만큼 플레이어의 스크립트에서 돈 빼기
            //인벤토리 스크립트에 접근하여 ClearSlot
            if (CurrentItemPoint == 999)
            {
                Debug.Log("상품이 선택되지 않았습니다.");
            } else
            {
                int totalItemCount = BodyCounts + WeaponCounts + UseCounts;
                int arrayIndex = CurrentItemPoint;
                int wordIndex;
                Debug.Log(arrayIndex);
                if (arrayIndex < BodyCounts)
                {
                    wordIndex = arrayIndex;
                    Debug.Log($"itemPoint는 BodyName 배열의 {wordIndex}번째 단어입니다.");
                    // BodySlots[wordIndex] 위치의 스크립트의 ClearSlot 메소드 실행
                    bodyInventory.BodySlots[wordIndex].ClearSlot();
                }
                else if (arrayIndex < BodyCounts + WeaponCounts)
                {
                    wordIndex = arrayIndex - BodyCounts;
                    Debug.Log($"itemPoint는 WeaponName 배열의 {wordIndex}번째 단어입니다.");
                    // BodySlots[wordIndex] 위치의 스크립트의 ClearSlot 메소드 실행
                    weaponInventory.Weaponslots[wordIndex].ClearSlot();
                }
                else
                {
                    wordIndex = arrayIndex - BodyCounts - WeaponCounts;
                    Debug.Log($"itemPoint는 UseName 배열의 {wordIndex}번째 단어입니다.");
                    // UseSlots[wordIndex] 위치의 스크립트의 ClearSlot 메소드 실행
                    //itemInventory.Itemslots[wordIndex].ClearSlot();
                    itemInventory.DecreaseItemCount(wordIndex, Count); // 아이템 개수 감소
                }
                OnDisable();
                SellItem();
                Count = 1;
                UpdateNumberText();
            }
        }
    }

    public void FindShopList(string name, int stageNum)
    {
        int index = 0;
        switch (name)
        {
            case "장착메뉴":
                index = 0;
                break;

            case "소비메뉴":
                index = 1;
                break;

            case "판매메뉴":
                index = 2;
                break;
            default:
                Debug.Log("잘못된 메뉴 이름입니다.");
                break;
        }
        // ShopItemPoint 태그를 가진 모든 오브젝트를 배열로 가져옵니다.
        shopItemPoints = GameObject.FindGameObjectsWithTag("ShopItemPoint");

        //실험을 위한 장비템으로 고정, 현재 stageNum 또한 스테이지가 아니여서 0으로 고정됨
        //index = 1;
        Debug.Log("현재 스테이지 : " + stageNum);
        if (index == 0)
        {
            for (int i = 0; i < Equipment[stageNum].Count; i++)
            {
                AddScriptToItemPoint(Equipment[stageNum][i], i, true);
            }
        }
        else if (index == 1)
        {
            for (int i = 0; i < Consumable[stageNum].Count; i++)
            {
                AddScriptToItemPoint(Consumable[stageNum][i], i, true);
            }
        }
        else
        {
            //판매용으로
            SellItem();
        }
    }

    private void SellItem()
    {
        //판매 관련
        inventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        if (inventoryObject != null)
        {
            // 스크립트에 접근 성공한 경우
            weaponInventory = inventoryObject.GetComponentInChildren<WeaponInventory>();
            itemInventory = inventoryObject.GetComponentInChildren<ItemInventory>();
            bodyInventory = inventoryObject.GetComponentInChildren<Body_Inventory>();
        }
        else
        {
            // 스크립트에 접근 실패한 경우
            Debug.Log("inventoryObject에 접근할 수 없습니다.");
        }
        BodyName = bodyInventory != null ? bodyInventory.GetAllName() : new string[0];
        WeaponName = weaponInventory != null ? weaponInventory.GetAllName() : new string[0];
        UseName = itemInventory != null ? itemInventory.GetAllName() : new string[0];
        UseCount = itemInventory != null ? itemInventory.GetAllItemCount() : new int[0];
        Debug.Log("BodyName 배열 크기: " + BodyName.Length);
        Debug.Log("WeaponName 배열 크기: " + WeaponName.Length);
        Debug.Log("UseName 배열 크기: " + UseName.Length);
        Debug.Log("UseCount 배열 크기: " + UseCount.Length);

        int itemPointCount = 0;
        UseItemCount = 0;
        BodyCounts = 0;
        WeaponCounts = 0;
        UseCounts = 0;
        for (int i = 0; i < BodyName.Length; i++)
        {
            if (string.IsNullOrEmpty(BodyName[i]))
            {
                continue;
            }

            AddScriptToItemPoint(BodyName[i], itemPointCount, false);
            itemPointCount++;
            BodyCounts++;
        }

        for (int i = 0; i < WeaponName.Length; i++)
        {
            if (string.IsNullOrEmpty(WeaponName[i]))
            {
                continue;
            }

            AddScriptToItemPoint(WeaponName[i], itemPointCount, false);
            itemPointCount++;
            WeaponCounts++;
        }

        for (int i = 0; i < UseName.Length; i++)
        {
            if (string.IsNullOrEmpty(UseName[i]))
            {
                continue;
            }

            AddScriptToItemPoint(UseName[i], itemPointCount, false);
            itemPointCount++;
            UseItemCount++;
            UseCounts++;
        }

    }


    private void AddScriptToItemPoint(string scriptName, int itemPointCount, bool Buy)
    {
        string randomName = scriptName; // RandomList의 요소의 이름을 가져옵니다
        Debug.Log(randomName);
        GameObject matchingPrefab = null;

        // prefab1 배열에서 이름이 같은 GameObject를 찾습니다
        matchingPrefab = System.Array.Find(prefabs1, obj => obj.name == randomName);

        if (matchingPrefab == null)
        {
            // prefab2 배열에서 이름이 같은 GameObject를 찾습니다
            matchingPrefab = System.Array.Find(prefabs2, obj => obj.name == randomName);
        }

        if (matchingPrefab == null)
        {
            // prefab3 배열에서 이름이 같은 GameObject를 찾습니다
            matchingPrefab = System.Array.Find(prefabs3, obj => obj.name == randomName);
        }

        GameObject itemInfoObject = Instantiate(ItemInfoPrefab, shopItemPoints[itemPointCount].transform); // ItemInfoPrefab을 인스턴스화하여 itemPoint의 자식으로 추가

        GameObject matchingObject = Instantiate(matchingPrefab); // matchingPrefab을 인스턴스화

        matchingObject.transform.SetParent(itemInfoObject.transform); // matchingObject를 itemInfoObject의 자식으로 설정
        matchingObject.SetActive(false); // matchingObject를 비활성화
        // 오브젝트의 이름을 변경합니다.
        itemInfoObject.name = scriptName;

        // Body_Parts_Item, Hand_Parts_Item, Potion_Parts_Item을 미리 선언합니다.
        Body_Parts_Item Body_Parts_Item = null;
        Hand_Parts_Item Hand_Parts_Item = null;
        Potion_Parts_Item Potion_Parts_Item = null;

        int price;
        int itemNumber;
        string itemName;
        string rank;
        int max_count;

        Body_Parts_Item = matchingObject.GetComponent<Body_Parts_Item>();
        if (Body_Parts_Item == null)
        {
            Hand_Parts_Item = matchingObject.GetComponent<Hand_Parts_Item>();
            if (Hand_Parts_Item == null)
            {
                Potion_Parts_Item = matchingObject.GetComponent<Potion_Parts_Item>();
                // 필드에 접근하여 값 가져오기
                price = Potion_Parts_Item.Price;
                itemNumber = Potion_Parts_Item.item_number;
                itemName = Potion_Parts_Item.item_Name;
                rank = (Potion_Parts_Item.Rank).ToString();
                if (!Buy)
                    max_count = UseCount[UseItemCount];
                else
                    max_count = Potion_Parts_Item.max_count;
                //sprite도 추가 필요
                //
                //
                // 자식 오브젝트의 TextMeshPro 컴포넌트 가져오기
                TextMeshProUGUI itemNameText = itemInfoObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI priceText = itemInfoObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                // TextMeshPro에 값 할당
                itemNameText.text = itemName;
                priceText.text = price.ToString() + " G";
                //Debug.Log("Price: " + price);
                //Debug.Log("Item Number: " + itemNumber);
                //Debug.Log("Item Name: " + itemName);
                //Debug.Log("Item Rank: " + rank);
                //Debug.Log("Item max_count: " + max_count);
            }
            else
            {
                // 필드에 접근하여 값 가져오기
                price = Hand_Parts_Item.Price;
                itemNumber = Hand_Parts_Item.item_number;
                itemName = Hand_Parts_Item.item_Name;
                rank = (Hand_Parts_Item.Rank).ToString();
                max_count = Hand_Parts_Item.max_count;
                //sprite도 추가 필요
                //
                //
                // 자식 오브젝트의 TextMeshPro 컴포넌트 가져오기
                TextMeshProUGUI itemNameText = itemInfoObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI priceText = itemInfoObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                // TextMeshPro에 값 할당
                itemNameText.text = itemName;
                priceText.text = price.ToString() + " G";
            }
        }
        else
        {
            // 필드에 접근하여 값 가져오기
            price = Body_Parts_Item.Price;
            itemNumber = Body_Parts_Item.item_number;
            itemName = Body_Parts_Item.item_Name;
            rank = (Body_Parts_Item.Rank).ToString();
            max_count = Body_Parts_Item.max_count;
            //sprite도 추가 필요, detail
            //
            //
            // 자식 오브젝트의 TextMeshPro 컴포넌트 가져오기
            TextMeshProUGUI itemNameText = itemInfoObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI priceText = itemInfoObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            // TextMeshPro에 값 할당
            itemNameText.text = itemName;
            priceText.text = price.ToString() + " G";
        }

        Image itemImage = itemInfoObject.GetComponent<Image>(); // itemInfoObject의 Image 컴포넌트 가져오기
        SpriteRenderer matchingSpriteRenderer = matchingObject.GetComponent<SpriteRenderer>(); // matchingObject의 SpriteRenderer 컴포넌트 가져오기

        if (itemImage != null && matchingSpriteRenderer != null)
        {
            itemImage.sprite = matchingSpriteRenderer.sprite; // itemImage의 스프라이트를 matchingSpriteRenderer의 스프라이트로 변경
        }
        else
        {
            Debug.LogWarning("itemImage 또는 matchingSpriteRenderer가 null입니다."); // 필요한 컴포넌트가 없는 경우 경고 메시지 출력
        }

        Image clickedImage = itemInfoObject.GetComponent<Image>();
        Button button = itemInfoObject.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                if (ItemImage != null && clickedImage != null)
                {
                    Image image = ItemImage.GetComponent<Image>();
                    if (image != null)
                    {
                        image.sprite = clickedImage.sprite;
                    }
                }
                    //Image image = ItemImage.GetComponent<Image>();
                    //image.sprite = sprite 가져오기;
                    //아이템 세부사항 글 추가
                    //TextMeshProUGUI ItemDetailText = ItemDetail.GetComponent<TextMeshProUGUI>();
                    //ItemDetailText.text = string;
                    // 클릭 이벤트 처리 로직을 여기에 작성합니다.
                    CurrentItemPoint = itemPointCount;
                MaxCount = max_count;
                Debug.Log("ItemInfoObject가 클릭되었습니다.");
                int Money = price;
                CurrentItem(Money);
            });
        }
    }

    public void CurrentItem(int ItemMoney)
    {
        Count = 1;
        CurrentMoney = ItemMoney;
        UpdateNumberText();
    }
}