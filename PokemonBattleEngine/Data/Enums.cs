﻿using System;

namespace PokemonBattleEngine.Data
{
    enum Gender : byte
    {
        Male = 0x00,
        M7F1 = 0x1F, // Male 7:1 Female
        M3F1 = 0x3F, // Male 3:1 Female
        M1F1 = 0x7F, // Male 1:1 Female
        M1F3 = 0xBF, // Male 1:3 Female
        M1F7 = 0xE1, // Male 1:7 Female // Does not exist before gen 6
        Female = 0xFE,
        Genderless = 0xFF
    }
    enum PossibleTarget // Used in MoveData
    {
        Any,
        AnySurrounding,
        Self
    }
    enum Target
    {
        All,
        AllyLeft,
        AllyMiddle,
        AllyRight,
        FoeLeft,
        FoeMiddle,
        FoeRight,
        Self
    }
    enum Status
    {
        None,
        Asleep,
        BadlyPoisoned,
        Burned,
        Frozen,
        Paralyzed,
        Poisoned
    }
    enum Type
    {
        Bug,
        Dark,
        Dragon,
        Electric,
        Fighting,
        Fire,
        Flying,
        Ghost,
        Grass,
        Ground,
        Ice,
        Normal,
        Poison,
        Psychic,
        Rock,
        Steel,
        Water
    }
    enum Nature
    {
        Adamant,
        Bashful,
        Bold,
        Brave,
        Calm,
        Careful,
        Docile,
        Gentle,
        Hardy,
        Hasty,
        Impish,
        Jolly,
        Lax,
        Loney,
        Mild,
        Modest,
        Naive,
        Naughty,
        Quiet,
        Quirky,
        Rash,
        Relaxed,
        Sassy,
        Serious,
        Timid,
        MAX
    }
    enum Item
    {
        None,
        ChoiceBand,
        DeepSeaScale,
        DeepSeaTooth,
        LightBall,
        MetalPowder,
        SoulDew,
        ThickClub,
    }
    enum Ability
    {
        None,
        BadDreams,
        BattleArmor,
        Blaze,
        Guts,
        HugePower,
        Hustle,
        Imposter,
        Levitate,
        LightningRod,
        Limber,
        MarvelScale,
        Minus,
        Overgrow,
        Plus,
        PurePower,
        Rattled,
        RockHead,
        SapSipper,
        ShellArmor,
        Static,
        Swarm,
        ThickFat,
        Torrent,
    }
    enum Species
    {
        None,
        Pikachu = 25,
        Cubone = 104,
        Marowak,
        Ditto = 132,
        Azumarill = 184,
        Clamperl = 366,
        Latias = 380,
        Latios,
        Cresselia = 488,
        Darkrai = 491,
    }
    enum MoveCategory
    {
        Status,
        Physical,
        Special
    }
    enum Effect
    {
        None,
        Flinch_Hit,
        Hit,
        Lower_DEF_SPDEF_By1_Raise_ATK_SPATK_SPD_By2,
        Lower_SPDEF_By1_Hit,
        Paralyze_Hit,
        Transform,
    }
    [Flags]
    enum MoveFlags
    {
        None = 0,
        MakesContact = 1 << 0,
        AffectedByProtect = 1 << 1,
        AffectedByMagicCoat = 1 << 2,
        AffectedBySnatch = 1 << 3,
        AffectedByMirrorMove = 1 << 4,
        AffectedByKingsRock = 1 << 5
    }
    enum Move
    {
        None,
        DarkPulse,
        DragonPulse,
        HydroPump,
        Psychic,
        Retaliate,
        ShellSmash,
        Tackle,
        Thunder,
        Transform,
    }
}
