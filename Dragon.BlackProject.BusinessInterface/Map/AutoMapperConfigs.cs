using AutoMapper;
using Dragon.BlackProject.ModelDtos.User;
using Dragon.BlackProject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon.BlackProject.BusinessInterface.Map
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs()
        {
            //CreateMap<source,destination>();

            CreateMap<Sys_User, SysUserInfo>();
        }
    }
}
