using AllShow.Interface;
using AllShow.Models;
using AllShowService.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWorksPlus _unitOfWork;
        public RefreshTokenService(IUnitOfWorksPlus unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void CreateRefreshToken(RefreshToken refreshToken)
        {
            _unitOfWork.RefreshTokenRepository.Insert(refreshToken);
            _unitOfWork.SaveChanges();
        }
        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            _unitOfWork.RefreshTokenRepository.Update(refreshToken);
            _unitOfWork.SaveChanges();
        }
    } 
}
