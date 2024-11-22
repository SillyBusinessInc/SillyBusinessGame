using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    // field
    public int id;
    public RoomType roomType;

    // constructor
    public Room(int id_, RoomType roomType_) {
        id = id_;
        roomType = roomType_;
    }

    public bool IsStandard() {
        return roomType switch {
            RoomType.COMBAT => true,
            RoomType.PARKOUR => true,
            RoomType.MOLDORB => true,
            RoomType.WAVESURVIVAL => true,
            _ => false,
        };
    }

    // static info
    public static Dictionary<RoomType, int> RoomDistribution = new() {
        {RoomType.COMBAT, 40},
        {RoomType.PARKOUR, 30},
        {RoomType.MOLDORB, 20},
        {RoomType.WAVESURVIVAL, 10}
    };
}

public enum RoomType {
    OTHER,      // anything non specified, preferably shouldn't be used
    ENTRANCE,   // entrance room, first room where the player starts the game
    EXIT,       // exit room, last room on a certain floor
    SHOP,       // shop, where the player can buy stuff
    BONUS,      // hidden treasure room that will redirect back to the previous room
    COMBAT,     // room with a focus on combat
    PARKOUR,    // room with a focus on movement
    MOLDORB,    // room that contains mold orbs that will need to be destroyed
    WAVESURVIVAL// room that spawn several waves of enemies
}