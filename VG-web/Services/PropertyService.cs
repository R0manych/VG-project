using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibDatabase.DatabaseContext;
using System.Linq.Expressions;
using LibDatabase.Interface;
using VG_web.Interface.ServiceInterfaces;
using AutoMapper;

namespace VG_web.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertiesRepository;
        private readonly ISubcategoryPropertyRepository _subcatPropRepository;

        public PropertyService(IPropertyRepository propertiesRepository, ISubcategoryPropertyRepository subcatPropRepository)
        {
            _propertiesRepository = propertiesRepository;
            _subcatPropRepository = subcatPropRepository;
        }

        public void Add(ViewModels.PropertyViewModel propertyViewModel)
        {
            Property property = MapFromViewModel(propertyViewModel);
            _propertiesRepository.Add(property);
        }

        public void Delete(int Id)
        {
            Property property = _propertiesRepository.GetById(Id);
            _propertiesRepository.Delete(property);
        }

        public void Edit(ViewModels.PropertyViewModel propertyViewModel)
        {
            Property property = MapFromViewModel(propertyViewModel);
            _propertiesRepository.Edit(property);
        }

        public IEnumerable<ViewModels.PropertyViewModel> GetAll()
        {
            List<ViewModels.PropertyViewModel> propViewModels = new List<ViewModels.PropertyViewModel>();
            foreach (Property property in _propertiesRepository.GetAll())
            {
                ViewModels.PropertyViewModel propertyViewModel = MapToViewModel(property);
                propViewModels.Add(propertyViewModel);
            }
            return propViewModels;
        }

        public ViewModels.PropertyViewModel GetById(int id)
        {
            Property property = _propertiesRepository.GetById(id);
            ViewModels.PropertyViewModel propertyViewModel = MapToViewModel(property);
            return propertyViewModel;
        }

        private ViewModels.PropertyViewModel MapToViewModel(Property property)
        {
            ViewModels.PropertyViewModel propertyViewModel = Mapper.Map<ViewModels.PropertyViewModel>(property);
            return propertyViewModel;
        }

        private Property MapFromViewModel(ViewModels.PropertyViewModel propertyViewModel)
        {
            Property property = Mapper.Map<Property>(propertyViewModel);
            return property;
        }
    }
}
