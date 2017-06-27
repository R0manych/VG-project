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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoriesRepository;

        public CategoryService(ICategoryRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public void Add(ViewModels.CategoryViewModel categoryViewModel)
        {
            try
            {
                Category category = MapFromViewModel(categoryViewModel);
                _categoriesRepository.Add(category);
                categoryViewModel.CategoryID = category.CategoryID;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void Delete(int Id)
        {
            try
            {
                Category category = _categoriesRepository.GetById(Id);
                _categoriesRepository.Delete(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(ViewModels.CategoryViewModel categoryViewModel)
        {
            try
            {
                Category category = MapFromViewModel(categoryViewModel);
                _categoriesRepository.Edit(category);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ViewModels.CategoryViewModel> GetAll()
        {
                List<ViewModels.CategoryViewModel> catViewModels = new List<ViewModels.CategoryViewModel>();
                foreach (Category category in _categoriesRepository.GetAll())
                {
                    ViewModels.CategoryViewModel categoryViewModel = MapToViewModel(category);
                    catViewModels.Add(categoryViewModel);
                }
                return catViewModels;
        }

        public ViewModels.CategoryViewModel GetById(int id)
        {

                Category category = _categoriesRepository.GetById(id);
                ViewModels.CategoryViewModel categoryViewModel = MapToViewModel(category);
                return categoryViewModel;
        }

        public ViewModels.CategoryViewModel GetByImg(int id)
        {
            Category category = _categoriesRepository.GetByImg(id);
            ViewModels.CategoryViewModel categoryViewModel = MapToViewModel(category);
            return categoryViewModel;
        }

        private ViewModels.CategoryViewModel MapToViewModel(Category category)
        {
            ViewModels.CategoryViewModel categoryViewModel = Mapper.Map<ViewModels.CategoryViewModel>(category);
            return categoryViewModel;
        }

        private Category MapFromViewModel(ViewModels.CategoryViewModel categoryViewModel)
        {
            Category category  = Mapper.Map<Category>(categoryViewModel);
            return category;
        }

        public void SetImage(int catId, int imgId)
        {
            Category category = _categoriesRepository.GetById(catId);
            category.ImageID = imgId;
            _categoriesRepository.Edit(category);
        }

        public void RemoveImage(int catId)
        {
            Category ctgr = _categoriesRepository.GetByImg(catId);
            ctgr.ImageID = null;
            _categoriesRepository.Edit(ctgr);
        }
    }
}
