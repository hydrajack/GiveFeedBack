using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeadBody.Centers;
using DeadBody.Models.db;
using DeadBody.Parameter;
using DeadBody.Tools;
using Microsoft.AspNetCore.Mvc;
using DeadBody.Dto;
using AutoMapper;
using System.Net.Mail;
using DeadBody.UsersParameter;

namespace DeadBody.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly UsersCenter UsersCenter;
        private readonly FileTool FileTool;
        private readonly MDFive HashTool;
        private readonly IMapper Mapper;

        public UsersController( UsersCenter userCenter,FileTool fileTool,MDFive hashTool,IMapper mapper)
        {
            FileTool = fileTool;
            UsersCenter = userCenter;
            HashTool = hashTool;
            Mapper = mapper;
        }
        // GET: 取得會員資料
        [HttpGet("{userId}")]
        public ActionResult GetUserInfo(int userId)
        {
            var userTuples = UsersCenter.GetUserInfoByUserId(userId);
            if(userTuples is null)
            {
                return NotFound("查無相關會員流水號");
            }
            var userDto = Mapper.Map<UserDto>(userTuples);
            return Ok(userDto);

        }

      

        // 註冊
        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] SignUpParameter signUpParameter )
        {
            var RepeatAccount = UsersCenter.GetUserInfoByUserAccount(signUpParameter.UserAccount);
            var RepeatEmail = UsersCenter.GetUserInfoByEmail(signUpParameter.UserEmail);
            if(RepeatAccount is  not null)
            {
                return BadRequest("已有重複帳號，請重新命名");
            }
            if(RepeatEmail is not null)
            {
                return BadRequest("已有重複email帳號進行註冊，請重新輸入");
            }
           string hash = HashTool.MD5password(signUpParameter.UserPassword);
            var NewUser = new User
            {
                UserAccount = signUpParameter.UserAccount,
                 UserPassword = hash,
                UserRealName = signUpParameter.UserRealName,            
                UserEmail = signUpParameter.UserEmail

            };
            UsersCenter.AddNewUser(NewUser);
            UsersCenter.SaveChanges();
            var userDto = Mapper.Map<UserDto>(NewUser);
            return CreatedAtAction(nameof(GetUserInfo), new { userId = userDto.UserId }, userDto);

        }
        //登入
        [HttpPost("signin")]
        public ActionResult PostSignIn([FromBody] SignInParameter signInParameter)
        {
             string hash = HashTool.MD5password(signInParameter.UserPassword);
            var user = UsersCenter.GetUserInfoByAccountPassword(signInParameter.UserAccount, hash);
            if (user is null)
            {
                return BadRequest("輸入了錯誤的帳號或密碼");
            }
            var userDto = Mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUserInfo), new { userId = userDto.UserId }, userDto);
        }



     
        // PUT api/<UsersController>/5
        [HttpPut("{userId}")]
        public ActionResult PutUserInfo(int userId, [FromForm] UserEditParameter parameter)
        {
            var user = UsersCenter.GetUserInfoByUserId(userId);
            if (user is null)
            {
                return BadRequest("查無此相關流水號的會員");
            }
            if(parameter.UserRealName is not null)
            {
                user.UserRealName = parameter.UserRealName;
            }
            if (parameter.HeadShotFile is not null)
            {
                FileTool.DeleteFile(user.UserHeadShotPath);             
                user.UserHeadShotPath = FileTool.SaveImageFile(parameter.HeadShotFile, "UserHeadShot", user.UserId, 0);
            }
            if (parameter.UserIntroduction is not null)
            {
                user.UserIntroduction = parameter.UserIntroduction;
            }
            if(parameter.UserBackgroundColor is not null)
            {
                user.UserBackgroundColor = parameter.UserBackgroundColor;
            }
            if (parameter.UserEmail is not null)
            {
                user.UserEmail=parameter.UserEmail;
            }

            UsersCenter.SaveChanges();
            var userDto = Mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUserInfo), new { userId = userDto.UserId }, userDto);

        }
        [HttpPut("{userId}/password")]
        public ActionResult PutUserPassword(int userId ,UserEditPasswordParameter parameter)
        {
            var user = UsersCenter.GetUserInfoByUserId(userId);
            if(user is null)
            {
                return NotFound("查無此相關流水號的會員");
            }
             string hashOld = HashTool.MD5password(parameter.OldPassword);
            if (!user.UserPassword.Equals(hashOld))
            {
                return Unauthorized("原本的密碼輸入錯誤，請重試");
            }
            if (!parameter.NewPassword.Equals(parameter.NewPasswordAgain))
            {
                return BadRequest("請確認新密碼輸入以及再次確認新密碼是否正確一致");

            }
             
            string hashNew = HashTool.MD5password(parameter.NewPassword);
            user.UserPassword = hashNew;
            UsersCenter.SaveChanges();
            var userDto = Mapper.Map<UserDto>(user);
            return CreatedAtAction(nameof(GetUserInfo), new { userId = userDto.UserId }, userDto);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/HeadShot")]
        public ActionResult DeleteUserHeadShot(int userId)
        {
            var user = UsersCenter.GetUserInfoByUserId(userId);
            if (user is null)
            {
                return NotFound("查無相關會員流水號");
            }
            if(user.UserHeadShotPath is null)
            {
                return NotFound("此會員沒有存妳想刪的照片");
            }
            FileTool.DeleteFile(user.UserHeadShotPath);
            user.UserHeadShotPath = null;
            UsersCenter.SaveChanges();
            return NoContent();

        }

        [HttpPost("forgotPassword")]
        public ActionResult ForgotPassWord([FromBody] ForgetPassword parameter)
        {
            var user = UsersCenter.GetUserByAccountEmail(parameter.UserAccount,parameter.UserEmail);
            if (user is null)
            {
                return NotFound("錯誤的帳號，或錯誤的信箱地址");
            }
            Random random = new Random();
            string password = random.Next(100000, 999999).ToString();
            user.UserPassword = HashTool.MD5password(password);
            UsersCenter.SaveChanges();
            MailMessage mail = new MailMessage();
            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("henianlin85888@gmail.com", "溫水主程式-忘記密碼信");

            //收信者email
            mail.To.Add(@$"{user.UserEmail}"); 
            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "AutoEmail";

            //內容
            mail.Body = $"<h3>{user.UserRealName}您好，您的帳號密碼已被重置為{password}，請重新登入後再次修改您的密碼。</h3>";

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("henianlin85888@gmail.com", "Jack19968588");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();
            return Ok();
        }
 




    }
}
