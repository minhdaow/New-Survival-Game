using UnityEditor;
using UnityEngine;

public class RTRPGHierarchyMenu : MonoBehaviour
{
    private static readonly string playerPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Player.prefab";
    private static readonly string soldierPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Soldier.prefab";
    private static readonly string specialistPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Specialist.prefab";
    private static readonly string brutePrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Brute.prefab";
    private static readonly string archerPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Archer.prefab";
    private static readonly string followerPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Follower.prefab";
    private static readonly string vendorPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Vendor.prefab";
    private static readonly string civilianPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Civilian.prefab";
    private static readonly string shotPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Camera Shot 3RD.prefab";
    private static readonly string shot2PrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Camera Shot Topdown.prefab";
    private static readonly string invCharPrefabPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/InventoryCharacter.prefab";
    private static readonly string checkPointPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Save/Checkpoint.prefab";
    private static readonly string savePointPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Save/Save Point.prefab";
    private static readonly string bedPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Bed.prefab";
    private static readonly string carryPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Carry.prefab";
    private static readonly string chairPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Chair.prefab";
    private static readonly string chestPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Chest.prefab";
    private static readonly string digPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Dig.prefab";
    private static readonly string doorPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Door.prefab";
    private static readonly string fishPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Fish.prefab";
    private static readonly string fountainPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Fountain.prefab";
    private static readonly string gatherPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Gather.prefab";
    private static readonly string pickupPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Pickup.prefab";
    private static readonly string emotePath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/Basic/Canvas-Emote.prefab";
    private static readonly string break01Path = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Breakable_Bowl.prefab";
    private static readonly string break02Path = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Breakable_Jug.prefab";
    private static readonly string break03Path = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Breakable_Vase.prefab";
    private static readonly string flowerPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Flower.prefab";
    private static readonly string hudPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Player-HUD.prefab";
    private static readonly string menuPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Main Menu.prefab";
    private static readonly string menuCustPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Main Menu Customization.prefab";
    private static readonly string indicatorPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Canvas-IndicatorAwareness.prefab";
    private static readonly string lightPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/General/Directional Light.prefab";
    private static readonly string volumePath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/General/Global Volume.prefab";
    private static readonly string managerPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/General/Enemy Manager.prefab";
    private static readonly string fadePath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Canvas-FadeIn.prefab";
    private static readonly string bossRoomPath = "Assets/Plugins/RVRGaming/RapidTemplate/Prefabs/RPG/Boss Room.prefab";



