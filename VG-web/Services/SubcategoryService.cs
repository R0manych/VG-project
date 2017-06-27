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
using VG_web.ViewModels;

namespace VG_web.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoriesRepository;

        public SubcategoryService(ISubcategoryRepository subcategoriesRepository)
        {
            _subcategoriesRepository = subcategoriesRepository;
        }

        public void Add(SubcategoryViewModel subcategoryViewModel)
        {
            Subcategory subcategory = MapFromViewModel(subcategoryViewModel);
            _subcategoriesRepository.Add(subcategory);
            subcategoryViewModel.SubcategoryID = subcategory.SubcategoryID;
        }

        public void Delete(int Id)
        {
            Subcategory subcategory = _subcategoriesRepository.GetById(Id);
            _subcategoriesRepository.Delete(subcategory);
        }

        public void Edit(SubcategoryViewModel subcategoryViewModel)
        {
            Subcategory subcategory = MapFromViewModel(subcategoryViewModel);
            _subcategoriesRepository.Edit(subcategory);
        }

        public IEnumerable<SubcategoryViewModel> GetAll()
        {
            List<SubcategoryViewModel> subcatViewModels = new List<SubcategoryViewModel>();
            foreach (Subcategory subcategory in _subcategoriesRepository.GetAll())
            {
                SubcategoryViewModel subcategoryViewModel = MapToViewModel(subcategory);
                subcatViewModels.Add(subcategoryViewModel);
            }
            return subcatViewModels;
        }

        public SubcategoryViewModel GetById(int id)
        {
            Subcategory subcategory = _subcategoriesRepository.GetById(id);
            SubcategoryViewModel subcategoryViewModel = MapToViewModel(subcategory);
            return subcategoryViewModel;
        }

        public SubcategoryViewModel GetByImg(int id)
        {
            Subcategory subcategory = _subcategoriesRepository.GetByImg(id);
            SubcategoryViewModel subcategoryViewModel = MapToViewModel(subcategory);
            return subcategoryViewModel;
        }

        public void SetImage(int subcatId, int imgId)
        {
            Subcategory subcategory = _subcategoriesRepository.GetById(subcatId);
            subcategory.ImageID = imgId;
            _subcategoriesRepository.Edit(subcategory);
        }

        private SubcategoryViewModel MapToViewModel(Subcategory subcategory)
        {
            SubcategoryViewModel subcategoryViewModel = Mapper.Map<SubcategoryViewModel>(subcategory);
            return subcategoryViewModel;
        }

        private Subcategory MapFromViewModel(SubcategoryViewModel subcategoryViewModel)
        {
            Subcategory subcategory  = Mapper.Map<Subcategory>(subcategoryViewModel);
            return subcategory;
        }

        public void RemoveImage(int subcatId)
        {
            Subcategory subcategory = _subcategoriesRepository.GetById(subcatId);
            subcategory.ImageID = null;
            _subcategoriesRepository.Edit(subcategory);
        }
    }
}
