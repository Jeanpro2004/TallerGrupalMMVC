using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TallerGrupalMMVC.Models;

namespace TallerGrupalMMVC.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TeamMember> _teamMembers;

        public ObservableCollection<TeamMember> TeamMembers
        {
            get => _teamMembers;
            set
            {
                if (_teamMembers != value)
                {
                    _teamMembers = value;
                    OnPropertyChanged();
                }
            }
        }

        public AboutViewModel()
        {
            LoadTeamMembers();
        }

        private void LoadTeamMembers()
        {
            TeamMembers = new ObservableCollection<TeamMember>
            {
                new TeamMember
                {
                    Name = "Johann Calva",
                    Age = 21,
                    FavoriteSport = "Fútbol",
                    PhotoPath = "https://purina.cl/sites/default/files/2025-04/razas-de-gatos.jpg", // Coloca tus imágenes en Resources/Images/
                    Role = "Desarrollador Frontend",
                    Description = "Apasionado por el desarrollo móvil y las interfaces de usuario."
                },
                new TeamMember
                {
                    Name = "Mathias Ruales",
                    Age = 22,
                    FavoriteSport = "Taekwando",
                    PhotoPath = "https://i.blogs.es/fed92e/topicochinos/450_1000.webp",
                    Role = "Desarrolladora Backend",
                    Description = "Especialista en APIs y arquitectura de software."
                },
                new TeamMember
                {
                    Name = "Jean Rodriguez",
                    Age = 20,
                    FavoriteSport = "Baloncesto",
                    PhotoPath = "https://phantom-marca.unidadeditorial.es/1e0f14f4f6518b0e3fd9926d3c91bcab/resize/828/f/jpg/assets/multimedia/imagenes/2024/10/18/17292382828020.jpg",
                    Role = "Diseñador UX/UI",
                    Description = "Creativo enfocado en experiencias de usuario memorables."
                },
          
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}