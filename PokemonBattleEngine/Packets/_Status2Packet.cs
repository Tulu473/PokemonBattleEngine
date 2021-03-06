﻿using Ether.Network.Packets;
using Kermalis.PokemonBattleEngine.Battle;
using Kermalis.PokemonBattleEngine.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kermalis.PokemonBattleEngine.Packets
{
    public sealed class PBEStatus2Packet : INetPacket
    {
        public const short Code = 0x12;
        public IEnumerable<byte> Buffer { get; }

        public PBEFieldPosition Status2Receiver { get; }
        public PBETeam Status2ReceiverTeam { get; }
        public PBEFieldPosition Pokemon2 { get; }
        public PBETeam Pokemon2Team { get; }
        public PBEStatus2 Status2 { get; }
        public PBEStatusAction StatusAction { get; }

        public PBEStatus2Packet(PBEPokemon status2Receiver, PBEPokemon pokemon2, PBEStatus2 status2, PBEStatusAction statusAction)
        {
            var bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(Code));
            bytes.Add((byte)(Status2Receiver = status2Receiver.FieldPosition));
            bytes.Add((Status2ReceiverTeam = status2Receiver.Team).Id);
            bytes.Add((byte)(Pokemon2 = pokemon2.FieldPosition));
            bytes.Add((Pokemon2Team = pokemon2.Team).Id);
            bytes.AddRange(BitConverter.GetBytes((uint)(Status2 = status2)));
            bytes.Add((byte)(StatusAction = statusAction));
            Buffer = BitConverter.GetBytes((short)bytes.Count).Concat(bytes);
        }
        public PBEStatus2Packet(byte[] buffer, PBEBattle battle)
        {
            Buffer = buffer;
            using (var r = new BinaryReader(new MemoryStream(buffer)))
            {
                r.ReadInt16(); // Skip Code
                Status2Receiver = (PBEFieldPosition)r.ReadByte();
                Status2ReceiverTeam = battle.Teams[r.ReadByte()];
                Pokemon2 = (PBEFieldPosition)r.ReadByte();
                Pokemon2Team = battle.Teams[r.ReadByte()];
                Status2 = (PBEStatus2)r.ReadUInt32();
                StatusAction = (PBEStatusAction)r.ReadByte();
            }
        }

        public void Dispose() { }
    }
}