    [MenuItem("GameObject/Rapid Template/RPG/Characters/Player", false, 10)]
    private static void AddPlayerPrefab()
    {
        AddPrefabToScene(playerPrefabPath, "Player");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Soldier", false, 11)]
    private static void AddSoldierPrefab()
    {
        AddPrefabToScene(soldierPrefabPath, "Soldier");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Specialist", false, 11)]
    private static void AddSpecialistPrefab()
    {
        AddPrefabToScene(specialistPrefabPath, "Specialist");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Brute", false, 11)]
    private static void AddBrutePrefab()
    {
        AddPrefabToScene(brutePrefabPath, "Brute");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Archer", false, 11)]
    private static void AddArcherPrefab()
    {
        AddPrefabToScene(archerPrefabPath, "Archer");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Follower", false, 12)]
    private static void AddFollowerPrefab()
    {
        AddPrefabToScene(followerPrefabPath, "Follower");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Vendor", false, 13)]
    private static void AddVendorPrefab()
    {
        AddPrefabToScene(vendorPrefabPath, "Vendor");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Civilian", false, 14)]
    private static void AddCivilianPrefab()
    {
        AddPrefabToScene(civilianPrefabPath, "Civilian");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Boss Room", false, 14)]
    private static void AddBossRoomPrefab()
    {
        AddPrefabToScene(bossRoomPath, "Boss Room");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Camera/Shot 3RD", false, 14)]
    private static void AddShotPrefab()
    {
        AddPrefabToScene(shotPrefabPath, "Shot 3RD");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Camera/Shot Topdown", false, 14)]
    private static void AddShot2Prefab()
    {
        AddPrefabToScene(shot2PrefabPath, "Shot Topdown");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Inventory/Inventory Character", false, 14)]
    private static void AddInvCharPrefab()
    {
        AddPrefabToScene(invCharPrefabPath, "Inventory Character");
    }

    [MenuItem("GameObject/Rapid Template/Save/Checkpoint", false, 14)]
    private static void AddcheckPointPrefab()
    {
        AddPrefabToScene(checkPointPath, "Checkpoint");
    }

    [MenuItem("GameObject/Rapid Template/Save/Save Point", false, 14)]
    private static void AddSavePointPrefab()
    {
        AddPrefabToScene(savePointPath, "Save Point");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Bed", false, 15)]
    private static void AddBedPrefab()
    {
        AddPrefabToScene(bedPath, "Bed");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Carry", false, 16)]
    private static void AddCarryPrefab()
    {
        AddPrefabToScene(carryPath, "Carry");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Chair", false, 17)]
    private static void AddChairPrefab()
    {
        AddPrefabToScene(chairPath, "Chair");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Chest", false, 18)]
    private static void AddChestPrefab()
    {
        AddPrefabToScene(chestPath, "Chest");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Dig", false, 19)]
    private static void AddDigPrefab()
    {
        AddPrefabToScene(digPath, "Dig");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Door", false, 20)]
    private static void AddDoorPrefab()
    {
        AddPrefabToScene(doorPath, "Door");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Fish", false, 21)]
    private static void AddFishPrefab()
    {
        AddPrefabToScene(fishPath, "Fish");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Fountain", false, 22)]
    private static void AddFountainPrefab()
    {
        AddPrefabToScene(fountainPath, "Fountain");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Gather", false, 23)]
    private static void AddGatherPrefab()
    {
        AddPrefabToScene(gatherPath, "Gather");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Pickup", false, 24)]
    private static void AddPickupPrefab()
    {
        AddPrefabToScene(pickupPath, "Pickup");
    }

    [MenuItem("GameObject/Rapid Template/Basic/Emote", false, 25)]
    private static void AddEmotePrefab()
    {
        AddPrefabToScene(emotePath, "Canvas-Emote");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Props/Breakable Bowl", false, 25)]
    private static void AddBreak01Prefab()
    {
        AddPrefabToScene(break01Path, "Breakable Bowl");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Props/Breakable Jug", false, 25)]
    private static void AddBreak02Prefab()
    {
        AddPrefabToScene(break02Path, "Breakable Jug");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Props/Breakable Vase", false, 25)]
    private static void AddBreak03Prefab()
    {
        AddPrefabToScene(break03Path, "Breakable Vase");
    }

    [MenuItem("GameObject/Rapid Template/RPG/Props/Flower", false, 25)]
    private static void AddFlowerPrefab()
    {
        AddPrefabToScene(flowerPath, "Flower");
    }

    [MenuItem("GameObject/Rapid Template/RPG/UI/HUD", false, 25)]
    private static void AddHudPrefab()
    {
        AddPrefabToScene(hudPath, "HUD");
    }

    [MenuItem("GameObject/Rapid Template/RPG/UI/Main Menu", false, 25)]
    private static void AddMenuPrefab()
    {
        AddPrefabToScene(menuPath, "Main Menu");
    }

    [MenuItem("GameObject/Rapid Template/RPG/UI/Main Menu Customization", false, 25)]
    private static void AddMenuCustPrefab()
    {
        AddPrefabToScene(menuCustPath, "Main Menu Customization");
    }

    [MenuItem("GameObject/Rapid Template/RPG/UI/Fade In", false, 25)]
    private static void AddFadePrefab()
    {
        AddPrefabToScene(fadePath, "Fade In");
    }

    [MenuItem("GameObject/Rapid Template/RPG/UI/Indicator", false, 25)]
    private static void AddIndicatorPrefab()
    {
        AddPrefabToScene(indicatorPath, "Indicator");
    }

    [MenuItem("GameObject/Rapid Template/General/Directional Light", false, 25)]
    private static void AddLightPrefab()
    {
        AddPrefabToScene(lightPath, "Directional Light");
    }

    [MenuItem("GameObject/Rapid Template/General/Global Volume", false, 25)]
    private static void AddVolumetPrefab()
    {
        AddPrefabToScene(volumePath, "Global Volume");
    }

    [MenuItem("GameObject/Rapid Template/General/Enemy Manager", false, 25)]
    private static void AddManagerPrefab()
    {
        AddPrefabToScene(managerPath, "Enemy Manager");
    }

    private static void AddPrefabToScene(string prefabPath, string prefabName)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        if (prefab == null)
        {
            Debug.LogError($"Prefab '{prefabName}' not found at path: {prefabPath}");
            return;
        }

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        Undo.RegisterCreatedObjectUndo(instance, $"Create {prefabName}");
        Selection.activeObject = instance;
    }

    [MenuItem("GameObject/Rapid Template/RPG/Characters/Player", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Soldier", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Specialist", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Brute", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Archer", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Follower", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Vendor", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Civilian", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Characters/Boss Room", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Camera/Shot 3RD", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Camera/Shot Topdown", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Inventory/Inventory Character", true)]
    [MenuItem("GameObject/Rapid Template/Save/Checkpoint", true)]
    [MenuItem("GameObject/Rapid Template/Save/Save Point", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Bed", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Carry", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Chair", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Chest", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Dig", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Door", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Emote", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Fish", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Fountain", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Gather", true)]
    [MenuItem("GameObject/Rapid Template/Basic/Pickup", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Props/Breakable Bowl", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Props/Breakable Jug", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Props/Breakable Vase", true)]
    [MenuItem("GameObject/Rapid Template/RPG/Props/Flower", true)]
    [MenuItem("GameObject/Rapid Template/RPG/UI/HUD", true)]
    [MenuItem("GameObject/Rapid Template/RPG/UI/Main Menu", true)]
    [MenuItem("GameObject/Rapid Template/RPG/UI/Main Menu Customization", true)]
    [MenuItem("GameObject/Rapid Template/RPG/UI/Indicator", true)]
    [MenuItem("GameObject/Rapid Template/RPG/UI/Fade In", true)]

    private static bool ValidateAddPrefab()
    {
        return true;
    }
}
