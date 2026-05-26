using HarmonyLib;
using UnityEngine;

namespace DoubleTrack.MP;

using MPAPI;
using MPAPI.Types;


public static class Bootstrap
{
    public static void Initialize()
    {
        Debug.Log("Bootstrapping DoubleTrack.MP.");
        if (!MultiplayerAPI.IsMultiplayerLoaded)
            return;

        // Set compatibility state for your mod
        MultiplayerAPI.Instance.SetModCompatibility(DoubleTrack.TrackPlacerEntry.ModEntry.Info.Id, MultiplayerCompatibility.All);

        // Apply any extra patches required for multiplayer that can't be applied in the your core mod
        Harmony harmony = new("DoubleTrack.MP");
        harmony.PatchAll();
        
        MPAPI.MultiplayerAPI.ServerStarted += MPConfig.SetupServer;
        
        
        MPAPI.MultiplayerAPI.ClientStarted +=  MPConfig.SetupClient;

        Settings.SaveSettings.AddListener(MPConfig.ConfigSendAll);
    }
}