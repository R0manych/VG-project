using AutoMapper;
using LibDatabase.DatabaseContext;
using LibDatabase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VG_web.Interface.ServiceInterfaces;
using VG_web.ViewModels;

namespace VG_web.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imagesRepository;

        public ImageService(IImageRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }

        public void Add(ImageViewModel imageViewModel)
        {
            Image image = MapFromViewModel(imageViewModel);
            image.UI = Guid.NewGuid();
            _imagesRepository.Add(image);
            imageViewModel.ImageID = image.ImageID;
        }

        public void Delete(int Id)
        {
            Image Image = _imagesRepository.GetById(Id);
            _imagesRepository.Delete(Image);
        }

        public void Edit(ImageViewModel imageViewModel)
        {
            Image image = MapFromViewModel(imageViewModel);
            image.UI = Guid.NewGuid();
            _imagesRepository.Edit(image);
        }

        public IEnumerable<ImageViewModel> GetAll()
        {
            List<ViewModels.ImageViewModel> imgViewModels = new List<ViewModels.ImageViewModel>();
            foreach (Image Image in _imagesRepository.GetAll())
            {
                ImageViewModel imageViewModel = MapToViewModel(Image);
                imgViewModels.Add(imageViewModel);
            }
            return imgViewModels;
        }

        public ImageViewModel GetById(int id)
        {
            Image Image = _imagesRepository.GetById(id);
            ImageViewModel imageViewModel = MapToViewModel(Image);
            return imageViewModel;
        }

        private ImageViewModel MapToViewModel(Image image)
        {
            ImageViewModel imageViewModel = Mapper.Map<ImageViewModel>(image);
            return imageViewModel;
        }

        private Image MapFromViewModel(ImageViewModel imageViewModel)
        {
            Image image = Mapper.Map<Image>(imageViewModel);
            return image;
        }
    }
}
