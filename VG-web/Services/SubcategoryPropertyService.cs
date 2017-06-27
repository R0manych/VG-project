using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using VG_web.Interface.ServiceInterfaces;
using LibDatabase.DatabaseContext;
using LibDatabase.Interface;
using AutoMapper;

namespace VG_web.Services
{
    public class SubcategoryPropertyService : ISubcategoryPropertyService
    {
        private readonly ISubcategoryPropertyRepository _subcatPropertyRepository;

        public SubcategoryPropertyService(ISubcategoryPropertyRepository subcatPropertyRepository)
        {
            _subcatPropertyRepository = subcatPropertyRepository;
        }

        public void Add(ViewModels.SubcategoryPropertyViewModel subcatPropViewModel)
        {
            if (!(_subcatPropertyRepository.GetAll().Where(pd => pd.PropertyID == subcatPropViewModel.PropertyID && pd.SubcategoryID == subcatPropViewModel.SubcategoryID).Any()))
            {
                SubcategoryProperty subcatProperty = MapFromViewModel(subcatPropViewModel);
                _subcatPropertyRepository.Add(subcatProperty);
            }
        }

        public void Delete(int Id )
        {
            SubcategoryProperty subcatProperty = _subcatPropertyRepository.GetById(Id);
            _subcatPropertyRepository.Delete(subcatProperty);
        }

        public void Edit(ViewModels.SubcategoryPropertyViewModel subcatPropViewModel)
        {
            SubcategoryProperty subcatProperty = MapFromViewModel(subcatPropViewModel);
            _subcatPropertyRepository.Edit(subcatProperty);
        }

        public IEnumerable<ViewModels.SubcategoryPropertyViewModel> GetAll()
        {
            List<ViewModels.SubcategoryPropertyViewModel> subcatPropViewModels = new List<ViewModels.SubcategoryPropertyViewModel>();
            foreach (SubcategoryProperty subcatProperty in _subcatPropertyRepository.GetAll())
            {
                ViewModels.SubcategoryPropertyViewModel subcatPropViewModel = MapToViewModel(subcatProperty);
                subcatPropViewModels.Add(subcatPropViewModel);
            }
            return subcatPropViewModels;
        }

        public ViewModels.SubcategoryPropertyViewModel GetById(int id)
        {
            SubcategoryProperty subcatProperty = _subcatPropertyRepository.GetById(id);
            ViewModels.SubcategoryPropertyViewModel subcatPropViewModel = MapToViewModel(subcatProperty);
            return subcatPropViewModel;
        }

        private ViewModels.SubcategoryPropertyViewModel MapToViewModel(SubcategoryProperty subcatProperty)
        {
            ViewModels.SubcategoryPropertyViewModel subcatPropViewModel = Mapper.Map<ViewModels.SubcategoryPropertyViewModel>(subcatProperty);
            return subcatPropViewModel;
        }

        private SubcategoryProperty MapFromViewModel(ViewModels.SubcategoryPropertyViewModel subcatPropViewModel)
        {
            SubcategoryProperty subcatProperty = Mapper.Map<SubcategoryProperty>(subcatPropViewModel);
            return subcatProperty;
        }
    }
}
