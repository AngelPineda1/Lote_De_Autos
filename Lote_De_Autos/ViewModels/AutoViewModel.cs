using Lote_De_Autos.Data;
using Lote_De_Autos.Models;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lote_De_Autos.ViewModels
{
    public class AutoViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Auto> Autos { get; set; }
        AutoDbContext data=new();

        public event PropertyChangedEventHandler PropertyChanged;

        public Auto NuevoAuto { get; set; } = new();
        public ICommand AgregarCommand => new AsyncCommand(Agregar);
        public ICommand ActualizarCommand => new AsyncCommand(Actualizar);
        public ICommand DetallesCommand => new AsyncCommand<int>(Detalles);
        public ICommand EliminarCommand => new AsyncCommand<int>(Eliminar);

        private async Task Detalles(int id)
        {
            NuevoAuto = await data.GetById(id);
            OnPropertyChanged(nameof(NuevoAuto));
        }

        private async Task Actualizar()
        {
           var autoact= await data.Actualizar(NuevoAuto);
            if(autoact != null)
            {
                llenarAutos();

                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }


        private async Task Eliminar(int id)
        {
            var eliminado = await data.Eliminar(id);
            if (eliminado)
            {
                llenarAutos();
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public AutoViewModel()
        {
            llenarAutos();
        }

        private async void llenarAutos()
        {
            Autos = new(await data.GetAll());

        }

        private async Task Agregar()
        {
            await data.Add(NuevoAuto);
            llenarAutos();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        

        public void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
