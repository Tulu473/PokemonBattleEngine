﻿using Kermalis.PokemonBattleEngine.Battle;
using Kermalis.PokemonBattleEngine.Packets;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kermalis.PokemonBattleEngine.Data
{
    public sealed class PPokemon
    {
        public readonly byte Id;
        // Not included in ToBytes() or FromBytes(). Set manually by the host
        // True indicates this Pokémon is owned by the client or team 0 in the eyes of the host/spectators
        public bool Local;
        public readonly PPokemonShell Shell;

        public string NameForTrainer(bool firstLetterCapitalized)
        {
            string prefix;
            if (firstLetterCapitalized)
            {
                if (Local)
                    prefix = "";
                else
                    prefix = "The foe's ";
            }
            else
            {
                if (Local)
                    prefix = "";
                else
                    prefix = "the foe's ";
            }
            return prefix + Shell.Nickname;
        }
        public string NameWithGender => Shell.Nickname + GenderSymbol;
        public string GenderSymbol => Shell.Gender == PGender.Female ? "♀" : Shell.Gender == PGender.Male ? "♂" : string.Empty;

        public ushort HP, MaxHP, Attack, Defense, SpAttack, SpDefense, Speed;
        public PMove[] Moves = new PMove[PSettings.NumMoves];
        public byte[] PP = new byte[PSettings.NumMoves], MaxPP = new byte[PSettings.NumMoves];

        public PSpecies Species;
        public bool Shiny;
        public PAbility Ability;
        public PType Type1, Type2;
        public PItem Item;
        public PFieldPosition FieldPosition;
        public PStatus1 Status1;
        public PStatus2 Status2;
        // These are in a set order; see BattleEffects->ApplyStatChange()
        public sbyte AttackChange, DefenseChange, SpAttackChange, SpDefenseChange, SpeedChange, AccuracyChange, EvasionChange;

        public byte Status1Counter; // Toxic/Sleep
        public byte SleepTurns; // Amount of turns to Sleep

        public byte ConfusionCounter; // Confused
        public byte ConfusionTurns; // Amount of turns to be Confused

        public byte ProtectCounter; // Protect
        public ushort SubstituteHP; // Substitute

        public PFieldPosition SeededPosition; // The position to return Leech Seed HP to on the opposing team

        public PAction PreviousAction, LockedAction, SelectedAction;

        // Stats & PP are set from the shell info, but Local will need to be manually set by the host
        public PPokemon(byte id, PPokemonShell shell)
        {
            Shell = shell;
            Species = Shell.Species;
            Shiny = Shell.Shiny;
            Ability = Shell.Ability;
            Item = Shell.Item;
            Id = id;
            SelectedAction.PokemonId = id;
            CalculateStats();
            HP = MaxHP;
            Moves = Shell.Moves;
            for (int i = 0; i < PSettings.NumMoves; i++)
            {
                PMove move = Shell.Moves[i];
                if (move != PMove.None)
                {
                    byte tier = PMoveData.Data[move].PPTier;
                    int movePP = (tier * PSettings.PPMultiplier) + (tier * Shell.PPUps[i]);
                    PP[i] = MaxPP[i] = (byte)movePP;
                }
            }
            Type1 = PPokemonData.Data[Species].Type1;
            Type2 = PPokemonData.Data[Species].Type2;
        }
        // This constructor is to define an unknown remote Pokémon
        // Local is set to false here
        public PPokemon(PPkmnSwitchInPacket psip)
        {
            Id = psip.PokemonId;
            Local = false;
            Shell = new PPokemonShell
            {
                Species = psip.Species,
                Shiny = psip.Shiny,
                Nickname = psip.Nickname,
                Level = psip.Level,
                Gender = psip.Gender,
                Ability = PAbility.MAX,
                Item = PItem.MAX,
                Nature = PNature.MAX
            };
            Species = psip.Species;
            Shiny = psip.Shiny;
            Ability = PAbility.MAX;
            Item = PItem.MAX;
            for (int i = 0; i < PSettings.NumMoves; i++)
                Moves[i] = PMove.MAX;
            Type1 = PPokemonData.Data[Species].Type1;
            Type2 = PPokemonData.Data[Species].Type2;
        }

        public bool HasType(PType type) => Type1 == type || Type2 == type;

        void CalculateStats()
        {
            PPokemonData pData = PPokemonData.Data[Species];

            MaxHP = (ushort)(((2 * pData.HP + Shell.IVs[0] + (Shell.EVs[0] / 4)) * Shell.Level / PSettings.MaxLevel) + Shell.Level + 10);

            int i = 0;
            ushort OtherStat(byte baseVal)
            {
                double natureMultiplier = 1 + (PPokemonData.NatureBoosts[Shell.Nature][i] * PSettings.NatureStatBoost);
                ushort val = (ushort)((((2 * baseVal + Shell.IVs[i + 1] + (Shell.EVs[i + 1] / 4)) * Shell.Level / PSettings.MaxLevel) + 5) * natureMultiplier);
                i++;
                return val;
            }
            Attack = OtherStat(pData.Attack);
            Defense = OtherStat(pData.Defense);
            SpAttack = OtherStat(pData.SpAttack);
            SpDefense = OtherStat(pData.SpDefense);
            Speed = OtherStat(pData.Speed);
        }

        public void ClearForSwitch()
        {
            FieldPosition = PFieldPosition.None;
            Species = Shell.Species;
            Ability = Shell.Ability;
            Shiny = Shell.Shiny;

            AttackChange = DefenseChange = SpAttackChange = SpDefenseChange = SpeedChange = AccuracyChange = EvasionChange = 0;

            if (Status1 == PStatus1.Asleep)
                Status1Counter = SleepTurns;
            else if (Status1 == PStatus1.BadlyPoisoned)
                Status1Counter = 1;

            Status2 &= ~PStatus2.Confused;
            ConfusionCounter = ConfusionTurns = 0;
            Status2 &= ~PStatus2.LeechSeed;
            SeededPosition = PFieldPosition.None;
            Status2 &= ~PStatus2.Pumped;
            Status2 &= ~PStatus2.Substitute;
            SubstituteHP = 0;
            Status2 &= ~PStatus2.Transformed;

            if (Shell.Nature != PNature.MAX) // If the nature is unset, the program is not the host and does not own the Pokémon
                CalculateStats();
        }

        // Transforms into "target" and sets both Pokémons' information to the parameters
        // Also sets the Status2 transformed bit
        public void Transform(PPokemon target, ushort targetAttack, ushort targetDefense, ushort targetSpAttack, ushort targetSpDefense, ushort targetSpeed, PAbility targetAbility, PType targetType1, PType targetType2, PMove[] targetMoves)
        {
            Species = target.Species;
            Shiny = target.Shiny;
            Ability = target.Ability = targetAbility;
            Type1 = target.Type1 = targetType1;
            Type2 = target.Type2 = targetType2;
            Attack = target.Attack = targetAttack;
            Defense = target.Defense = targetDefense;
            SpAttack = target.SpAttack = targetSpAttack;
            SpDefense = target.SpDefense = targetSpDefense;
            Speed = target.Speed = targetSpeed;
            AttackChange = target.AttackChange;
            DefenseChange = target.DefenseChange;
            SpAttackChange = target.SpAttackChange;
            SpDefenseChange = target.SpDefenseChange;
            SpeedChange = target.SpeedChange;
            AccuracyChange = target.AccuracyChange;
            EvasionChange = target.EvasionChange;
            Moves = target.Moves = targetMoves;
            for (int i = 0; i < PSettings.NumMoves; i++)
                PP[i] = MaxPP[i] = PSettings.PPMultiplier;
            Status2 |= PStatus2.Transformed;
        }

        public PType GetHiddenPowerType()
        {
            int a = Shell.IVs[0] & 1,
                b = Shell.IVs[1] & 1,
                c = Shell.IVs[2] & 1,
                d = Shell.IVs[5] & 1,
                e = Shell.IVs[3] & 1,
                f = Shell.IVs[4] & 1;
            return PPokemonData.HiddenPowerTypes[((1 << 0) * a + (1 << 1) * b + (1 << 2) * c + (1 << 3) * d + (1 << 4) * e + (1 << 5) * f) * (PPokemonData.HiddenPowerTypes.Length - 1) / ((1 << 6) - 1)];
        }
        public int GetHiddenPowerBasePower()
        {
            int a = (Shell.IVs[0] & 2) == 2 ? 1 : 0,
                b = (Shell.IVs[1] & 2) == 2 ? 1 : 0,
                c = (Shell.IVs[2] & 2) == 2 ? 1 : 0,
                d = (Shell.IVs[5] & 2) == 2 ? 1 : 0,
                e = (Shell.IVs[3] & 2) == 2 ? 1 : 0,
                f = (Shell.IVs[4] & 2) == 2 ? 1 : 0;
            // 30 is minimum, 30+40 is maximum
            return (((1 << 0) * a + (1 << 1) * b + (1 << 2) * c + (1 << 3) * d + (1 << 4) * e + (1 << 5) * f) * 40 / ((1 << 6) - 1)) + 30;
        }

        // ToBytes() and FromBytes() will only be used when the server sends you your team Ids, so they do not need to contain all info
        internal byte[] ToBytes()
        {
            var bytes = new List<byte>();
            bytes.Add(Id);
            bytes.AddRange(Shell.ToBytes());
            return bytes.ToArray();
        }
        internal static PPokemon FromBytes(BinaryReader r)
        {
            return new PPokemon(r.ReadByte(), PPokemonShell.FromBytes(r));
        }

        public override bool Equals(object obj)
        {
            if (obj is PPokemon other)
                return other.Id.Equals(Id);
            return base.Equals(obj);
        }
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString()
        {
            bool remotePokemon = Shell.Nature == PNature.MAX; // If the nature is unset, the program is not the host and does not own the Pokémon

            string item = Item.ToString().Replace("MAX", "???");
            string nature = Shell.Nature.ToString().Replace("MAX", "???");
            string ability = Ability.ToString().Replace("MAX", "???");
            string[] moveStrs = new string[PSettings.NumMoves];
            for (int i = 0; i < PSettings.NumMoves; i++)
            {
                string mStr = Moves[i].ToString().Replace("MAX", "???");
                if (!remotePokemon)
                    mStr += $" {PP[i]}/{MaxPP[i]}";
                moveStrs[i] = mStr;
            }
            string moves = moveStrs.Print(false);

            var sb = new StringBuilder();

            sb.AppendLine($"{Shell.Nickname}/{Species} {GenderSymbol} Lv.{Shell.Level}");
            sb.AppendLine($"HP: {HP}/{MaxHP} ({(double)HP / MaxHP:P2})");
            sb.AppendLine($"Status1: {Status1}");
            sb.AppendLine($"Status2: {Status2}");
            if (!remotePokemon && Status2.HasFlag(PStatus2.Substitute))
                sb.AppendLine($"Substitute HP: {SubstituteHP}");
            sb.AppendLine($"Item: {item}");
            sb.AppendLine($"Ability: {ability}");
            if (!remotePokemon)
                sb.AppendLine($"Nature: {nature}");
            if (!remotePokemon)
                sb.AppendLine($"Hidden Power: {GetHiddenPowerType()}/{GetHiddenPowerBasePower()}");
            sb.Append($"Moves: {moves}");

            return sb.ToString();
        }
    }
}
