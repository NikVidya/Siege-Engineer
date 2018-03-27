using System.Collections;
using System.Collections.Generic;

public static class Constants {
    public static readonly string[] MOVEMENT_BLOCKING_LAYERS = { "Default", "Walls" };
    public static class InputNames {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        public const string DASH = "Dash";
        public const string PICKUP = "Pickup";
        public const string REPAIR = "Repair";
        public const string PAUSE = "Pause";
        public const string UICANCEL = "UICancel";
    }

    public static class Resource {
        public enum ResourceType {
            WOOD,
            STONE,
            STEEL
        }
    }

    public static class Tags {
        public const string RESOURCE = "GameResource";
    }

    public static class PlayerPrefsKeys {
        public const string PLAYER_CURRENCY = "PlayerCurrency";
        public const string PLAYER_SCORE = "PlayerScore";
    }
}