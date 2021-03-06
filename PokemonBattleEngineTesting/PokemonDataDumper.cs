﻿using Kermalis.EndianBinaryIO;
using Kermalis.PokemonBattleEngine.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kermalis.PokemonBattleEngineTesting
{
    class PokemonDataDumper
    {
        private class Pokemon
        {
            public byte HP, Attack, Defense, SpAttack, SpDefense, Speed;
            public PBEType Type1, Type2;
            public PBEGenderRatio GenderRatio;
            public double Weight;
            public List<PBESpecies> PreEvolutions = new List<PBESpecies>();
            public List<PBESpecies> Evolutions = new List<PBESpecies>();
            public List<PBEAbility> Abilities = new List<PBEAbility>();
            public Dictionary<Tuple<int, PBEMove>, string> LevelUpMoves = new Dictionary<Tuple<int, PBEMove>, string>();
            public Dictionary<PBEMove, string> OtherMoves = new Dictionary<PBEMove, string>();
        }

        static readonly PBEType[] gen5Types = new PBEType[17]
        {
            PBEType.Normal,
            PBEType.Fighting,
            PBEType.Flying,
            PBEType.Poison,
            PBEType.Ground,
            PBEType.Rock,
            PBEType.Bug,
            PBEType.Ghost,
            PBEType.Steel,
            PBEType.Fire,
            PBEType.Water,
            PBEType.Grass,
            PBEType.Electric,
            PBEType.Psychic,
            PBEType.Ice,
            PBEType.Dragon,
            PBEType.Dark
        };
        static readonly Dictionary<byte, PBEGenderRatio> gen5GenderRatios = new Dictionary<byte, PBEGenderRatio>
        {
            { 0x00, PBEGenderRatio.M1_F0 },
            { 0x1F, PBEGenderRatio.M7_F1 },
            { 0x3F, PBEGenderRatio.M3_F1 },
            { 0x7F, PBEGenderRatio.M1_F1 },
            { 0xBF, PBEGenderRatio.M1_F3 },
            { 0xFE, PBEGenderRatio.M0_F1 },
            { 0xFF, PBEGenderRatio.M0_F0 },
        };
        static readonly Dictionary<int, PBESpecies> gen3SpeciesIndexToPBESpecies = new Dictionary<int, PBESpecies>
        {
            { 1, PBESpecies.Bulbasaur },
            { 2, PBESpecies.Ivysaur },
            { 3, PBESpecies.Venusaur },
            { 4, PBESpecies.Charmander },
            { 5, PBESpecies.Charmeleon },
            { 6, PBESpecies.Charizard },
            { 7, PBESpecies.Squirtle },
            { 8, PBESpecies.Wartortle },
            { 9, PBESpecies.Blastoise },
            { 10, PBESpecies.Caterpie },
            { 11, PBESpecies.Metapod },
            { 12, PBESpecies.Butterfree },
            { 13, PBESpecies.Weedle },
            { 14, PBESpecies.Kakuna },
            { 15, PBESpecies.Beedrill },
            { 16, (PBESpecies)16 },
            { 17, (PBESpecies)17 },
            { 18, (PBESpecies)18 },
            { 19, (PBESpecies)19 },
            { 20, (PBESpecies)20 },
            { 21, (PBESpecies)21 },
            { 22, (PBESpecies)22 },
            { 23, (PBESpecies)23 },
            { 24, (PBESpecies)24 },
            { 25, PBESpecies.Pikachu },
            { 26, PBESpecies.Raichu },
            { 27, (PBESpecies)27 },
            { 28, (PBESpecies)28 },
            { 29, (PBESpecies)29 },
            { 30, (PBESpecies)30 },
            { 31, (PBESpecies)31 },
            { 32, (PBESpecies)32 },
            { 33, (PBESpecies)33 },
            { 34, (PBESpecies)34 },
            { 35, (PBESpecies)35 },
            { 36, (PBESpecies)36 },
            { 37, PBESpecies.Vulpix },
            { 38, PBESpecies.Ninetales },
            { 39, (PBESpecies)39 },
            { 40, (PBESpecies)40 },
            { 41, PBESpecies.Zubat },
            { 42, PBESpecies.Golbat },
            { 43, (PBESpecies)43 },
            { 44, (PBESpecies)44 },
            { 45, (PBESpecies)45 },
            { 46, (PBESpecies)46 },
            { 47, (PBESpecies)47 },
            { 48, (PBESpecies)48 },
            { 49, (PBESpecies)49 },
            { 50, (PBESpecies)50 },
            { 51, (PBESpecies)51 },
            { 52, (PBESpecies)52 },
            { 53, (PBESpecies)53 },
            { 54, PBESpecies.Psyduck },
            { 55, PBESpecies.Golduck },
            { 56, (PBESpecies)56 },
            { 57, (PBESpecies)57 },
            { 58, (PBESpecies)58 },
            { 59, (PBESpecies)59 },
            { 60, PBESpecies.Poliwag },
            { 61, PBESpecies.Poliwhirl },
            { 62, PBESpecies.Poliwrath },
            { 63, (PBESpecies)63 },
            { 64, (PBESpecies)64 },
            { 65, (PBESpecies)65 },
            { 66, (PBESpecies)66 },
            { 67, (PBESpecies)67 },
            { 68, (PBESpecies)68 },
            { 69, (PBESpecies)69 },
            { 70, (PBESpecies)70 },
            { 71, (PBESpecies)71 },
            { 72, (PBESpecies)72 },
            { 73, (PBESpecies)73 },
            { 74, (PBESpecies)74 },
            { 75, (PBESpecies)75 },
            { 76, (PBESpecies)76 },
            { 77, (PBESpecies)77 },
            { 78, (PBESpecies)78 },
            { 79, (PBESpecies)79 },
            { 80, (PBESpecies)80 },
            { 81, (PBESpecies)81 },
            { 82, (PBESpecies)82 },
            { 83, PBESpecies.Farfetchd },
            { 84, (PBESpecies)84 },
            { 85, (PBESpecies)85 },
            { 86, (PBESpecies)86 },
            { 87, (PBESpecies)87 },
            { 88, (PBESpecies)88 },
            { 89, (PBESpecies)89 },
            { 90, (PBESpecies)90 },
            { 91, (PBESpecies)91 },
            { 92, (PBESpecies)92 },
            { 93, (PBESpecies)93 },
            { 94, (PBESpecies)94 },
            { 95, (PBESpecies)95 },
            { 96, (PBESpecies)96 },
            { 97, (PBESpecies)97 },
            { 98, (PBESpecies)98 },
            { 99, (PBESpecies)99 },
            { 100, (PBESpecies)100 },
            { 101, (PBESpecies)101 },
            { 102, (PBESpecies)102 },
            { 103, (PBESpecies)103 },
            { 104, PBESpecies.Cubone },
            { 105, PBESpecies.Marowak },
            { 106, (PBESpecies)106 },
            { 107, (PBESpecies)107 },
            { 108, (PBESpecies)108 },
            { 109, (PBESpecies)109 },
            { 110, (PBESpecies)110 },
            { 111, (PBESpecies)111 },
            { 112, (PBESpecies)112 },
            { 113, PBESpecies.Chansey },
            { 114, (PBESpecies)114 },
            { 115, (PBESpecies)115 },
            { 116, (PBESpecies)116 },
            { 117, (PBESpecies)117 },
            { 118, (PBESpecies)118 },
            { 119, (PBESpecies)119 },
            { 120, (PBESpecies)120 },
            { 121, (PBESpecies)121 },
            { 122, (PBESpecies)122 },
            { 123, (PBESpecies)123 },
            { 124, (PBESpecies)124 },
            { 125, (PBESpecies)125 },
            { 126, (PBESpecies)126 },
            { 127, (PBESpecies)127 },
            { 128, (PBESpecies)128 },
            { 129, PBESpecies.Magikarp },
            { 130, PBESpecies.Gyarados },
            { 131, (PBESpecies)131 },
            { 132, PBESpecies.Ditto },
            { 133, PBESpecies.Eevee },
            { 134, PBESpecies.Vaporeon },
            { 135, PBESpecies.Jolteon },
            { 136, PBESpecies.Flareon },
            { 137, (PBESpecies)137 },
            { 138, (PBESpecies)138 },
            { 139, (PBESpecies)139 },
            { 140, (PBESpecies)140 },
            { 141, (PBESpecies)141 },
            { 142, (PBESpecies)142 },
            { 143, (PBESpecies)143 },
            { 144, (PBESpecies)144 },
            { 145, (PBESpecies)145 },
            { 146, (PBESpecies)146 },
            { 147, (PBESpecies)147 },
            { 148, (PBESpecies)148 },
            { 149, (PBESpecies)149 },
            { 150, (PBESpecies)150 },
            { 151, (PBESpecies)151 },
            { 152, (PBESpecies)152 },
            { 153, (PBESpecies)153 },
            { 154, (PBESpecies)154 },
            { 155, (PBESpecies)155 },
            { 156, (PBESpecies)156 },
            { 157, (PBESpecies)157 },
            { 158, (PBESpecies)158 },
            { 159, (PBESpecies)159 },
            { 160, (PBESpecies)160 },
            { 161, (PBESpecies)161 },
            { 162, (PBESpecies)162 },
            { 163, (PBESpecies)163 },
            { 164, (PBESpecies)164 },
            { 165, (PBESpecies)165 },
            { 166, (PBESpecies)166 },
            { 167, (PBESpecies)167 },
            { 168, (PBESpecies)168 },
            { 169, PBESpecies.Crobat },
            { 170, (PBESpecies)170 },
            { 171, (PBESpecies)171 },
            { 172, PBESpecies.Pichu },
            { 173, (PBESpecies)173 },
            { 174, (PBESpecies)174 },
            { 175, (PBESpecies)175 },
            { 176, (PBESpecies)176 },
            { 177, (PBESpecies)177 },
            { 178, (PBESpecies)178 },
            { 179, (PBESpecies)179 },
            { 180, (PBESpecies)180 },
            { 181, (PBESpecies)181 },
            { 182, (PBESpecies)182 },
            { 183, PBESpecies.Marill },
            { 184, PBESpecies.Azumarill },
            { 185, (PBESpecies)185 },
            { 186, PBESpecies.Politoed },
            { 187, (PBESpecies)187 },
            { 188, (PBESpecies)188 },
            { 189, (PBESpecies)189 },
            { 190, (PBESpecies)190 },
            { 191, (PBESpecies)191 },
            { 192, (PBESpecies)192 },
            { 193, (PBESpecies)193 },
            { 194, (PBESpecies)194 },
            { 195, (PBESpecies)195 },
            { 196, PBESpecies.Espeon },
            { 197, PBESpecies.Umbreon },
            { 198, (PBESpecies)198 },
            { 199, (PBESpecies)199 },
            { 200, PBESpecies.Misdreavus },
            { 201, PBESpecies.Unown_A },
            { 202, (PBESpecies)202 },
            { 203, (PBESpecies)203 },
            { 204, (PBESpecies)204 },
            { 205, (PBESpecies)205 },
            { 206, (PBESpecies)206 },
            { 207, (PBESpecies)207 },
            { 208, (PBESpecies)208 },
            { 209, (PBESpecies)209 },
            { 210, (PBESpecies)210 },
            { 211, (PBESpecies)211 },
            { 212, (PBESpecies)212 },
            { 213, (PBESpecies)213 },
            { 214, (PBESpecies)214 },
            { 215, (PBESpecies)215 },
            { 216, (PBESpecies)216 },
            { 217, (PBESpecies)217 },
            { 218, (PBESpecies)218 },
            { 219, (PBESpecies)219 },
            { 220, (PBESpecies)220 },
            { 221, (PBESpecies)221 },
            { 222, (PBESpecies)222 },
            { 223, (PBESpecies)223 },
            { 224, (PBESpecies)224 },
            { 225, (PBESpecies)225 },
            { 226, (PBESpecies)226 },
            { 227, (PBESpecies)227 },
            { 228, (PBESpecies)228 },
            { 229, (PBESpecies)229 },
            { 230, (PBESpecies)230 },
            { 231, (PBESpecies)231 },
            { 232, (PBESpecies)232 },
            { 233, (PBESpecies)233 },
            { 234, (PBESpecies)234 },
            { 235, PBESpecies.Smeargle },
            { 236, (PBESpecies)236 },
            { 237, (PBESpecies)237 },
            { 238, (PBESpecies)238 },
            { 239, (PBESpecies)239 },
            { 240, (PBESpecies)240 },
            { 241, (PBESpecies)241 },
            { 242, PBESpecies.Blissey },
            { 243, (PBESpecies)243 },
            { 244, (PBESpecies)244 },
            { 245, (PBESpecies)245 },
            { 246, (PBESpecies)246 },
            { 247, (PBESpecies)247 },
            { 248, (PBESpecies)248 },
            { 249, (PBESpecies)249 },
            { 250, (PBESpecies)250 },
            { 251, (PBESpecies)251 },
            { 277, (PBESpecies)252 },
            { 278, (PBESpecies)253 },
            { 279, (PBESpecies)254 },
            { 280, PBESpecies.Torchic },
            { 281, PBESpecies.Combusken },
            { 282, PBESpecies.Blaziken },
            { 283, (PBESpecies)258 },
            { 284, (PBESpecies)259 },
            { 285, (PBESpecies)260 },
            { 286, (PBESpecies)261 },
            { 287, (PBESpecies)262 },
            { 288, (PBESpecies)263 },
            { 289, (PBESpecies)264 },
            { 290, (PBESpecies)265 },
            { 291, (PBESpecies)266 },
            { 292, (PBESpecies)267 },
            { 293, (PBESpecies)268 },
            { 294, (PBESpecies)269 },
            { 295, (PBESpecies)270 },
            { 296, (PBESpecies)271 },
            { 297, (PBESpecies)272 },
            { 298, (PBESpecies)273 },
            { 299, (PBESpecies)274 },
            { 300, (PBESpecies)275 },
            { 301, PBESpecies.Nincada },
            { 302, PBESpecies.Ninjask },
            { 303, PBESpecies.Shedinja },
            { 304, (PBESpecies)276 },
            { 305, (PBESpecies)277 },
            { 306, (PBESpecies)285 },
            { 307, (PBESpecies)286 },
            { 308, (PBESpecies)327 },
            { 309, (PBESpecies)278 },
            { 310, (PBESpecies)279 },
            { 311, (PBESpecies)283 },
            { 312, (PBESpecies)284 },
            { 313, (PBESpecies)320 },
            { 314, (PBESpecies)321 },
            { 315, PBESpecies.Skitty },
            { 316, PBESpecies.Delcatty },
            { 317, (PBESpecies)352 },
            { 318, (PBESpecies)343 },
            { 319, (PBESpecies)344 },
            { 320, (PBESpecies)299 },
            { 321, (PBESpecies)324 },
            { 322, (PBESpecies)302 },
            { 323, (PBESpecies)339 },
            { 324, (PBESpecies)340 },
            { 325, (PBESpecies)370 },
            { 326, (PBESpecies)341 },
            { 327, (PBESpecies)342 },
            { 328, (PBESpecies)349 },
            { 329, (PBESpecies)350 },
            { 330, (PBESpecies)318 },
            { 331, (PBESpecies)319 },
            { 332, (PBESpecies)328 },
            { 333, (PBESpecies)329 },
            { 334, (PBESpecies)330 },
            { 335, (PBESpecies)296 },
            { 336, (PBESpecies)297 },
            { 337, (PBESpecies)309 },
            { 338, (PBESpecies)310 },
            { 339, (PBESpecies)322 },
            { 340, (PBESpecies)323 },
            { 341, (PBESpecies)363 },
            { 342, (PBESpecies)364 },
            { 343, (PBESpecies)365 },
            { 344, (PBESpecies)331 },
            { 345, (PBESpecies)332 },
            { 346, (PBESpecies)361 },
            { 347, (PBESpecies)362 },
            { 348, (PBESpecies)337 },
            { 349, (PBESpecies)338 },
            { 350, PBESpecies.Azurill },
            { 351, (PBESpecies)325 },
            { 352, (PBESpecies)326 },
            { 353, PBESpecies.Plusle },
            { 354, PBESpecies.Minun },
            { 355, (PBESpecies)303 },
            { 356, (PBESpecies)307 },
            { 357, (PBESpecies)308 },
            { 358, (PBESpecies)333 },
            { 359, (PBESpecies)334 },
            { 360, (PBESpecies)360 },
            { 361, (PBESpecies)355 },
            { 362, (PBESpecies)356 },
            { 363, (PBESpecies)315 },
            { 364, (PBESpecies)287 },
            { 365, (PBESpecies)288 },
            { 366, (PBESpecies)289 },
            { 367, (PBESpecies)316 },
            { 368, (PBESpecies)317 },
            { 369, PBESpecies.Tropius },
            { 370, (PBESpecies)293 },
            { 371, (PBESpecies)294 },
            { 372, (PBESpecies)295 },
            { 373, PBESpecies.Clamperl },
            { 374, PBESpecies.Huntail },
            { 375, PBESpecies.Gorebyss },
            { 376, PBESpecies.Absol },
            { 377, (PBESpecies)353 },
            { 378, (PBESpecies)354 },
            { 379, (PBESpecies)336 },
            { 380, (PBESpecies)335 },
            { 381, (PBESpecies)369 },
            { 382, (PBESpecies)304 },
            { 383, (PBESpecies)305 },
            { 384, (PBESpecies)306 },
            { 385, (PBESpecies)351 },
            { 386, (PBESpecies)313 },
            { 387, (PBESpecies)314 },
            { 388, PBESpecies.Lileep },
            { 389, PBESpecies.Cradily },
            { 390, (PBESpecies)347 },
            { 391, (PBESpecies)348 },
            { 392, (PBESpecies)280 },
            { 393, (PBESpecies)281 },
            { 394, (PBESpecies)282 },
            { 395, (PBESpecies)371 },
            { 396, (PBESpecies)372 },
            { 397, (PBESpecies)373 },
            { 398, PBESpecies.Beldum },
            { 399, PBESpecies.Metang },
            { 400, PBESpecies.Metagross },
            { 401, PBESpecies.Regirock },
            { 402, (PBESpecies)378 },
            { 403, (PBESpecies)379 },
            { 404, (PBESpecies)382 },
            { 405, (PBESpecies)383 },
            { 406, (PBESpecies)384 },
            { 407, PBESpecies.Latias },
            { 408, PBESpecies.Latios },
            { 409, (PBESpecies)385 },
            { 410, (PBESpecies)386 },
            { 411, (PBESpecies)358 },
            { 413, PBESpecies.Unown_B },
            { 414, PBESpecies.Unown_C },
            { 415, PBESpecies.Unown_D },
            { 416, PBESpecies.Unown_E },
            { 417, PBESpecies.Unown_F },
            { 418, PBESpecies.Unown_G },
            { 419, PBESpecies.Unown_H },
            { 420, PBESpecies.Unown_I },
            { 421, PBESpecies.Unown_J },
            { 422, PBESpecies.Unown_K },
            { 423, PBESpecies.Unown_L },
            { 424, PBESpecies.Unown_M },
            { 425, PBESpecies.Unown_N },
            { 426, PBESpecies.Unown_O },
            { 427, PBESpecies.Unown_P },
            { 428, PBESpecies.Unown_Q },
            { 429, PBESpecies.Unown_R },
            { 430, PBESpecies.Unown_S },
            { 431, PBESpecies.Unown_T },
            { 432, PBESpecies.Unown_U },
            { 433, PBESpecies.Unown_V },
            { 434, PBESpecies.Unown_W },
            { 435, PBESpecies.Unown_X },
            { 436, PBESpecies.Unown_Y },
            { 437, PBESpecies.Unown_Z },
            { 438, PBESpecies.Unown_Exclamation },
            { 439, PBESpecies.Unown_Question }
        };
        static readonly PBEMove[] gen3TMHMs = new PBEMove[58]
        {
            (PBEMove)264, // FocusPunch
            PBEMove.DragonClaw,
            PBEMove.WaterPulse,
            PBEMove.CalmMind,
            PBEMove.Roar,
            PBEMove.Toxic,
            PBEMove.Hail,
            PBEMove.BulkUp,
            (PBEMove)331, // BulletSeed
            PBEMove.HiddenPower,
            PBEMove.SunnyDay,
            (PBEMove)269, // Taunt
            PBEMove.IceBeam,
            PBEMove.Blizzard,
            (PBEMove)63, // HyperBeam
            PBEMove.LightScreen,
            PBEMove.Protect,
            PBEMove.RainDance,
            PBEMove.GigaDrain,
            (PBEMove)219, // Safeguard
            PBEMove.Frustration,
            (PBEMove)76, // SolarBeam
            PBEMove.IronTail,
            PBEMove.Thunderbolt,
            PBEMove.Thunder,
            PBEMove.Earthquake,
            PBEMove.Return,
            PBEMove.Dig,
            PBEMove.Psychic,
            PBEMove.ShadowBall,
            PBEMove.BrickBreak,
            PBEMove.DoubleTeam,
            PBEMove.Reflect,
            PBEMove.ShockWave,
            PBEMove.Flamethrower,
            PBEMove.SludgeBomb,
            PBEMove.Sandstorm,
            PBEMove.FireBlast,
            PBEMove.RockTomb,
            PBEMove.AerialAce,
            (PBEMove)259, // Torment
            PBEMove.Facade,
            (PBEMove)290, // SecretPower
            PBEMove.Rest,
            (PBEMove)213, // Attract
            (PBEMove)168, // Thief
            PBEMove.SteelWing,
            (PBEMove)285, // SkillSwap
            (PBEMove)289, // Snatch
            PBEMove.Overheat,
            PBEMove.Cut,
            PBEMove.Fly,
            PBEMove.Surf,
            PBEMove.Strength,
            PBEMove.Flash,
            PBEMove.RockSmash,
            PBEMove.Waterfall,
            PBEMove.Dive
        };
        static readonly PBEMove[] frlgTutorMoves = new PBEMove[15]
        {
            PBEMove.MegaPunch,
            PBEMove.SwordsDance,
            PBEMove.MegaKick,
            PBEMove.BodySlam,
            PBEMove.DoubleEdge,
            (PBEMove)68, // Counter
            PBEMove.SeismicToss,
            (PBEMove)102, // Mimic
            PBEMove.Metronome,
            PBEMove.Softboiled,
            PBEMove.DreamEater,
            PBEMove.ThunderWave,
            PBEMove.Explosion,
            PBEMove.RockSlide,
            PBEMove.Substitute
        };
        static readonly PBEMove[] emeraldTutorMoves = new PBEMove[30]
        {
            PBEMove.MegaPunch,
            PBEMove.SwordsDance,
            PBEMove.MegaKick,
            PBEMove.BodySlam,
            PBEMove.DoubleEdge,
            (PBEMove)68, // Counter
            PBEMove.SeismicToss,
            (PBEMove)102, // Mimic
            PBEMove.Metronome,
            PBEMove.Softboiled,
            PBEMove.DreamEater,
            PBEMove.ThunderWave,
            PBEMove.Explosion,
            PBEMove.RockSlide,
            PBEMove.Substitute,
            PBEMove.DynamicPunch,
            (PBEMove)205, // Rollout
            PBEMove.PsychUp,
            PBEMove.Snore,
            PBEMove.IcyWind,
            (PBEMove)203, // Endure
            PBEMove.MudSlap,
            PBEMove.IcePunch,
            PBEMove.Swagger,
            (PBEMove)214, // SleepTalk
            PBEMove.Swift,
            PBEMove.DefenseCurl,
            PBEMove.ThunderPunch,
            PBEMove.FirePunch,
            (PBEMove)210 // FuryCutter
        };
        static readonly Dictionary<int, PBESpecies> gen4SpeciesIndexToPBESpecies = new Dictionary<int, PBESpecies>
        {
            { 496, (PBESpecies)(386 | (1 << 0x10)) }, // Deoxys_Attack
            { 497, (PBESpecies)(386 | (2 << 0x10)) }, // Deoxys_Defense
            { 498, (PBESpecies)(386 | (3 << 0x10)) }, // Deoxys_Speed
            { 499, (PBESpecies)(413 | (1 << 0x10)) }, // Wormadam_Sandy
            { 500, (PBESpecies)(413 | (2 << 0x10)) }, // Wormadam_Trash
            { 501, PBESpecies.Giratina_Origin },
            { 502, (PBESpecies)(492 | (1 << 0x10)) }, // Shaymin_Sky
            // Not sure on the order of the Rotoms, but they all have the same level up moves & tm moves
            { 503, PBESpecies.Rotom_Fan },
            { 504, PBESpecies.Rotom_Frost },
            { 505, PBESpecies.Rotom_Heat },
            { 506, PBESpecies.Rotom_Mow },
            { 507, PBESpecies.Rotom_Wash }
        };
        static readonly PBEMove[] gen4TMHMs = new PBEMove[100]
        {
            (PBEMove)264, // FocusPunch
            PBEMove.DragonClaw,
            PBEMove.WaterPulse,
            PBEMove.CalmMind,
            PBEMove.Roar,
            PBEMove.Toxic,
            PBEMove.Hail,
            PBEMove.BulkUp,
            (PBEMove)331, // BulletSeed
            PBEMove.HiddenPower,
            PBEMove.SunnyDay,
            (PBEMove)269, // Taunt
            PBEMove.IceBeam,
            PBEMove.Blizzard,
            (PBEMove)63, // HyperBeam
            PBEMove.LightScreen,
            PBEMove.Protect,
            PBEMove.RainDance,
            PBEMove.GigaDrain,
            (PBEMove)219, // Safeguard
            PBEMove.Frustration,
            (PBEMove)76, // SolarBeam
            PBEMove.IronTail,
            PBEMove.Thunderbolt,
            PBEMove.Thunder,
            PBEMove.Earthquake,
            PBEMove.Return,
            PBEMove.Dig,
            PBEMove.Psychic,
            PBEMove.ShadowBall,
            PBEMove.BrickBreak,
            PBEMove.DoubleTeam,
            PBEMove.Reflect,
            PBEMove.ShockWave,
            PBEMove.Flamethrower,
            PBEMove.SludgeBomb,
            PBEMove.Sandstorm,
            PBEMove.FireBlast,
            PBEMove.RockTomb,
            PBEMove.AerialAce,
            (PBEMove)259, // Torment
            PBEMove.Facade,
            (PBEMove)290, // SecretPower
            PBEMove.Rest,
            (PBEMove)213, // Attract
            (PBEMove)168, // Thief
            PBEMove.SteelWing,
            (PBEMove)285, // SkillSwap
            (PBEMove)289, // Snatch
            PBEMove.Overheat,
            (PBEMove)355, // Roost
            PBEMove.FocusBlast,
            PBEMove.EnergyBall,
            (PBEMove)206, // FalseSwipe
            PBEMove.Brine,
            (PBEMove)374, // Fling
            PBEMove.ChargeBeam,
            (PBEMove)203, // Endure
            PBEMove.DragonPulse,
            PBEMove.DrainPunch,
            PBEMove.WillOWisp,
            PBEMove.SilverWind,
            (PBEMove)373, // Embargo
            PBEMove.Explosion,
            PBEMove.ShadowClaw,
            (PBEMove)371, // Payback
            (PBEMove)278, // Recycle
            (PBEMove)416, // GigaImpact
            PBEMove.RockPolish,
            PBEMove.Flash,
            PBEMove.StoneEdge,
            (PBEMove)419, // Avalanche
            PBEMove.ThunderWave,
            (PBEMove)360, // GyroBall
            PBEMove.SwordsDance,
            PBEMove.StealthRock,
            PBEMove.PsychUp,
            (PBEMove)445, // Captivate
            PBEMove.DarkPulse,
            PBEMove.RockSlide,
            PBEMove.XScissor,
            (PBEMove)214, // SleepTalk
            (PBEMove)363, // NaturalGift
            PBEMove.PoisonJab,
            PBEMove.DreamEater,
            PBEMove.GrassKnot,
            PBEMove.Swagger,
            (PBEMove)365, // Pluck
            (PBEMove)369, // Uturn
            PBEMove.Substitute,
            PBEMove.FlashCannon,
            PBEMove.TrickRoom,
            PBEMove.Cut,
            PBEMove.Fly,
            PBEMove.Surf,
            PBEMove.Strength,
            PBEMove.None, // Defog/Whirlpool - code will apply the right one
            PBEMove.RockSmash,
            PBEMove.Waterfall,
            PBEMove.RockClimb
        };
        // These tutor moves are copied from overlay_0005.bin address 0x2FF64 to ram address 0x02200CE4 on each map load (USA offsets)
        // The tutor compatibility is at the end of the table (0x3012C and 0x02200EAC [USA offsets]), starting with Bulbasaur and ending with Arceus (no forme entries), and each compatibility is a bitfield of 5 bytes
        // Each tutor move entry is 0xC bytes:
        // u16 moveId
        // u8 redShard
        // u8 blueShard
        // u8 yellowShard
        // u8 greenShard
        // u16 unk1
        // u32 areaId (0 = Route 212, 1 = Survival Area, 2 = Snowpoint City)
        static readonly PBEMove[] ptTutorMoves = new PBEMove[38]
        {
            PBEMove.Dive,
            PBEMove.MudSlap,
            (PBEMove)210, // FuryCutter
            PBEMove.IcyWind,
            (PBEMove)205, // Rollout
            PBEMove.ThunderPunch,
            PBEMove.FirePunch,
            PBEMove.Superpower,
            PBEMove.IcePunch,
            PBEMove.IronHead,
            PBEMove.AquaTail,
            PBEMove.OminousWind,
            PBEMove.GastroAcid,
            PBEMove.Snore,
            (PBEMove)180, // Spite,
            PBEMove.AirCutter,
            PBEMove.HelpingHand,
            PBEMove.Endeavor,
            (PBEMove)200, // Outrage
            PBEMove.AncientPower,
            PBEMove.Synthesis,
            PBEMove.SignalBeam,
            PBEMove.ZenHeadbutt,
            PBEMove.VacuumWave,
            PBEMove.EarthPower,
            PBEMove.GunkShot,
            (PBEMove)239, // Twister
            PBEMove.SeedBomb,
            PBEMove.IronDefense,
            (PBEMove)393, // MagnetRise
            (PBEMove)387, // LastResort
            (PBEMove)340, // Bounce
            (PBEMove)271, // Trick
            PBEMove.HeatWave,
            (PBEMove)282, // KnockOff
            PBEMove.SuckerPunch,
            PBEMove.Swift,
            (PBEMove)253 // Uproar
        };
        // These tutor moves are decompressed to memory (ram address 0x022093E0 in HG, 0x022093F0 in SS) on each map load (USA offsets)
        // Each tutor move entry is 0x4 bytes:
        // u16 moveId
        // u8 bpCost
        // u8 areaId (0 = Frontier Access [top left tutor], 1 = Frontier Access [top right tutor], 2 = Frontier Access [bottom right tutor], 3 = Ilex Forest [Headbutt tutor])
        static readonly PBEMove[] hgssTutorMoves = new PBEMove[52]
        {
            PBEMove.Dive,
            PBEMove.MudSlap,
            (PBEMove)210, // FuryCutter
            PBEMove.IcyWind,
            (PBEMove)205, // Rollout
            PBEMove.ThunderPunch,
            PBEMove.FirePunch,
            PBEMove.Superpower,
            PBEMove.IcePunch,
            PBEMove.IronHead,
            PBEMove.AquaTail,
            PBEMove.OminousWind,
            PBEMove.GastroAcid,
            PBEMove.Snore,
            (PBEMove)180, // Spite
            PBEMove.AirCutter,
            PBEMove.HelpingHand,
            PBEMove.Endeavor,
            (PBEMove)200, // Outrage
            PBEMove.AncientPower,
            PBEMove.Synthesis,
            PBEMove.SignalBeam,
            PBEMove.ZenHeadbutt,
            PBEMove.VacuumWave,
            PBEMove.EarthPower,
            PBEMove.GunkShot,
            (PBEMove)239, // Twister
            PBEMove.SeedBomb,
            PBEMove.IronDefense,
            (PBEMove)393, // MagnetRise
            (PBEMove)387, // LastResort
            (PBEMove)340, // Bounce
            (PBEMove)271, // Trick
            PBEMove.HeatWave,
            (PBEMove)282, // KnockOff
            PBEMove.SuckerPunch,
            PBEMove.Swift,
            (PBEMove)253, // Uproar
            PBEMove.SuperFang,
            PBEMove.PainSplit,
            PBEMove.StringShot,
            (PBEMove)366, // Tailwind
            (PBEMove)356, // Gravity
            (PBEMove)388, // WorrySeed
            (PBEMove)277, // MagicCoat
            (PBEMove)272, // RolePlay
            (PBEMove)215, // HealBell
            PBEMove.LowKick,
            (PBEMove)143, // SkyAttack
            (PBEMove)335, // Block
            (PBEMove)450, // BugBite
            PBEMove.Headbutt
        };
        static readonly Dictionary<int, PBESpecies> bwSpeciesIndexToPBESpecies = new Dictionary<int, PBESpecies>
        {
            { 650, (PBESpecies)(386 | (1 << 0x10)) }, // Deoxys_Attack
            { 651, (PBESpecies)(386 | (2 << 0x10)) }, // Deoxys_Defense
            { 652, (PBESpecies)(386 | (3 << 0x10)) }, // Deoxys_Speed
            { 653, (PBESpecies)(413 | (1 << 0x10)) }, // Wormadam_Sandy
            { 654, (PBESpecies)(413 | (2 << 0x10)) }, // Wormadam_Trash
            { 655, (PBESpecies)(492 | (1 << 0x10)) }, // Shaymin_Sky
            { 656, PBESpecies.Giratina_Origin },
            // Not sure on the order of the Rotoms, but they all have the same level up moves & tm moves
            { 657, PBESpecies.Rotom_Fan },
            { 658, PBESpecies.Rotom_Frost },
            { 659, PBESpecies.Rotom_Heat },
            { 660, PBESpecies.Rotom_Mow },
            { 661, PBESpecies.Rotom_Wash },
            // Not sure on the order of the Castforms, but they all have the same level up moves & tm moves
            { 662, (PBESpecies)(351 | (1 << 0x10)) }, // Castform_Rainy
            { 663, (PBESpecies)(351 | (2 << 0x10)) }, // Castform_Snowy
            { 664, (PBESpecies)(351 | (3 << 0x10)) }, // Castform_Sunny
            { 665, PBESpecies.Basculin_Red },
            { 666, PBESpecies.Darmanitan_Zen },
            { 667, PBESpecies.Meloetta_Pirouette }
        };
        static readonly Dictionary<int, PBESpecies> b2w2SpeciesIndexToPBESpecies = new Dictionary<int, PBESpecies>
        {
            { 685, (PBESpecies)(386 | (1 << 0x10)) }, // Deoxys_Attack
            { 686, (PBESpecies)(386 | (2 << 0x10)) }, // Deoxys_Defense
            { 687, (PBESpecies)(386 | (3 << 0x10)) }, // Deoxys_Speed
            { 688, (PBESpecies)(413 | (1 << 0x10)) }, // Wormadam_Sandy
            { 689, (PBESpecies)(413 | (2 << 0x10)) }, // Wormadam_Trash
            { 690, (PBESpecies)(492 | (1 << 0x10)) }, // Shaymin_Sky
            { 691, PBESpecies.Giratina_Origin },
            // Not sure on the order of the Rotoms, but they all have the same level up moves & tm moves
            { 692, PBESpecies.Rotom_Fan },
            { 693, PBESpecies.Rotom_Frost },
            { 694, PBESpecies.Rotom_Heat },
            { 695, PBESpecies.Rotom_Mow },
            { 696, PBESpecies.Rotom_Wash },
            // Not sure on the order of the Castforms, but they all have the same level up moves & tm moves
            { 697, (PBESpecies)(351 | (1 << 0x10)) }, // Castform_Rainy
            { 698, (PBESpecies)(351 | (2 << 0x10)) }, // Castform_Snowy
            { 699, (PBESpecies)(351 | (3 << 0x10)) }, // Castform_Sunny
            { 700, PBESpecies.Basculin_Red },
            { 701, PBESpecies.Darmanitan_Zen },
            { 702, PBESpecies.Meloetta_Pirouette },
            { 703, PBESpecies.Kyurem_White },
            { 704, PBESpecies.Kyurem_Black },
            { 705, PBESpecies.Keldeo_Resolute },
            { 706, PBESpecies.Tornadus_Therian },
            { 707, PBESpecies.Thundurus_Therian },
            { 708, PBESpecies.Landorus_Therian }
        };
        static readonly PBEMove[] gen5TMHMs = new PBEMove[101]
        {
            PBEMove.HoneClaws,
            PBEMove.DragonClaw,
            PBEMove.Psyshock,
            PBEMove.CalmMind,
            PBEMove.Roar,
            PBEMove.Toxic,
            PBEMove.Hail,
            PBEMove.BulkUp,
            PBEMove.Venoshock,
            PBEMove.HiddenPower,
            PBEMove.SunnyDay,
            (PBEMove)269, // Taunt
            PBEMove.IceBeam,
            PBEMove.Blizzard,
            (PBEMove)63, // HyperBeam
            PBEMove.LightScreen,
            PBEMove.Protect,
            PBEMove.RainDance,
            (PBEMove)477, // Telekinesis
            (PBEMove)219, // Safeguard
            PBEMove.Frustration,
            (PBEMove)76, // SolarBeam
            (PBEMove)479, // SmackDown
            PBEMove.Thunderbolt,
            PBEMove.Thunder,
            PBEMove.Earthquake,
            PBEMove.Return,
            PBEMove.Dig,
            PBEMove.Psychic,
            PBEMove.ShadowBall,
            PBEMove.BrickBreak,
            PBEMove.DoubleTeam,
            PBEMove.Reflect,
            PBEMove.SludgeWave,
            PBEMove.Flamethrower,
            PBEMove.SludgeBomb,
            PBEMove.Sandstorm,
            PBEMove.FireBlast,
            PBEMove.RockTomb,
            PBEMove.AerialAce,
            (PBEMove)259, // Torment
            PBEMove.Facade,
            PBEMove.FlameCharge,
            PBEMove.Rest,
            (PBEMove)213, // Attract
            (PBEMove)168, // Thief
            PBEMove.LowSweep,
            (PBEMove)496, // Round
            (PBEMove)497, // EchoedVoice
            PBEMove.Overheat,
            (PBEMove)502, // AllySwitch
            PBEMove.FocusBlast,
            PBEMove.EnergyBall,
            (PBEMove)206, // FalseSwipe
            PBEMove.Scald,
            (PBEMove)374, // Fling
            PBEMove.ChargeBeam,
            (PBEMove)507, // SkyDrop
            (PBEMove)510, // Incinerate
            (PBEMove)511, // Quash
            PBEMove.WillOWisp,
            PBEMove.Acrobatics,
            (PBEMove)373, // Embargo
            PBEMove.Explosion,
            PBEMove.ShadowClaw,
            (PBEMove)371, // Payback
            PBEMove.Retaliate,
            (PBEMove)416, // GigaImpact
            PBEMove.RockPolish,
            PBEMove.Flash,
            PBEMove.StoneEdge,
            (PBEMove)521, // VoltSwitch
            PBEMove.ThunderWave,
            (PBEMove)360, // GyroBall
            PBEMove.SwordsDance,
            PBEMove.StruggleBug,
            PBEMove.PsychUp,
            PBEMove.Bulldoze,
            PBEMove.FrostBreath,
            PBEMove.RockSlide,
            PBEMove.XScissor,
            (PBEMove)525, // DragonTail
            PBEMove.WorkUp,
            PBEMove.PoisonJab,
            PBEMove.DreamEater,
            PBEMove.GrassKnot,
            PBEMove.Swagger,
            (PBEMove)365, // Pluck
            (PBEMove)369, // Uturn
            PBEMove.Substitute,
            PBEMove.FlashCannon,
            PBEMove.TrickRoom,
            PBEMove.WildCharge,
            PBEMove.RockSmash,
            PBEMove.Snarl,
            PBEMove.Cut,
            PBEMove.Fly,
            PBEMove.Surf,
            PBEMove.Strength,
            PBEMove.Waterfall,
            PBEMove.Dive
        };
        static readonly PBEMove[] gen5FreeTutorMoves = new PBEMove[7]
        {
            (PBEMove)520, // GrassPledge
            (PBEMove)519, // FirePledge
            (PBEMove)518, // WaterPledge
            (PBEMove)338, // FrenzyPlant
            (PBEMove)307, // BlastBurn
            (PBEMove)308, // HydroCannon
            PBEMove.DracoMeteor
        };
        // These tutor moves are decompressed to memory (ram address 0x021D0B38 in B2, 0x021D0B6C in W2) on each map load (USA offsets)
        // For some reason, the location order in this table is different from the Pokémon's compatibility (this table is [Humilau,Driftveil,Nacrene,Lentimas] but in Pokémon data it is [Driftveil,Lentimas,Humilau,Nacrene])
        // Each tutor move entry is 0xC bytes:
        // u32 moveId
        // u32 shardCost
        // u32 indexInList
        static readonly PBEMove[][] b2w2TutorMoves = new PBEMove[4][]
        {
            new PBEMove[15] // Driftveil City
            {
                (PBEMove)450, // BugBite
                (PBEMove)343, // Covet
                PBEMove.SuperFang,
                (PBEMove)530, // DualChop
                PBEMove.SignalBeam,
                PBEMove.IronHead,
                PBEMove.SeedBomb,
                PBEMove.DrillRun,
                (PBEMove)340, // Bounce
                PBEMove.LowKick,
                PBEMove.GunkShot,
                (PBEMove)253, // Uproar
                PBEMove.ThunderPunch,
                PBEMove.FirePunch,
                PBEMove.IcePunch
            },
            new PBEMove[17] // Lentimas Town
            {
                (PBEMove)277, // MagicCoat
                (PBEMove)335, // Block
                PBEMove.EarthPower,
                PBEMove.FoulPlay,
                (PBEMove)356, // Gravity
                (PBEMove)393, // MagnetRise
                PBEMove.IronDefense,
                (PBEMove)387, // LastResort
                PBEMove.Superpower,
                PBEMove.Electroweb,
                PBEMove.IcyWind,
                PBEMove.AquaTail,
                PBEMove.DarkPulse,
                PBEMove.ZenHeadbutt,
                PBEMove.DragonPulse,
                PBEMove.HyperVoice,
                PBEMove.IronTail
            },
            new PBEMove[13] // Humilau City
            {
                (PBEMove)20, // Bind
                PBEMove.Snore,
                (PBEMove)282, // KnockOff
                PBEMove.Synthesis,
                PBEMove.HeatWave,
                (PBEMove)272, // RolePlay
                (PBEMove)215, // HealBell
                (PBEMove)366, // Tailwind
                (PBEMove)143, // SkyAttack
                PBEMove.PainSplit,
                PBEMove.GigaDrain,
                PBEMove.DrainPunch,
                (PBEMove)355 // Roost
            },
            new PBEMove[15] // Nacrene City
            {
                PBEMove.GastroAcid,
                (PBEMove)388, // WorrySeed
                (PBEMove)180, // Spite
                (PBEMove)495, // AfterYou
                PBEMove.HelpingHand,
                (PBEMove)271, // Trick
                (PBEMove)478, // MagicRoom
                (PBEMove)472, // WonderRoom
                PBEMove.Endeavor,
                (PBEMove)200, // Outrage
                (PBEMove)278, // Recycle
                (PBEMove)289, // Snatch
                PBEMove.StealthRock,
                (PBEMove)214, // SleepTalk
                (PBEMove)285 // SkillSwap
            }
        };

        // You must dump everything yourself
        // The GBA ROMs must all be USA v1.0
        // Colo and XD must be USA
        // DPPt dumps use overlay files which may or may not have different offsets depending on the region, so just keep in mind I have USA versions of each game
        // HGSS/Gen5 dumps should work across all regions
        //
        // Colo and XD level-up moves are in common.fsys/common_rel.fdat
        //
        // D, P, and Pt level-up move NARC is /poketool/personal/wotbl.narc (D and P have identical level-up move NARCs)
        // D, P, and Pt egg moves are in overlay/overlay_0005.bin
        // Pt TMHM moves are in the Pokémon data NARC which is /poketool/personal/pl_personal.narc (Pt changed no TMHM compatibility from DP so I use it alone)
        // Pt tutor compatibility is in overlay/overlay_0005.bin
        // HG and SS level-up move NARC is /a/0/3/3 (HG and SS have identical level-up move NARCs)
        // HG and SS TMHM moves are in the Pokémon data NARC which is /a/0/0/2 (HG and SS have identical Pokémon data NARCs)
        // HG and SS tutor compatibility is in /fielddata/wazaoshie/waza_oshie.bin (HG and SS have identical tutor compatibility)
        // HG and SS egg move NARC is /a/2/2/9 (HG and SS have identical egg move NARCs)
        //
        // B2 and W2 evolution NARC is /a/0/1/9 (B2 and W2 have identical evolution NARCs)
        // B, W, B2, and W2 level-up move NARC is /a/0/1/8 (B and W have identical level-up move NARCs) (B2 and W2 have identical level-up move NARCs)
        // B, W, B2, and W2 TMHM moves are in the Pokémon data NARC which is /a/0/1/6 (B and W have identical Pokémon data NARCs) (B2 and W2 have identical Pokémon data NARCs)
        // B2 and W2 tutor compatibility is in the Pokémon data NARC which is /a/0/1/6 (B2 and W2 have identical Pokémon data NARCs)
        // B and W egg move NARC is /a/1/2/3, B2 and W2 egg move NARC is /a/1/2/4 (B, W, B2, and W2 have identical egg move NARCs)
        //
        // TODO: XD - tutor
        // TODO: FRLG - Ultimate starter tutor moves
        // TODO: D, P, Pt - Free tutor moves
        // TODO: HG, SS - Free tutor moves (aside from headbutt)
        // TODO: Pichu & Volt Tackle (and check for other egg move special cases)
        // TODO: Share moves across formes
#pragma warning disable CS8321 // Local function is declared but never used
        public static void Dump()
        {
            //using (var writer = new StreamWriter(@"../../../../\PokemonBattleEngine\Data\PokemonData_Data.cs"))
            using (var writer = new StreamWriter(@"../../../\DumpedData\Dumped\PokemonData.txt")) // Temporary
            using (var r = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\R.gba"), Endianness.LittleEndian))
            using (var s = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\S.gba"), Endianness.LittleEndian))
            using (var fr = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\FR.gba"), Endianness.LittleEndian))
            using (var lg = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\LG.gba"), Endianness.LittleEndian))
            using (var e = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\E.gba"), Endianness.LittleEndian))
            using (var coloCommonRel = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\Colocommon_rel.fdat"), Endianness.BigEndian))
            using (var xdCommonRel = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\XDcommon_rel.fdat"), Endianness.BigEndian))
            {
                var dict = new Dictionary<PBESpecies, Pokemon>();
                void AddSpecies(PBESpecies species)
                {
                    if (!dict.ContainsKey(species))
                    {
                        dict.Add(species, new Pokemon());
                    }
                }
                void AddLevelUpMove(PBESpecies species, int level, PBEMove move, string flag)
                {
                    AddSpecies(species);
                    Tuple<int, PBEMove> key = dict[species].LevelUpMoves.Keys.SingleOrDefault(k => k.Item1 == level && k.Item2 == move);
                    if (key != null)
                    {
                        dict[species].LevelUpMoves[key] += $" | {flag}";
                    }
                    else
                    {
                        dict[species].LevelUpMoves.Add(Tuple.Create(level, move), flag);
                    }
                }
                void AddOtherMove(PBESpecies species, PBEMove move, string flag)
                {
                    AddSpecies(species);
                    if (dict[species].OtherMoves.ContainsKey(move))
                    {
                        dict[species].OtherMoves[move] += $" | {flag}";
                    }
                    else
                    {
                        dict[species].OtherMoves.Add(move, flag);
                    }
                }

                #region Pokémon Data

                using (var b2w2Pokedata = new NARC(@"../../../\DumpedData\B2W2Pokedata.narc"))
                using (var b2w2Evolution = new NARC(@"../../../\DumpedData\B2W2Evolution.narc"))
                {
                    for (int sp = 1; sp <= 708; sp++)
                    {
                        // Skip Egg, Bad Egg, and Pokéstar Studios Pokémon
                        if (sp <= 649 || sp >= 685)
                        {
                            PBESpecies species = b2w2SpeciesIndexToPBESpecies.ContainsKey(sp) ? b2w2SpeciesIndexToPBESpecies[sp] : (PBESpecies)sp;
                            AddSpecies(species);
                            using (var pokedata = new EndianBinaryReader(b2w2Pokedata.Files[sp], Endianness.LittleEndian))
                            using (var evolution = new EndianBinaryReader(b2w2Evolution.Files[sp], Endianness.LittleEndian))
                            {
                                Pokemon pkmn = dict[species];
                                // Pokedata
                                pkmn.HP = pokedata.ReadByte(0x0);
                                pkmn.Attack = pokedata.ReadByte(0x1);
                                pkmn.Defense = pokedata.ReadByte(0x2);
                                pkmn.SpAttack = pokedata.ReadByte(0x4);
                                pkmn.SpDefense = pokedata.ReadByte(0x5);
                                pkmn.Speed = pokedata.ReadByte(0x3);
                                pkmn.Type1 = gen5Types[pokedata.ReadByte(0x6)];
                                pkmn.Type2 = gen5Types[pokedata.ReadByte(0x7)];
                                if (pkmn.Type1 == pkmn.Type2)
                                {
                                    pkmn.Type2 = PBEType.None;
                                }
                                pkmn.GenderRatio = gen5GenderRatios[pokedata.ReadByte(0x12)];
                                for (int i = 0; i < 3; i++)
                                {
                                    var ability = (PBEAbility)pokedata.ReadByte(0x18 + i);
                                    if (ability != PBEAbility.None && !pkmn.Abilities.Contains(ability))
                                    {
                                        pkmn.Abilities.Add(ability);
                                    }
                                }
                                pkmn.Weight = pokedata.ReadUInt16(0x26) * 0.1;
                                // Evolution
                                for (int i = 0; i < 7; i++)
                                {
                                    ushort method = evolution.ReadUInt16();
                                    evolution.ReadUInt16(); // Param
                                    var evo = (PBESpecies)evolution.ReadUInt16();
                                    if (method != 0)
                                    {
                                        pkmn.Evolutions.Add(evo);
                                        AddSpecies(evo);
                                        List<PBESpecies> evosPreEvos = dict[evo].PreEvolutions;
                                        evosPreEvos.AddRange(pkmn.PreEvolutions);
                                        evosPreEvos.Add(species);
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Level Up Moves

                // Gen 3
                for (int sp = 1; sp <= 411; sp++)
                {
                    // Gen 2 Unown slots are ignored in gen 3
                    if (sp > 251 && sp < 277)
                    {
                        continue;
                    }
                    // It is the same in Ruby, Sapphire, Colo, and XD; the others have some differences
                    r.BaseStream.Position = 0x207BC8 + (sizeof(uint) * sp);
                    s.BaseStream.Position = 0x207B58 + (sizeof(uint) * sp);
                    fr.BaseStream.Position = 0x25D7B4 + (sizeof(uint) * sp);
                    lg.BaseStream.Position = 0x25D794 + (sizeof(uint) * sp);
                    e.BaseStream.Position = 0x32937C + (sizeof(uint) * sp);
                    coloCommonRel.BaseStream.Position = 0x123250 + (0x11C * sp) + 0xBA;
                    xdCommonRel.BaseStream.Position = 0x29DA8 + (0x124 * sp) + 0xC4;
                    void ReadGBALevelUpMoves(EndianBinaryReader reader, string flag)
                    {
                        PBESpecies species = gen3SpeciesIndexToPBESpecies[sp];
                        if (species == (PBESpecies)386)
                        {
                            if (reader == e)
                            {
                                species = (PBESpecies)(386 | (3 << 0x10)); // Deoxys_Speed
                            }
                            if (reader == lg)
                            {
                                species = (PBESpecies)(386 | (2 << 0x10)); // Deoxys_Defense
                            }
                            if (reader == fr)
                            {
                                species = (PBESpecies)(386 | (1 << 0x10)); // Deoxys_Attack
                            }
                        }
                        reader.BaseStream.Position = reader.ReadUInt32() - 0x8000000;
                        while (true)
                        {
                            ushort val = reader.ReadUInt16();
                            if (val == 0xFFFF)
                            {
                                break;
                            }
                            else
                            {
                                AddLevelUpMove(species, val >> 9, (PBEMove)(val & 0x1FF), flag);
                            }
                        }
                    }
                    ReadGBALevelUpMoves(r, "PBEMoveObtainMethod.LevelUp_RSColoXD");
                    //ReadGBALevelUpMoves(s, "PBEMoveObtainMethod.LevelUp_RSColoXD");
                    ReadGBALevelUpMoves(fr, "PBEMoveObtainMethod.LevelUp_FR");
                    ReadGBALevelUpMoves(lg, "PBEMoveObtainMethod.LevelUp_LG");
                    ReadGBALevelUpMoves(e, "PBEMoveObtainMethod.LevelUp_E");
                    void ReadGCLevelUpMoves(EndianBinaryReader reader, string flag)
                    {
                        PBESpecies species = gen3SpeciesIndexToPBESpecies[sp];
                        for (int i = 0; i < 17; i++)
                        {
                            int level = reader.ReadByte();
                            reader.ReadByte(); // Padding
                            var move = (PBEMove)reader.ReadUInt16();
                            if (move != PBEMove.None)
                            {
                                AddLevelUpMove(species, level, move, flag);
                            }
                        }
                    }
                    //ReadGCLevelUpMoves(coloCommonRel, "PBEMoveObtainMethod.LevelUp_RSColoXD");
                    //ReadGCLevelUpMoves(xdCommonRel, "PBEMoveObtainMethod.LevelUp_RSColoXD");
                }
                // Gen 4
                using (var dp = new NARC(@"../../../\DumpedData\DPLevelUp.narc"))
                using (var pt = new NARC(@"../../../\DumpedData\PtLevelUp.narc"))
                using (var hgss = new NARC(@"../../../\DumpedData\HGSSLevelUp.narc"))
                {
                    for (int sp = 1; sp <= 507; sp++)
                    {
                        // 494 is Egg, 495 is Bad Egg
                        if (sp == 494 || sp == 495)
                        {
                            continue;
                        }
                        PBESpecies species = gen4SpeciesIndexToPBESpecies.ContainsKey(sp) ? gen4SpeciesIndexToPBESpecies[sp] : (PBESpecies)sp;
                        void ReadLevelUpMoves(MemoryStream file, string flag)
                        {
                            using (var reader = new EndianBinaryReader(file, Endianness.LittleEndian))
                            {
                                while (true)
                                {
                                    ushort val = reader.ReadUInt16();
                                    if (val == 0xFFFF)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        AddLevelUpMove(species, val >> 9, (PBEMove)(val & 0x1FF), flag);
                                    }
                                }
                            }
                        }
                        // DP only has 0-500
                        if (sp <= 500)
                        {
                            ReadLevelUpMoves(dp.Files[sp], "PBEMoveObtainMethod.LevelUp_DP");
                        }
                        ReadLevelUpMoves(pt.Files[sp], "PBEMoveObtainMethod.LevelUp_Pt");
                        ReadLevelUpMoves(hgss.Files[sp], "PBEMoveObtainMethod.LevelUp_HGSS");
                    }
                }
                // Gen 5
                using (var bw = new NARC(@"../../../\DumpedData\BWLevelUp.narc"))
                using (var b2w2 = new NARC(@"../../../\DumpedData\B2W2LevelUp.narc"))
                {
                    for (int sp = 1; sp <= 708; sp++)
                    {
                        void ReadLevelUpMoves(MemoryStream file, bool isBW)
                        {
                            Dictionary<int, PBESpecies> d = isBW ? bwSpeciesIndexToPBESpecies : b2w2SpeciesIndexToPBESpecies;
                            PBESpecies species = d.ContainsKey(sp) ? d[sp] : (PBESpecies)sp;
                            using (var reader = new EndianBinaryReader(file, Endianness.LittleEndian))
                            {
                                while (true)
                                {
                                    uint val = reader.ReadUInt32();
                                    if (val == 0xFFFFFFFF)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        AddLevelUpMove(species, (int)(val >> 16), (PBEMove)(val & 0xFFFF), $"PBEMoveObtainMethod.LevelUp_{(isBW ? "BW" : "B2W2")}");
                                    }
                                }
                            }
                        }
                        // BW only has 0-667 (no Egg or Bad Egg)
                        if (sp <= 667)
                        {
                            ReadLevelUpMoves(bw.Files[sp], true);
                        }
                        // Skip Egg, Bad Egg, and Pokéstar Studios Pokémon in B2W2
                        if (sp <= 649 || sp >= 685)
                        {
                            ReadLevelUpMoves(b2w2.Files[sp], false);
                        }
                    }
                }

                #endregion

                #region TMHM Compatibility

                // Gen 3
                for (int sp = 1; sp <= 411; sp++)
                {
                    // Gen 2 Unown slots are ignored in gen 3
                    if (sp > 251 && sp < 277)
                    {
                        continue;
                    }
                    PBESpecies species = gen3SpeciesIndexToPBESpecies[sp];
                    // It is the same across all of gen 3, so I will only read one
                    r.BaseStream.Position = 0x1FD0F0 + (8 * sp);
                    s.BaseStream.Position = 0x1FD080 + (8 * sp);
                    fr.BaseStream.Position = 0x252BC8 + (8 * sp);
                    lg.BaseStream.Position = 0x252BA4 + (8 * sp);
                    e.BaseStream.Position = 0x31E898 + (8 * sp);
                    coloCommonRel.BaseStream.Position = 0x123250 + (0x11C * sp) + 0x34;
                    xdCommonRel.BaseStream.Position = 0x29DA8 + (0x124 * sp) + 0x34;
                    string GetFlag(int i)
                    {
                        return $"PBEMoveObtainMethod.{(i < 50 ? "TM" : "HM")}_RSFRLGEColoXD";
                    }
                    void ReadGBATMHM(EndianBinaryReader reader)
                    {
                        byte[] bytes = reader.ReadBytes(8);
                        for (int i = 0; i < gen3TMHMs.Length; i++)
                        {
                            if ((bytes[i / 8] & (1 << (i % 8))) != 0)
                            {
                                AddOtherMove(species, gen3TMHMs[i], GetFlag(i));
                            }
                        }
                    }
                    ReadGBATMHM(r);
                    //ReadGBATMHM(s);
                    //ReadGBATMHM(fr);
                    //ReadGBATMHM(lg);
                    //ReadGBATMHM(e);
                    void ReadGCTMHM(EndianBinaryReader reader)
                    {
                        for (int i = 0; i < gen3TMHMs.Length; i++)
                        {
                            if (reader.ReadBoolean())
                            {
                                AddOtherMove(species, gen3TMHMs[i], GetFlag(i));
                            }
                        }
                    }
                    //ReadGCTM(coloCommonRel);
                    //ReadGCTM(xdCommonRel);
                }
                // Gen 4
                using (var dppt = new NARC(@"../../../\DumpedData\PtPokedata.narc"))
                using (var hgss = new NARC(@"../../../\DumpedData\HGSSPokedata.narc"))
                {
                    for (int sp = 1; sp <= 507; sp++)
                    {
                        // 494 is Egg, 495 is Bad Egg
                        if (sp == 494 || sp == 495)
                        {
                            continue;
                        }
                        PBESpecies species = gen4SpeciesIndexToPBESpecies.ContainsKey(sp) ? gen4SpeciesIndexToPBESpecies[sp] : (PBESpecies)sp;
                        void ReadTMHMMoves(MemoryStream file, bool isDPPt)
                        {
                            using (var reader = new EndianBinaryReader(file, Endianness.LittleEndian))
                            {
                                byte[] bytes = reader.ReadBytes(13, 0x1C);
                                for (int i = 0; i < gen4TMHMs.Length; i++)
                                {
                                    if ((bytes[i / 8] & (1 << (i % 8))) != 0)
                                    {
                                        PBEMove move = gen4TMHMs[i];
                                        if (move == PBEMove.None)
                                        {
                                            move = isDPPt ? (PBEMove)432 : (PBEMove)250;
                                        }
                                        AddOtherMove(species, move, $"PBEMoveObtainMethod.{(i < 92 ? "TM" : "HM")}_{(isDPPt ? "DPPt" : "HGSS")}");
                                    }
                                }
                            }
                        }
                        ReadTMHMMoves(dppt.Files[sp], true);
                        ReadTMHMMoves(hgss.Files[sp], false);
                    }
                }
                // Gen 5
                using (var bw = new NARC(@"../../../\DumpedData\BWPokedata.narc"))
                using (var b2w2 = new NARC(@"../../../\DumpedData\B2W2Pokedata.narc"))
                {
                    for (int sp = 1; sp <= 708; sp++)
                    {
                        void ReadTMHMMoves(MemoryStream file, bool isBW)
                        {
                            Dictionary<int, PBESpecies> d = isBW ? bwSpeciesIndexToPBESpecies : b2w2SpeciesIndexToPBESpecies;
                            PBESpecies species = d.ContainsKey(sp) ? d[sp] : (PBESpecies)sp;
                            using (var reader = new EndianBinaryReader(file, Endianness.LittleEndian))
                            {
                                byte[] bytes = reader.ReadBytes(13, 0x28);
                                for (int i = 0; i < gen5TMHMs.Length; i++)
                                {
                                    if ((bytes[i / 8] & (1 << (i % 8))) != 0)
                                    {
                                        string flag;
                                        if (i < 95)
                                        {
                                            flag = $"PBEMoveObtainMethod.TM_{(isBW ? "BW" : "B2W2")}";
                                        }
                                        else
                                        {
                                            flag = "PBEMoveObtainMethod.HM_BWB2W2";
                                        }
                                        AddOtherMove(species, gen5TMHMs[i], flag);
                                    }
                                }
                            }
                        }
                        // BW only has 0-667 (no Egg or Bad Egg)
                        if (sp <= 667)
                        {
                            ReadTMHMMoves(bw.Files[sp], true);
                        }
                        // Skip Egg, Bad Egg, and Pokéstar Studios Pokémon in B2W2
                        if (sp <= 649 || sp >= 685)
                        {
                            ReadTMHMMoves(b2w2.Files[sp], false);
                        }
                    }
                }

                #endregion

                #region Move Tutor

                // Gen 3 - FRLGE
                for (int sp = 1; sp <= 411; sp++)
                {
                    // Gen 2 Unown slots are ignored in gen 3
                    if (sp > 251 && sp < 277)
                    {
                        continue;
                    }
                    PBESpecies species = gen3SpeciesIndexToPBESpecies[sp];
                    // It is the same in FR and LG, so I will only read one
                    fr.BaseStream.Position = 0x459B7E + (sizeof(ushort) * sp);
                    lg.BaseStream.Position = 0x45959E + (sizeof(ushort) * sp);
                    e.BaseStream.Position = 0x615048 + (sizeof(uint) * sp);
                    void ReadTutorMoves(uint val, PBEMove[] tutorMoves, string flag)
                    {
                        for (int i = 0; i < tutorMoves.Length; i++)
                        {
                            if ((val & (1u << i)) != 0)
                            {
                                AddOtherMove(species, tutorMoves[i], flag);
                            }
                        }
                    }
                    ReadTutorMoves(fr.ReadUInt16(), frlgTutorMoves, "PBEMoveObtainMethod.MoveTutor_FRLG");
                    //ReadTutorMoves(lg.ReadUInt16(), frlgTutorMoves, "PBEMoveObtainMethod.MoveTutor_FRLG");
                    ReadTutorMoves(e.ReadUInt32(), emeraldTutorMoves, "PBEMoveObtainMethod.MoveTutor_E");
                }
                // Gen 4
                using (var pt = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\Ptoverlay_0005.bin"), Endianness.LittleEndian))
                {
                    for (int sp = 1; sp <= 493; sp++)
                    {
                        var species = (PBESpecies)sp;
                        byte[] bytes = pt.ReadBytes(5, 0x3012C + (5 * (sp - 1)));
                        for (int i = 0; i < ptTutorMoves.Length; i++)
                        {
                            if ((bytes[i / 8] & (1 << (i % 8))) != 0)
                            {
                                AddOtherMove(species, ptTutorMoves[i], "PBEMoveObtainMethod.MoveTutor_Pt");
                            }
                        }
                    }
                }
                using (var hgss = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\HGSSTutor.bin"), Endianness.LittleEndian))
                {
                    for (int sp = 1; sp <= 505; sp++) // Includes formes, but not eggs
                    {
                        PBESpecies species = sp > 493 ? gen4SpeciesIndexToPBESpecies[sp + 2] : (PBESpecies)sp;
                        byte[] bytes = hgss.ReadBytes(8);
                        for (int i = 0; i < hgssTutorMoves.Length; i++)
                        {
                            if ((bytes[i / 8] & (1 << (i % 8))) != 0)
                            {
                                AddOtherMove(species, hgssTutorMoves[i], "PBEMoveObtainMethod.MoveTutor_HGSS");
                            }
                        }
                    }
                }
                // Gen 5
                using (var bw = new NARC(@"../../../\DumpedData\BWPokedata.narc"))
                using (var b2w2 = new NARC(@"../../../\DumpedData\B2W2Pokedata.narc"))
                {
                    for (int sp = 1; sp <= 708; sp++)
                    {
                        void ReadFreeTutorMoves(EndianBinaryReader reader, bool isBW)
                        {
                            Dictionary<int, PBESpecies> d = isBW ? bwSpeciesIndexToPBESpecies : b2w2SpeciesIndexToPBESpecies;
                            PBESpecies species = d.ContainsKey(sp) ? d[sp] : (PBESpecies)sp;
                            byte val = reader.ReadByte(0x38);
                            for (int i = 0; i < gen5FreeTutorMoves.Length; i++)
                            {
                                if ((val & (1 << i)) != 0)
                                {
                                    AddOtherMove(species, gen5FreeTutorMoves[i], $"PBEMoveObtainMethod.MoveTutor_{(isBW ? "BW" : "B2W2")}");
                                }
                            }
                        }
                        void ReadB2W2TutorMoves(EndianBinaryReader reader)
                        {
                            PBESpecies species = b2w2SpeciesIndexToPBESpecies.ContainsKey(sp) ? b2w2SpeciesIndexToPBESpecies[sp] : (PBESpecies)sp;
                            for (int i = 0; i < b2w2TutorMoves.Length; i++)
                            {
                                uint val = reader.ReadUInt32(0x3C + (sizeof(uint) * i));
                                for (int j = 0; j < b2w2TutorMoves[i].Length; j++)
                                {
                                    if ((val & (1u << j)) != 0)
                                    {
                                        AddOtherMove(species, b2w2TutorMoves[i][j], "PBEMoveObtainMethod.MoveTutor_B2W2");
                                    }
                                }
                            }
                        }
                        // BW only has 0-667 (no Egg or Bad Egg)
                        if (sp <= 667)
                        {
                            using (var reader = new EndianBinaryReader(bw.Files[sp], Endianness.LittleEndian))
                            {
                                ReadFreeTutorMoves(reader, true);
                            }
                        }
                        // Skip Egg, Bad Egg, and Pokéstar Studios Pokémon
                        if (sp <= 649 || sp >= 685)
                        {
                            using (var reader = new EndianBinaryReader(b2w2.Files[sp], Endianness.LittleEndian))
                            {
                                ReadFreeTutorMoves(reader, false);
                                ReadB2W2TutorMoves(reader);
                            }
                        }
                    }
                }

                #endregion

                #region Egg Moves

                // Gen 3 & Gen 4
                using (var d = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\Doverlay_0005.bin"), Endianness.LittleEndian))
                using (var p = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\Poverlay_0005.bin"), Endianness.LittleEndian))
                using (var pt = new EndianBinaryReader(File.OpenRead(@"../../../\DumpedData\Ptoverlay_0005.bin"), Endianness.LittleEndian))
                using (var hgssNARC = new NARC(@"../../../\DumpedData\HGSSEgg.narc"))
                using (var hgss = new EndianBinaryReader(hgssNARC.Files[0], Endianness.LittleEndian))
                {
                    // The table is the same in all five GBA games, so I will only read one
                    r.BaseStream.Position = 0x2091DC;
                    s.BaseStream.Position = 0x20916C;
                    fr.BaseStream.Position = 0x25EF0C;
                    lg.BaseStream.Position = 0x25EEEC;
                    e.BaseStream.Position = 0x32ADD8;
                    // The table is the same across DPPt, so I will only read one
                    d.BaseStream.Position = 0x20668;
                    p.BaseStream.Position = 0x20668;
                    pt.BaseStream.Position = 0x29222;
                    void ReadEggMoves(EndianBinaryReader reader, bool isGen3, string flag)
                    {
                        PBESpecies species = 0;
                        while (true)
                        {
                            ushort val = reader.ReadUInt16();
                            if (val == 0xFFFF)
                            {
                                break;
                            }
                            else if (val > 20000)
                            {
                                int speciesIndex = val - 20000;
                                species = isGen3 ? gen3SpeciesIndexToPBESpecies[speciesIndex] : (PBESpecies)speciesIndex;
                            }
                            else
                            {
                                AddOtherMove(species, (PBEMove)val, flag);
                            }
                        }
                    }
                    ReadEggMoves(r, true, "PBEMoveObtainMethod.EggMove_RSFRLGE");
                    //ReadEggMoves(s, true, "PBEMoveObtainMethod.EggMove_RSFRLGE");
                    //ReadEggMoves(fr, true, "PBEMoveObtainMethod.EggMove_RSFRLGE");
                    //ReadEggMoves(lg, true, "PBEMoveObtainMethod.EggMove_RSFRLGE");
                    //ReadEggMoves(e, true, "PBEMoveObtainMethod.EggMove_RSFRLGE");
                    ReadEggMoves(d, false, "PBEMoveObtainMethod.EggMove_DPPt");
                    //ReadEggMoves(p, false, "PBEMoveObtainMethod.EggMove_DPPt");
                    //ReadEggMoves(pt, false, "PBEMoveObtainMethod.EggMove_DPPt");
                    ReadEggMoves(hgss, false, "PBEMoveObtainMethod.EggMove_HGSS");
                }
                // Gen 5
                using (var bwb2w2 = new NARC(@"../../../\DumpedData\BWB2W2Egg.narc"))
                {
                    for (int sp = 1; sp <= 649; sp++)
                    {
                        using (var reader = new EndianBinaryReader(bwb2w2.Files[sp], Endianness.LittleEndian))
                        {
                            ushort numEggMoves = reader.ReadUInt16();
                            if (numEggMoves > 0)
                            {
                                var species = (PBESpecies)sp;
                                for (int i = 0; i < numEggMoves; i++)
                                {
                                    AddOtherMove(species, (PBEMove)reader.ReadUInt16(), "PBEMoveObtainMethod.EggMove_BWB2W2");
                                }
                            }
                        }
                    }
                }

                #endregion

                writer.WriteLine("using System;");
                writer.WriteLine("using System.Collections.Generic;");
                writer.WriteLine("using System.Collections.ObjectModel;");
                writer.WriteLine();
                writer.WriteLine("namespace Kermalis.PokemonBattleEngine.Data");
                writer.WriteLine("{");
                writer.WriteLine("    public sealed partial class PBEPokemonData");
                writer.WriteLine("    {");
                writer.WriteLine("        public static ReadOnlyDictionary<PBESpecies, PBEPokemonData> Data { get; } = new ReadOnlyDictionary<PBESpecies, PBEPokemonData>(new Dictionary<PBESpecies, PBEPokemonData>");
                writer.WriteLine("        {");
                KeyValuePair<PBESpecies, Pokemon> lastPkmn = dict.LastOrDefault();
                foreach (KeyValuePair<PBESpecies, Pokemon> pkmn in dict)
                {
                    writer.WriteLine("            {");
                    writer.WriteLine($"                PBESpecies.{pkmn.Key},");
                    writer.WriteLine("                new PBEPokemonData");
                    writer.WriteLine("                (");
                    writer.WriteLine($"                    {pkmn.Value.HP}, {pkmn.Value.Attack}, {pkmn.Value.Defense}, {pkmn.Value.SpAttack}, {pkmn.Value.SpDefense}, {pkmn.Value.Speed},");
                    writer.WriteLine($"                    PBEType.{pkmn.Value.Type1}, PBEType.{pkmn.Value.Type2}, PBEGenderRatio.{pkmn.Value.GenderRatio}, {pkmn.Value.Weight:N1},");
                    if (pkmn.Value.PreEvolutions.Count == 0)
                    {
                        writer.WriteLine($"                    Array.Empty<PBESpecies>(),");
                    }
                    else
                    {
                        writer.WriteLine($"                    new PBESpecies[] {{ {string.Join(", ", pkmn.Value.PreEvolutions.Select(a => $"PBESpecies.{a}"))} }},");
                    }
                    if (pkmn.Value.Evolutions.Count == 0)
                    {
                        writer.WriteLine($"                    Array.Empty<PBESpecies>(),");
                    }
                    else
                    {
                        writer.WriteLine($"                    new PBESpecies[] {{ {string.Join(", ", pkmn.Value.Evolutions.Select(a => $"PBESpecies.{a}"))} }},");
                    }
                    writer.WriteLine($"                    new PBEAbility[] {{ {string.Join(", ", pkmn.Value.Abilities.Select(a => $"PBEAbility.{a}"))} }},");
                    writer.WriteLine("                    new Tuple<PBEMove, int, PBEMoveObtainMethod>[]");
                    writer.WriteLine("                    {");
                    KeyValuePair<Tuple<int, PBEMove>, string> lastLevelUp = pkmn.Value.LevelUpMoves.LastOrDefault();
                    foreach (KeyValuePair<Tuple<int, PBEMove>, string> levelup in pkmn.Value.LevelUpMoves)
                    {
                        writer.WriteLine($"                        {(Enum.IsDefined(typeof(PBEMove), levelup.Key.Item2) ? string.Empty : "// ")}Tuple.Create(PBEMove.{levelup.Key.Item2}, {levelup.Key.Item1}, {levelup.Value}){(levelup.Equals(lastLevelUp) ? string.Empty : ",")}");
                    }
                    writer.WriteLine("                    },");
                    if (pkmn.Value.OtherMoves.Count == 0)
                    {
                        writer.WriteLine("                    Array.Empty<Tuple<PBEMove, PBEMoveObtainMethod>>()");
                    }
                    else
                    {
                        writer.WriteLine("                    new Tuple<PBEMove, PBEMoveObtainMethod>[]");
                        writer.WriteLine("                    {");
                        KeyValuePair<PBEMove, string> lastOther = pkmn.Value.OtherMoves.LastOrDefault();
                        foreach (KeyValuePair<PBEMove, string> other in pkmn.Value.OtherMoves)
                        {
                            writer.WriteLine($"                        {(Enum.IsDefined(typeof(PBEMove), other.Key) ? string.Empty : "// ")}Tuple.Create(PBEMove.{other.Key}, {other.Value}){(other.Equals(lastOther) ? string.Empty : ",")}");
                        }
                        writer.WriteLine("                    }");
                    }
                    writer.WriteLine("                )");
                    writer.WriteLine($"            }}{(pkmn.Equals(lastPkmn) ? string.Empty : ", ")}");
                }
                writer.WriteLine("        });");
                writer.WriteLine("    }");
                writer.WriteLine("}");
                writer.WriteLine();
            }
        }
#pragma warning restore CS8321 // Local function is declared but never used
    }
}
