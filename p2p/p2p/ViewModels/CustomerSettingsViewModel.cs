using p2p.Helpers;
using p2p.Models;
using p2p.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace p2p.ViewModels
{
    public class CustomerSettingsViewModel
    {
        public Action<string[]> DisplaySelected;
        private IBackendProxy _backendProxy;
        private IBackendSessionManager _backendSessionManager;

        public CustomerSettingsViewModel(IBackendProxy backendProxy, IBackendSessionManager backendSessionManager)
        {
            _backendProxy = backendProxy;
            _backendSessionManager = backendSessionManager;

           

            DataList =  new List<SelectableData<CategoriesData>>()
            {
                new SelectableData<CategoriesData>() { Data = new CategoriesData() {Id = 1, ParentId=0, Name = "Test1", Description = "Description1" }, Selected = true },
                new SelectableData<CategoriesData>() { Data = new CategoriesData() {Id = 2, ParentId=1, Name = "Test2", Description = "Description2" } },
                new SelectableData<CategoriesData>() { Data = new CategoriesData() {Id = 3, ParentId=0, Name = "Test3", Description = "Description3" } },
                new SelectableData<CategoriesData>() { Data = new CategoriesData() {Id = 4, ParentId=3, Name = "Test4", Description = "Description4" } },
                new SelectableData<CategoriesData>() { Data = new CategoriesData() {Id = 5, ParentId=0, Name = "Test5", Description = "Description5" }, Selected = true }
            }; 
        }

        public async void OnAppearing()
        {
            var data =  await _backendProxy.GetCategoriesAsync(_backendSessionManager.Session.AccessToken);
        }

        // As example if you need to convert
        //private void LoadData(List<CategoriesData> data)
        //{
        //	var list = new List<SelectableData<CategoriesData>>();

        //	foreach (var item in data)
        //		list.Add(new SelectableData<CategoriesData>() { Data = item });

        //	DataList = list;
        //}

        public List<SelectableData<CategoriesData>> DataList { get; set; }

        public List<SelectableData<CategoriesData>> GetNewData()
        {
            var list = new List<SelectableData<CategoriesData>>();

            foreach (var data in DataList)
                list.Add(new SelectableData<CategoriesData>() { Data = data.Data.Clone(), Selected = data.Selected });

            return list;
        }


        
        public ICommand FinishCommand
        {
            get
            {
                return new Command(() =>
                {
                    var selected = GetNewData();
                    List<string> selectionStrings = new List<string>();
                    foreach( var s in selected)
                    {
                        if (s.Selected)
                        {
                            selectionStrings.Add(s.Data.Name);
                          
                        }
                    }
                    DisplaySelected(selectionStrings.ToArray());
                });
            }

        }
       

    }
}
