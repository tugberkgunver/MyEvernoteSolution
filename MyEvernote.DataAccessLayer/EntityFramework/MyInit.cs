using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MyEvernote.Entities;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
   public class MyInit : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Tugberk",
                Surname = "Gunver",
                Email = "tugberkgunver@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = true,
                Username = "tugberkgunver",
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUser = "tugberkgunver"
            };

            EvernoteUser standart = new EvernoteUser()
            {
                Name = "Ezgi",
                Surname = "Gunver",
                Email = "tugberkkgunver@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                Username = "ezgigunver",
                Password = "654321",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(10),
                ModifiedUser = "tugberkgunver"
            };

            context.EvernoteUsers.Add(admin);
            context.EvernoteUsers.Add(standart);
            context.SaveChanges();
            


        }
    }
}
