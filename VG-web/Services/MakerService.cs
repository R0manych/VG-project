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
    public class MakerService : IMakerService
    {
        private readonly IMakerRepository _makersRepository;

        public MakerService(IMakerRepository makersRepository)
        {
            _makersRepository = makersRepository;
        }

        public void Add(MakerViewModel makerViewModel)
        {
            Maker maker = MapFromViewModel(makerViewModel);
            _makersRepository.Add(maker);
        }

        public void Delete(int Id)
        {
            Maker maker = _makersRepository.GetById(Id);
            _makersRepository.Delete(maker);
        }

        public void Edit(MakerViewModel makerViewModel)
        {
            Maker maker = MapFromViewModel(makerViewModel);
            _makersRepository.Edit(maker);
        }

        public IEnumerable<MakerViewModel> GetAll()
        {
            List<MakerViewModel> mkrViewModels = new List<MakerViewModel>();
            foreach (Maker maker in _makersRepository.GetAll())
            {
                MakerViewModel makerViewModel = MapToViewModel(maker);
                mkrViewModels.Add(makerViewModel);
            }
            return mkrViewModels;
        }

        public ViewModels.MakerViewModel GetById(int id)
        {
            Maker maker = _makersRepository.GetById(id);
            MakerViewModel makerViewModel = MapToViewModel(maker);
            return makerViewModel;
        }

        public ViewModels.MakerViewModel GetByImg(int id)
        {
            Maker maker = _makersRepository.GetByImg(id);
            MakerViewModel makerViewModel = MapToViewModel(maker);
            return makerViewModel;
        }

        private MakerViewModel MapToViewModel(Maker maker)
        {
            MakerViewModel makerViewModel = Mapper.Map<MakerViewModel>(maker);
            return makerViewModel;
        }

        private Maker MapFromViewModel(MakerViewModel makerViewModel)
        {
            Maker maker = Mapper.Map<Maker>(makerViewModel);
            return maker;
        }

        public void SetImage(int makerId, int imgId)
        {
            Maker maker = _makersRepository.GetById(makerId);
            maker.ImageID = imgId;
            _makersRepository.Edit(maker);
        }

        public void RemoveImage(int mkrId)
        {
            Maker maker = _makersRepository.GetById(mkrId);
            maker.ImageID = null;
            _makersRepository.Edit(maker);
        }
    }
}
