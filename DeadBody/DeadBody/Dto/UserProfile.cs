using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeadBody.Models.db;

namespace DeadBody.Dto
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //添加映射關係，處理源類型與映射目標類型屬性名稱不一致的問題
            //參數一：源類型，參數二：目標映射類型
            CreateMap<User, UserDto>()
                .ForMember(target => target.UserId,
                    opt => opt.MapFrom(src => src.UserId))

                .ForMember(target => target.UserAccount,
                opt => opt.MapFrom(src => src.UserAccount))

                .ForMember(target => target.UserRealName,
                opt => opt.MapFrom(src => src.UserRealName))

                 .ForMember(target => target.UserHeadShotPath,
                opt => opt.MapFrom(src => src.UserHeadShotPath))

                 .ForMember(target => target.UserBackgroundColor,
                opt => opt.MapFrom(src => src.UserBackgroundColor))

                 .ForMember(target => target.UserIntroduction,
                opt => opt.MapFrom(src => src.UserIntroduction))

                 .ForMember(target => target.UserEmail,
                opt => opt.MapFrom(src => src.UserEmail))

                  .ForMember(target => target.UserDateTime,
                opt => opt.MapFrom(src => src.UserDateTime));

        }
    }
}
