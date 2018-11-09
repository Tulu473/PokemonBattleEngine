﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Kermalis.PokemonBattleEngine.Data;
using System.ComponentModel;

namespace Kermalis.PokemonBattleEngineClient.Views
{
    class BattleView : UserControl, INotifyPropertyChanged
    {
        void OnPropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        public new event PropertyChangedEventHandler PropertyChanged;

        string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
                IsMessageBoxVisible = !string.IsNullOrWhiteSpace(message);
                OnPropertyChanged(nameof(IsMessageBoxVisible));
            }
        }
        bool IsMessageBoxVisible { get; set; }

        public BattleClient Client;

        public BattleView()
        {
            AvaloniaXamlLoader.Load(this);
            DataContext = this;

            Name = "Battle0"; // Temporary
        }

        // pkmn.FieldPosition must be changed before calling this
        public void PokemonPositionChanged(PPokemon pkmn, PFieldPosition oldPosition)
        {
            PokemonView view;
            if (oldPosition != PFieldPosition.None)
            {
                view = this.FindControl<PokemonView>($"Battler{(pkmn.Local ? 0 : 1)}_{oldPosition}");
                view.PokemonChanged();
            }
            if (pkmn.FieldPosition != PFieldPosition.None)
            {
                view = this.FindControl<PokemonView>($"Battler{(pkmn.Local ? 0 : 1)}_{pkmn.FieldPosition}");
                view.Pokemon = pkmn;
            }

            switch (Client.BattleStyle)
            {
                case PBattleStyle.Single:
                    this.FindControl<PokemonView>("Battler0_Center").Location = new Point(75, 53);

                    this.FindControl<PokemonView>("Battler1_Center").Location = new Point(284, 8);
                    break;
                case PBattleStyle.Double:
                    this.FindControl<PokemonView>("Battler0_Left").Location = new Point(-37, 43);
                    this.FindControl<PokemonView>("Battler0_Right").Location = new Point(168, 54);

                    this.FindControl<PokemonView>("Battler1_Right").Location = new Point(242, 9);
                    this.FindControl<PokemonView>("Battler1_Left").Location = new Point(332, 15);
                    break;
                case PBattleStyle.Triple:
                    this.FindControl<PokemonView>("Battler0_Left").Location = new Point(-53, 51);
                    this.FindControl<PokemonView>("Battler0_Center").Location = new Point(92, 31);
                    this.FindControl<PokemonView>("Battler0_Right").Location = new Point(221, 76);

                    this.FindControl<PokemonView>("Battler1_Right").Location = new Point(209, -1);
                    this.FindControl<PokemonView>("Battler1_Center").Location = new Point(282, 16);
                    this.FindControl<PokemonView>("Battler1_Left").Location = new Point(362, 8);
                    break;
                case PBattleStyle.Rotation:
                    this.FindControl<PokemonView>("Battler0_Left").Location = new Point(-46, 384); // Hidden
                    this.FindControl<PokemonView>("Battler0_Center").Location = new Point(52, 72);
                    this.FindControl<PokemonView>("Battler0_Right").Location = new Point(228, 384); // Hidden

                    this.FindControl<PokemonView>("Battler1_Right").Location = new Point(211, -34);
                    this.FindControl<PokemonView>("Battler1_Center").Location = new Point(282, 16);
                    this.FindControl<PokemonView>("Battler1_Left").Location = new Point(421, -24);
                    break;
            }
        }
    }
}
