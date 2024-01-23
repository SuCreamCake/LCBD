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

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            Equipment[i] = new List<string>();
            Consumable[i] = new List<string>();
        }

        string folderPath = "Assets/Scripts/Item/"; // 검색할 폴더 경로를 지정해주세요.
        string[] csFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories);
        foreach (string csFile in csFiles)
        {
            string[] fileLines = File.ReadAllLines(csFile);
            string fileName = Path.GetFileName(csFile);

            // 파일 내에서 CS파일 찾기
            int hasRankFunction = 0;
            foreach (string line in fileLines)
            {
                if (line.Contains(": Body_Parts_Item") || line.Contains(": Hand_Parts_Item"))
                {
                    hasRankFunction = 1;
                    break;
                }
                else if (line.Contains(": Potion_Parts_Item"))
                {
                    hasRankFunction = 2;
                    break;
                }
            }

            if (hasRankFunction == 1)
            {
                bool isAdded = false;
                foreach (string line in fileLines) // line 변수를 정의하여 문제 해결
                {
                    if (line.Contains("Drop_age.Baby"))
                    {
                        //Debug.Log(fileName + " 장비 / Baby");
                        // Drop_age.Baby가 있으면 Equipment의 [0][]에 추가
                        Equipment[0].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Child"))
                    {
                        //Debug.Log(fileName + " 장비 / Child");
                        // Drop_age.Child이면 Equipment[1][]에 추가
                        Equipment[1].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Young"))
                    {
                        //Debug.Log(fileName + " 장비 / Young");
                        // Drop_age.Young이면 Equipment[2][]에 추가
                        Equipment[2].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Adult"))
                    {
                        //Debug.Log(fileName + " 장비 / Adult");
                        // Drop_age.Adult이면 Equipment[3][]에 추가
                        Equipment[3].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Old"))
                    {
                        //Debug.Log(fileName + " 장비 / Old");
                        // Drop_age.Old이면 Equipment[4][]에 추가
                        Equipment[4].Add(fileName);
                        isAdded = true;
                    }
                }
                // 이미 추가한 경우에는 모든 Equipment 배열에 추가하지 않도록 수정
                if (!isAdded)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Equipment[i].Add(fileName);
                    }
                }
            }
            else if (hasRankFunction == 2)
            {
                bool isAdded = false;
                foreach (string line in fileLines)
                {
                    if (line.Contains("Drop_age.Baby"))
                    {
                        //Debug.Log(fileName + " 소비 / Baby");
                        Consumable[0].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Child"))
                    {
                        //Debug.Log(fileName + " 소비 / Baby");
                        Consumable[1].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Young"))
                    {
                        //Debug.Log(fileName + " 소비 / Young");
                        Consumable[2].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Adult"))
                    {
                        //Debug.Log(fileName + " 소비 / Adult");
                        Consumable[3].Add(fileName);
                        isAdded = true;
                    }
                    else if (line.Contains("Drop_age.Old"))
                    {
                        //Debug.Log(fileName + " 소비 / Old");
                        Consumable[4].Add(fileName);
                        isAdded = true;
                    }
                }
                // 이미 추가한 경우에는 모든 Consumable 배열에 추가하지 않도록 수정
                if (!isAdded)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Consumable[i].Add(fileName);
                    }
                }
            }
        }
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
            int totalItemCount = BodyName.Length + WeaponName.Length + UseName.Length;
            int arrayIndex = CurrentItemPoint % totalItemCount;
            int wordIndex;

            if (arrayIndex < BodyName.Length)
            {
                wordIndex = arrayIndex;
                Debug.Log($"itemPoint는 BodyName 배열의 {wordIndex}번째 단어입니다.");
                // BodySlots[wordIndex] 위치의 스크립트의 ClearSlot 메소드 실행
                bodyInventory.BodySlots[wordIndex].ClearSlot();
            }
            else if (arrayIndex < BodyName.Length + WeaponName.Length)
            {
                wordIndex = arrayIndex - BodyName.Length;
                Debug.Log($"itemPoint는 WeaponName 배열의 {wordIndex}번째 단어입니다.");
                // BodySlots[wordIndex] 위치의 스크립트의 ClearSlot 메소드 실행
                weaponInventory.Weaponslots[wordIndex].ClearSlot();
            }
            else
            {
                wordIndex = arrayIndex - BodyName.Length - WeaponName.Length;
                Debug.Log($"itemPoint는 UseName 배열의 {wordIndex}번째 단어입니다.");
                // UseSlots[wordIndex] 위치의 스크립트의 ClearSlot 메소드 실행
                itemInventory.Itemslots[wordIndex].ClearSlot();
                itemInventory.DecreaseItemCount(wordIndex, Count); // 아이템 개수 감소
            }
            SellItem();
            Count = 1;
            UpdateNumberText();
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

        //for (int i = 0; i < Equipment.Length; i++)
        //{
        //    Debug.Log("Equipment[" + i + "]:");
        //    for (int j = 0; j < Equipment[i].Count; j++)
        //    {
        //        Debug.Log(" - " + Equipment[i][j]);
        //    }
        //}
        //
        //실험을 위한 장비템으로 고정, 현재 stageNum 또한 스테이지가 아니여서 0으로 고정됨
        index = 2;
        if (index == 0)
        {
            for (int i = 0; i < Equipment[stageNum].Count; i++)
            {
                AddScriptToItemPoint(Equipment[stageNum][i], i, true);
            }
        }
        else if(index == 1)
        {
            for (int i = 0; i < Consumable[stageNum].Count; i++)
            {
                AddScriptToItemPoint(Consumable[stageNum][i], i, true);
            }
        } else
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

        bodyInventory.GetDebug();
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

        for (int i = 0; i < BodyName.Length; i++)
        {
            if (string.IsNullOrEmpty(BodyName[i]))
            {
                continue;
            }

            AddScriptToItemPoint(BodyName[i] + ".cs", itemPointCount, false);
            itemPointCount++;
        }

        for (int i = 0; i < WeaponName.Length; i++)
        {
            if (string.IsNullOrEmpty(WeaponName[i]))
            {
                continue;
            }

            AddScriptToItemPoint(WeaponName[i] + ".cs", itemPointCount, false);
            itemPointCount++;
        }

        for (int i = 0; i < UseName.Length; i++)
        {
            if (string.IsNullOrEmpty(UseName[i]))
            {
                continue;
            }

            AddScriptToItemPoint(UseName[i] + ".cs", itemPointCount, false);
            itemPointCount++;
            UseItemCount++;
        }

    }


    private void AddScriptToItemPoint(string scriptName, int itemPointCount, bool Buy)
    {
        string[] filePaths = Directory.GetFiles("Assets/Scripts/Item", "*" + scriptName, SearchOption.AllDirectories);
        MonoScript script = null;
        if (filePaths.Length > 0)
        {
            string scriptPath = filePaths[0];
            script = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        }

        // 스크립트를 자식 오브젝트에 추가합니다.
        if (script != null)
        {
            // ItemInfoPrefab을 인스턴스화하여 itemPoint의 자식으로 추가합니다.
            GameObject itemInfoObject = Instantiate(ItemInfoPrefab, shopItemPoints[itemPointCount].transform);

            // 스크립트를 컴포넌트로 추가합니다.
            itemInfoObject.AddComponent(script.GetClass());

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

            Body_Parts_Item = itemInfoObject.GetComponent<Body_Parts_Item>();
            if (Body_Parts_Item == null)
            {
                Hand_Parts_Item = itemInfoObject.GetComponent<Hand_Parts_Item>();
                if (Hand_Parts_Item == null)
                {
                    Potion_Parts_Item = itemInfoObject.GetComponent<Potion_Parts_Item>();
                    // 필드에 접근하여 값 가져오기
                    price = Potion_Parts_Item.Price;
                    itemNumber = Potion_Parts_Item.item_number;
                    itemName = Potion_Parts_Item.item_Name;
                    rank = (Potion_Parts_Item.Rank).ToString();
                    if(!Buy)
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
            } else
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
            //// 숍 관련 정보만 따로 분리하여 저장
            //ShopObjectInfo shopObjectInfo = itemInfoObject.GetComponent<ShopObjectInfo>();
            //if (shopObjectInfo != null)
            //{
            //    shopObjectInfo.Price = price;
            //    shopObjectInfo.ItemNumber = itemNumber;
            //    shopObjectInfo.ItemName = itemName;
            //    shopObjectInfo.Rank = rank;
            //    shopObjectInfo.MaxCount = max_count;
            //}

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
        else
        {
            Debug.Log(scriptName + " 스크립트를 찾을 수 없습니다.");
        }
    }

    public void CurrentItem(int ItemMoney) {
        Count = 1;
        CurrentMoney = ItemMoney;
        UpdateNumberText();
    }
}