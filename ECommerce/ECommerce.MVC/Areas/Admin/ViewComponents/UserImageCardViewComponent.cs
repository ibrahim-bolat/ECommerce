﻿using AutoMapper;
using ECommerce.Business.Abstract;
using ECommerce.Business.Dtos.UserDtos;
using ECommerce.Business.Dtos.UserImageDtos;
using ECommerce.Entities.Concrete.Identity.Entities;
using ECommerce.Shared.Utilities.ComplexTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Admin.ViewComponents;

[ViewComponent]
public class UserImageCardViewComponent : ViewComponent
    {
        private readonly IUserImageService _userImageService;
        private readonly IMapper _mapper;

        public UserImageCardViewComponent(IUserImageService userImageService,IMapper mapper)
        {
            _userImageService = userImageService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId)
        {
            if (userId > 0)
            {
                var dresult = await _userImageService.GetAllByUserIdAsync(userId);
                if (dresult.ResultStatus==ResultStatus.Success)
                {
                    ViewBag.UserId = userId;
                    return View(dresult.Data);
                }
            }
            return View();
        }
    }
