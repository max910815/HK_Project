using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HK_project.Interface;
using HK_Product.Data;
using HK_project.Models;
using HK_project.ViewModels;

namespace HK_Product.Services
{
    public class AccountServices
    {

        private readonly HKContext _ctx;
        private readonly IHashService _hashService;

        public AccountServices(HKContext context, IHashService hashService)
        {
            _ctx = context;
            _hashService = hashService;
        }

        public async Task<Member> AuthenticateUser(LoginViewModel loginVM)
        {
            //find user
            // _hashService.MD5Hash(loginVM.Password)
            var member = await _ctx.Member
                .FirstOrDefaultAsync(u => u.MemberEmail.ToUpper() == loginVM.Email.ToUpper() && u.MemberPassword == _hashService.MD5Hash( loginVM.Password));

            if (member != null)
            {
                Member userInfo = new Member
                {
                    MemberId = member.MemberId,
                    MemberName = member.MemberId,
                    MemberEmail = loginVM.Email,
                    MemberPassword = loginVM.Password,
                };

                return userInfo;
            }
            else
            {
                return null;
            }
        }
    }
}
