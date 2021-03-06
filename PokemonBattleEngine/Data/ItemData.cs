﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kermalis.PokemonBattleEngine.Data
{
    public sealed class PBEItemData
    {
        /// <summary>
        /// The power <see cref="PBEMove.Fling"/> has when the user is holding this item. 0 will cause the move to fail.
        /// </summary>
        public byte FlingPower { get; }
        /// <summary>
        /// The power <see cref="PBEMove.NaturalGift"/> has when the user is holding this item. 0 will cause the move to fail.
        /// </summary>
        public byte NaturalGiftPower { get; }
        /// <summary>
        /// The type <see cref="PBEMove.NaturalGift"/> becomes when the user is holding this item.
        /// </summary>
        public PBEType NaturalGiftType { get; }

        private PBEItemData(byte flingPower = 0, byte naturalGiftPower = 0, PBEType naturalGiftType = PBEType.None)
        {
            FlingPower = flingPower;
            NaturalGiftPower = naturalGiftPower;
            NaturalGiftType = naturalGiftType;
        }

        public static ReadOnlyDictionary<PBEItem, PBEItemData> Data { get; } = new ReadOnlyDictionary<PBEItem, PBEItemData>(new Dictionary<PBEItem, PBEItemData>
        {
            { PBEItem.AbsorbBulb, new PBEItemData(30) },
            { PBEItem.AdamantOrb, new PBEItemData(60) },
            { PBEItem.AguavBerry, new PBEItemData(10, 60, PBEType.Dragon) },
            { PBEItem.AirBalloon, new PBEItemData(10) },
            { PBEItem.AmuletCoin, new PBEItemData(30) },
            { PBEItem.Antidote, new PBEItemData(30) },
            { PBEItem.ApicotBerry, new PBEItemData(10, 80, PBEType.Ground) },
            { PBEItem.ArmorFossil, new PBEItemData(100) },
            { PBEItem.AspearBerry, new PBEItemData(10, 60, PBEType.Ice) },
            { PBEItem.Awakening, new PBEItemData(30) },
            { PBEItem.BabiriBerry, new PBEItemData(10, 60, PBEType.Steel) },
            { PBEItem.BalmMushroom, new PBEItemData(30) },
            { PBEItem.BelueBerry, new PBEItemData(10, 80, PBEType.Electric) },
            { PBEItem.BerryJuice, new PBEItemData(30) },
            { PBEItem.BigMushroom, new PBEItemData(30) },
            { PBEItem.BigNugget, new PBEItemData(30) },
            { PBEItem.BigPearl, new PBEItemData(30) },
            { PBEItem.BigRoot, new PBEItemData(10) },
            { PBEItem.BindingBand, new PBEItemData(30) },
            { PBEItem.BlkApricorn, new PBEItemData() },
            { PBEItem.BlackBelt, new PBEItemData(30) },
            { PBEItem.BlackFlute, new PBEItemData(30) },
            { PBEItem.BlackGlasses, new PBEItemData(30) },
            { PBEItem.BlackSludge, new PBEItemData(30) },
            { PBEItem.BluApricorn, new PBEItemData() },
            { PBEItem.BlueFlute, new PBEItemData(30) },
            { PBEItem.BlueScarf, new PBEItemData(10) },
            { PBEItem.BlueShard, new PBEItemData(30) },
            { PBEItem.BlukBerry, new PBEItemData(10, 70, PBEType.Fire) },
            { PBEItem.BridgeMailD, new PBEItemData() },
            { PBEItem.BridgeMailM, new PBEItemData() },
            { PBEItem.BridgeMailS, new PBEItemData() },
            { PBEItem.BridgeMailT, new PBEItemData() },
            { PBEItem.BridgeMailV, new PBEItemData() },
            { PBEItem.BrightPowder, new PBEItemData(10) },
            { PBEItem.BugGem, new PBEItemData() },
            { PBEItem.BurnDrive, new PBEItemData(70) },
            { PBEItem.BurnHeal, new PBEItemData(30) },
            { PBEItem.Calcium, new PBEItemData(30) },
            { PBEItem.Carbos, new PBEItemData(30) },
            { PBEItem.Casteliacone, new PBEItemData(30) },
            { PBEItem.CellBattery, new PBEItemData(30) },
            { PBEItem.Charcoal, new PBEItemData(30) },
            { PBEItem.ChartiBerry, new PBEItemData(10, 60, PBEType.Rock) },
            { PBEItem.CheriBerry, new PBEItemData(10, 60, PBEType.Fire) },
            { PBEItem.CherishBall, new PBEItemData() },
            { PBEItem.ChestoBerry, new PBEItemData(10, 60, PBEType.Water) },
            { PBEItem.ChilanBerry, new PBEItemData(10, 60, PBEType.Normal) },
            { PBEItem.ChillDrive, new PBEItemData(70) },
            { PBEItem.ChoiceBand, new PBEItemData(10) },
            { PBEItem.ChoiceScarf, new PBEItemData(10) },
            { PBEItem.ChoiceSpecs, new PBEItemData(10) },
            { PBEItem.ChopleBerry, new PBEItemData(10, 60, PBEType.Fighting) },
            { PBEItem.ClawFossil, new PBEItemData(100) },
            { PBEItem.CleanseTag, new PBEItemData(30) },
            { PBEItem.CleverWing, new PBEItemData(20) },
            { PBEItem.CobaBerry, new PBEItemData(10, 60, PBEType.Flying) },
            { PBEItem.ColburBerry, new PBEItemData(10, 60, PBEType.Dark) },
            { PBEItem.CometShard, new PBEItemData(30) },
            { PBEItem.CornnBerry, new PBEItemData(10, 70, PBEType.Bug) },
            { PBEItem.CoverFossil, new PBEItemData(100) },
            { PBEItem.CustapBerry, new PBEItemData(10, 80, PBEType.Ghost) },
            { PBEItem.DampMulch, new PBEItemData(30) },
            { PBEItem.DampRock, new PBEItemData(60) },
            { PBEItem.DarkGem, new PBEItemData() },
            { PBEItem.DawnStone, new PBEItemData(80) },
            { PBEItem.DeepSeaScale, new PBEItemData(30) },
            { PBEItem.DeepSeaTooth, new PBEItemData(90) },
            { PBEItem.DestinyKnot, new PBEItemData(10) },
            { PBEItem.DireHit, new PBEItemData(30) },
            { PBEItem.DiveBall, new PBEItemData() },
            { PBEItem.DomeFossil, new PBEItemData(100) },
            { PBEItem.DouseDrive, new PBEItemData(70) },
            { PBEItem.DracoPlate, new PBEItemData(90) },
            { PBEItem.DragonFang, new PBEItemData(70) },
            { PBEItem.DragonGem, new PBEItemData() },
            { PBEItem.DragonScale, new PBEItemData(30) },
            { PBEItem.DreadPlate, new PBEItemData(90) },
            { PBEItem.DreamBall, new PBEItemData() },
            { PBEItem.DubiousDisc, new PBEItemData(50) },
            { PBEItem.DurinBerry, new PBEItemData(10, 80, PBEType.Water) },
            { PBEItem.DuskBall, new PBEItemData() },
            { PBEItem.DuskStone, new PBEItemData(80) },
            { PBEItem.EarthPlate, new PBEItemData(90) },
            { PBEItem.EjectButton, new PBEItemData(30) },
            { PBEItem.Electirizer, new PBEItemData(80) },
            { PBEItem.ElectricGem, new PBEItemData() },
            { PBEItem.Elixir, new PBEItemData(30) },
            { PBEItem.EnergyPowder, new PBEItemData(30) },
            { PBEItem.EnergyRoot, new PBEItemData(30) },
            { PBEItem.EnigmaBerry, new PBEItemData(10, 80, PBEType.Bug) },
            { PBEItem.EscapeRope, new PBEItemData(30) },
            { PBEItem.Ether, new PBEItemData(30) },
            { PBEItem.Everstone, new PBEItemData(30) },
            { PBEItem.Eviolite, new PBEItemData(40) },
            { PBEItem.ExpertBelt, new PBEItemData(10) },
            { PBEItem.ExpShare, new PBEItemData(30) },
            { PBEItem.FastBall, new PBEItemData() },
            { PBEItem.FavoredMail, new PBEItemData() },
            { PBEItem.FightingGem, new PBEItemData() },
            { PBEItem.FigyBerry, new PBEItemData(10, 60, PBEType.Bug) },
            { PBEItem.FireGem, new PBEItemData() },
            { PBEItem.FireStone, new PBEItemData(30) },
            { PBEItem.FistPlate, new PBEItemData(90) },
            { PBEItem.FlameOrb, new PBEItemData(30) },
            { PBEItem.FlamePlate, new PBEItemData(90) },
            { PBEItem.FloatStone, new PBEItemData(30) },
            { PBEItem.FluffyTail, new PBEItemData(30) },
            { PBEItem.FlyingGem, new PBEItemData() },
            { PBEItem.FocusBand, new PBEItemData(10) },
            { PBEItem.FocusSash, new PBEItemData(10) },
            { PBEItem.FreshWater, new PBEItemData(30) },
            { PBEItem.FriendBall, new PBEItemData() },
            { PBEItem.FullHeal, new PBEItemData(30) },
            { PBEItem.FullIncense, new PBEItemData(10) },
            { PBEItem.FullRestore, new PBEItemData(30) },
            { PBEItem.GanlonBerry, new PBEItemData(10, 80, PBEType.Ice) },
            { PBEItem.GeniusWing, new PBEItemData(20) },
            { PBEItem.GhostGem, new PBEItemData() },
            { PBEItem.GooeyMulch, new PBEItemData(30) },
            { PBEItem.GrassGem, new PBEItemData() },
            { PBEItem.GreatBall, new PBEItemData() },
            { PBEItem.GrnApricorn, new PBEItemData() },
            { PBEItem.GreenScarf, new PBEItemData(10) },
            { PBEItem.GreenShard, new PBEItemData(30) },
            { PBEItem.GreetMail, new PBEItemData() },
            { PBEItem.GrepaBerry, new PBEItemData(10, 70, PBEType.Flying) },
            { PBEItem.GripClaw, new PBEItemData(90) },
            { PBEItem.GriseousOrb, new PBEItemData(60) },
            { PBEItem.GroundGem, new PBEItemData() },
            { PBEItem.GrowthMulch, new PBEItemData(30) },
            { PBEItem.GuardSpec, new PBEItemData(30) },
            { PBEItem.HabanBerry, new PBEItemData(10, 60, PBEType.Dragon) },
            { PBEItem.HardStone, new PBEItemData(100) },
            { PBEItem.HealBall, new PBEItemData() },
            { PBEItem.HealPowder, new PBEItemData(30) },
            { PBEItem.HealthWing, new PBEItemData(20) },
            { PBEItem.HeartScale, new PBEItemData(30) },
            { PBEItem.HeatRock, new PBEItemData(60) },
            { PBEItem.HeavyBall, new PBEItemData() },
            { PBEItem.HelixFossil, new PBEItemData(100) },
            { PBEItem.HondewBerry, new PBEItemData(10, 70, PBEType.Ground) },
            { PBEItem.Honey, new PBEItemData(30) },
            { PBEItem.HPUp, new PBEItemData(30) },
            { PBEItem.HyperPotion, new PBEItemData(30) },
            { PBEItem.IapapaBerry, new PBEItemData(10, 60, PBEType.Dark) },
            { PBEItem.IceGem, new PBEItemData() },
            { PBEItem.IceHeal, new PBEItemData(30) },
            { PBEItem.IciclePlate, new PBEItemData(90) },
            { PBEItem.IcyRock, new PBEItemData(40) },
            { PBEItem.InquiryMail, new PBEItemData() },
            { PBEItem.InsectPlate, new PBEItemData(90) },
            { PBEItem.Iron, new PBEItemData(30) },
            { PBEItem.IronBall, new PBEItemData(130) },
            { PBEItem.IronPlate, new PBEItemData(90) },
            { PBEItem.JabocaBerry, new PBEItemData(10, 80, PBEType.Dragon) },
            { PBEItem.KasibBerry, new PBEItemData(10, 60, PBEType.Ghost) },
            { PBEItem.KebiaBerry, new PBEItemData(10, 60, PBEType.Poison) },
            { PBEItem.KelpsyBerry, new PBEItemData(10, 70, PBEType.Fighting) },
            { PBEItem.KingsRock, new PBEItemData(30) },
            { PBEItem.LaggingTail, new PBEItemData(10) },
            { PBEItem.LansatBerry, new PBEItemData(10, 80, PBEType.Flying) },
            { PBEItem.LavaCookie, new PBEItemData(30) },
            { PBEItem.LaxIncense, new PBEItemData(10) },
            { PBEItem.LeafStone, new PBEItemData(30) },
            { PBEItem.Leftovers, new PBEItemData(10) },
            { PBEItem.Lemonade, new PBEItemData(30) },
            { PBEItem.LeppaBerry, new PBEItemData(10, 60, PBEType.Fighting) },
            { PBEItem.LevelBall, new PBEItemData() },
            { PBEItem.LiechiBerry, new PBEItemData(10, 80, PBEType.Grass) },
            { PBEItem.LifeOrb, new PBEItemData(30) },
            { PBEItem.LightBall, new PBEItemData(30) },
            { PBEItem.LightClay, new PBEItemData(30) },
            { PBEItem.LikeMail, new PBEItemData() },
            { PBEItem.LoveBall, new PBEItemData() },
            { PBEItem.LuckIncense, new PBEItemData(10) },
            { PBEItem.LuckyEgg, new PBEItemData(30) },
            { PBEItem.LuckyPunch, new PBEItemData(40) },
            { PBEItem.LumBerry, new PBEItemData(10, 60, PBEType.Flying) },
            { PBEItem.LureBall, new PBEItemData() },
            { PBEItem.LustrousOrb, new PBEItemData(60) },
            { PBEItem.LuxuryBall, new PBEItemData() },
            { PBEItem.MachoBrace, new PBEItemData(60) },
            { PBEItem.Magmarizer, new PBEItemData(80) },
            { PBEItem.Magnet, new PBEItemData(30) },
            { PBEItem.MagoBerry, new PBEItemData(10, 60, PBEType.Ghost) },
            { PBEItem.MagostBerry, new PBEItemData(10, 70, PBEType.Rock) },
            { PBEItem.MasterBall, new PBEItemData() },
            { PBEItem.MaxElixir, new PBEItemData(30) },
            { PBEItem.MaxEther, new PBEItemData(30) },
            { PBEItem.MaxPotion, new PBEItemData(30) },
            { PBEItem.MaxRepel, new PBEItemData(30) },
            { PBEItem.MaxRevive, new PBEItemData(30) },
            { PBEItem.MeadowPlate, new PBEItemData(90) },
            { PBEItem.MentalHerb, new PBEItemData(10) },
            { PBEItem.MetalCoat, new PBEItemData(30) },
            { PBEItem.MetalPowder, new PBEItemData(10) },
            { PBEItem.Metronome, new PBEItemData(30) },
            { PBEItem.MicleBerry, new PBEItemData(10, 80, PBEType.Rock) },
            { PBEItem.MindPlate, new PBEItemData(90) },
            { PBEItem.MiracleSeed, new PBEItemData(30) },
            { PBEItem.MoomooMilk, new PBEItemData(30) },
            { PBEItem.MoonBall, new PBEItemData() },
            { PBEItem.MoonStone, new PBEItemData(30) },
            { PBEItem.MuscleBand, new PBEItemData(10) },
            { PBEItem.MuscleWing, new PBEItemData(20) },
            { PBEItem.MysticWater, new PBEItemData(30) },
            { PBEItem.NanabBerry, new PBEItemData(10, 70, PBEType.Water) },
            { PBEItem.NestBall, new PBEItemData() },
            { PBEItem.NetBall, new PBEItemData() },
            { PBEItem.NeverMeltIce, new PBEItemData(30) },
            { PBEItem.NomelBerry, new PBEItemData(10, 70, PBEType.Dragon) },
            { PBEItem.NormalGem, new PBEItemData() },
            { PBEItem.Nugget, new PBEItemData(30) },
            { PBEItem.OccaBerry, new PBEItemData(10, 60, PBEType.Fire) },
            { PBEItem.OddIncense, new PBEItemData(10) },
            { PBEItem.OddKeystone, new PBEItemData(80) },
            { PBEItem.OldAmber, new PBEItemData(100) },
            { PBEItem.OldGateau, new PBEItemData(30) },
            { PBEItem.OranBerry, new PBEItemData(10, 60, PBEType.Poison) },
            { PBEItem.OvalStone, new PBEItemData(80) },
            { PBEItem.PamtreBerry, new PBEItemData(10, 70, PBEType.Steel) },
            { PBEItem.ParalyzHeal, new PBEItemData(30) },
            { PBEItem.ParkBall, new PBEItemData() },
            { PBEItem.PasshoBerry, new PBEItemData(10, 60, PBEType.Water) },
            { PBEItem.PassOrb, new PBEItemData(30) },
            { PBEItem.PayapaBerry, new PBEItemData(10, 60, PBEType.Psychic) },
            { PBEItem.Pearl, new PBEItemData(30) },
            { PBEItem.PearlString, new PBEItemData(30) },
            { PBEItem.PechaBerry, new PBEItemData(10, 60, PBEType.Electric) },
            { PBEItem.PersimBerry, new PBEItemData(10, 60, PBEType.Ground) },
            { PBEItem.PetayaBerry, new PBEItemData(10, 80, PBEType.Poison) },
            { PBEItem.PinapBerry, new PBEItemData(10, 70, PBEType.Grass) },
            { PBEItem.PnkApricorn, new PBEItemData() },
            { PBEItem.PinkScarf, new PBEItemData(10) },
            { PBEItem.PlumeFossil, new PBEItemData(100) },
            { PBEItem.PoisonBarb, new PBEItemData(70) },
            { PBEItem.PoisonGem, new PBEItemData() },
            { PBEItem.PokeBall, new PBEItemData() },
            { PBEItem.PokeDoll, new PBEItemData(30) },
            { PBEItem.PokeToy, new PBEItemData(30) },
            { PBEItem.PomegBerry, new PBEItemData(10, 70, PBEType.Ice) },
            { PBEItem.Potion, new PBEItemData(30) },
            { PBEItem.PowerAnklet, new PBEItemData(70) },
            { PBEItem.PowerBand, new PBEItemData(70) },
            { PBEItem.PowerBelt, new PBEItemData(70) },
            { PBEItem.PowerBracer, new PBEItemData(70) },
            { PBEItem.PowerHerb, new PBEItemData(10) },
            { PBEItem.PowerLens, new PBEItemData(70) },
            { PBEItem.PowerWeight, new PBEItemData(70) },
            { PBEItem.PPMax, new PBEItemData(30) },
            { PBEItem.PPUp, new PBEItemData(30) },
            { PBEItem.PremierBall, new PBEItemData() },
            { PBEItem.PrettyWing, new PBEItemData(30) },
            { PBEItem.PrismScale, new PBEItemData(30) },
            { PBEItem.Protector, new PBEItemData(80) },
            { PBEItem.Protein, new PBEItemData(30) },
            { PBEItem.PsychicGem, new PBEItemData() },
            { PBEItem.PureIncense, new PBEItemData(10) },
            { PBEItem.QualotBerry, new PBEItemData(10, 70, PBEType.Poison) },
            { PBEItem.QuickBall, new PBEItemData() },
            { PBEItem.QuickClaw, new PBEItemData(80) },
            { PBEItem.QuickPowder, new PBEItemData(10) },
            { PBEItem.RabutaBerry, new PBEItemData(10, 70, PBEType.Ghost) },
            { PBEItem.RageCandyBar, new PBEItemData(30) },
            { PBEItem.RareBone, new PBEItemData(100) },
            { PBEItem.RareCandy, new PBEItemData(30) },
            { PBEItem.RawstBerry, new PBEItemData(10, 60, PBEType.Grass) },
            { PBEItem.RazorClaw, new PBEItemData(80) },
            { PBEItem.RazorFang, new PBEItemData(30) },
            { PBEItem.RazzBerry, new PBEItemData(10, 60, PBEType.Steel) },
            { PBEItem.ReaperCloth, new PBEItemData(10) },
            { PBEItem.RedApricorn, new PBEItemData() },
            { PBEItem.RedCard, new PBEItemData(10) },
            { PBEItem.RedFlute, new PBEItemData(30) },
            { PBEItem.RedScarf, new PBEItemData(10) },
            { PBEItem.RedShard, new PBEItemData(30) },
            { PBEItem.RelicBand, new PBEItemData(30) },
            { PBEItem.RelicCopper, new PBEItemData(30) },
            { PBEItem.RelicCrown, new PBEItemData(30) },
            { PBEItem.RelicGold, new PBEItemData(30) },
            { PBEItem.RelicSilver, new PBEItemData(30) },
            { PBEItem.RelicStatue, new PBEItemData(30) },
            { PBEItem.RelicVase, new PBEItemData(30) },
            { PBEItem.RepeatBall, new PBEItemData() },
            { PBEItem.Repel, new PBEItemData(30) },
            { PBEItem.ReplyMail, new PBEItemData() },
            { PBEItem.ResistWing, new PBEItemData(20) },
            { PBEItem.RevivalHerb, new PBEItemData(30) },
            { PBEItem.Revive, new PBEItemData(30) },
            { PBEItem.RindoBerry, new PBEItemData(10, 60, PBEType.Grass) },
            { PBEItem.RingTarget, new PBEItemData(10) },
            { PBEItem.RockGem, new PBEItemData() },
            { PBEItem.RockIncense, new PBEItemData(10) },
            { PBEItem.RockyHelmet, new PBEItemData(60) },
            { PBEItem.RootFossil, new PBEItemData(100) },
            { PBEItem.RoseIncense, new PBEItemData(10) },
            { PBEItem.RowapBerry, new PBEItemData(10, 80, PBEType.Dark) },
            { PBEItem.RSVPMail, new PBEItemData() },
            { PBEItem.SacredAsh, new PBEItemData(30) },
            { PBEItem.SafariBall, new PBEItemData() },
            { PBEItem.SalacBerry, new PBEItemData(10, 80, PBEType.Fighting) },
            { PBEItem.ScopeLens, new PBEItemData(30) },
            { PBEItem.SeaIncense, new PBEItemData(10) },
            { PBEItem.SharpBeak, new PBEItemData(50) },
            { PBEItem.ShedShell, new PBEItemData(10) },
            { PBEItem.ShellBell, new PBEItemData(30) },
            { PBEItem.ShinyStone, new PBEItemData(80) },
            { PBEItem.ShoalSalt, new PBEItemData(30) },
            { PBEItem.ShoalShell, new PBEItemData(30) },
            { PBEItem.ShockDrive, new PBEItemData(70) },
            { PBEItem.ShucaBerry, new PBEItemData(10, 60, PBEType.Ground) },
            { PBEItem.SilkScarf, new PBEItemData(10) },
            { PBEItem.SilverPowder, new PBEItemData(10) },
            { PBEItem.SitrusBerry, new PBEItemData(10, 60, PBEType.Psychic) },
            { PBEItem.SkullFossil, new PBEItemData(100) },
            { PBEItem.SkyPlate, new PBEItemData(90) },
            { PBEItem.SmokeBall, new PBEItemData(30) },
            { PBEItem.SmoothRock, new PBEItemData(10) },
            { PBEItem.SodaPop, new PBEItemData(30) },
            { PBEItem.SoftSand, new PBEItemData(10) },
            { PBEItem.SootheBell, new PBEItemData(10) },
            { PBEItem.SoulDew, new PBEItemData(30) },
            { PBEItem.SpellTag, new PBEItemData(30) },
            { PBEItem.SpelonBerry, new PBEItemData(10, 70, PBEType.Dark) },
            { PBEItem.SplashPlate, new PBEItemData(90) },
            { PBEItem.SpookyPlate, new PBEItemData(90) },
            { PBEItem.SportBall, new PBEItemData() },
            { PBEItem.StableMulch, new PBEItemData(30) },
            { PBEItem.Stardust, new PBEItemData(30) },
            { PBEItem.StarfBerry, new PBEItemData(10, 80, PBEType.Psychic) },
            { PBEItem.StarPiece, new PBEItemData(30) },
            { PBEItem.SteelGem, new PBEItemData() },
            { PBEItem.Stick, new PBEItemData(60) },
            { PBEItem.StickyBarb, new PBEItemData(80) },
            { PBEItem.StonePlate, new PBEItemData(90) },
            { PBEItem.SunStone, new PBEItemData(30) },
            { PBEItem.SuperPotion, new PBEItemData(30) },
            { PBEItem.SuperRepel, new PBEItemData(30) },
            { PBEItem.SweetHeart, new PBEItemData(30) },
            { PBEItem.SwiftWing, new PBEItemData(20) },
            { PBEItem.TamatoBerry, new PBEItemData(10, 70, PBEType.Psychic) },
            { PBEItem.TangaBerry, new PBEItemData(10, 60, PBEType.Bug) },
            { PBEItem.ThanksMail, new PBEItemData() },
            { PBEItem.ThickClub, new PBEItemData(90) },
            { PBEItem.Thunderstone, new PBEItemData(30) },
            { PBEItem.TimerBall, new PBEItemData() },
            { PBEItem.TinyMushroom, new PBEItemData(30) },
            { PBEItem.ToxicOrb, new PBEItemData(30) },
            { PBEItem.ToxicPlate, new PBEItemData(90) },
            { PBEItem.TwistedSpoon, new PBEItemData(30) },
            { PBEItem.UltraBall, new PBEItemData() },
            { PBEItem.UpGrade, new PBEItemData(30) },
            { PBEItem.WacanBerry, new PBEItemData(10, 60, PBEType.Electric) },
            { PBEItem.WaterGem, new PBEItemData() },
            { PBEItem.WaterStone, new PBEItemData(30) },
            { PBEItem.WatmelBerry, new PBEItemData(10, 80, PBEType.Fire) },
            { PBEItem.WaveIncense, new PBEItemData(10) },
            { PBEItem.WepearBerry, new PBEItemData(10, 70, PBEType.Electric) },
            { PBEItem.WhtApricorn, new PBEItemData() },
            { PBEItem.WhiteFlute, new PBEItemData(30) },
            { PBEItem.WhiteHerb, new PBEItemData(10) },
            { PBEItem.WideLens, new PBEItemData(10) },
            { PBEItem.WikiBerry, new PBEItemData(10, 60, PBEType.Rock) },
            { PBEItem.WiseGlasses, new PBEItemData(10) },
            { PBEItem.XAccuracy, new PBEItemData(30) },
            { PBEItem.XAttack, new PBEItemData(30) },
            { PBEItem.XDefend, new PBEItemData(30) },
            { PBEItem.XSpDef, new PBEItemData(30) },
            { PBEItem.XSpecial, new PBEItemData(30) },
            { PBEItem.XSpeed, new PBEItemData(30) },
            { PBEItem.YacheBerry, new PBEItemData(10, 60, PBEType.Ice) },
            { PBEItem.YlwApricorn, new PBEItemData() },
            { PBEItem.YellowFlute, new PBEItemData(30) },
            { PBEItem.YellowScarf, new PBEItemData(10) },
            { PBEItem.YellowShard, new PBEItemData(30) },
            { PBEItem.ZapPlate, new PBEItemData(90) },
            { PBEItem.Zinc, new PBEItemData(30) },
            { PBEItem.ZoomLens, new PBEItemData(10) }
        });
    }
}
