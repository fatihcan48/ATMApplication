using ATMApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.DataAccess.EntityFramework.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData( 
                new User
                {
                    Id = 1, FirstName = "null", LastName = "null", AccountNo = "null",
                    Password="null", AccountBalance = 0
                },
                new User
                {   Id = 2, FirstName = "Fatih", LastName="Can" , AccountNo = "10000001",
                    Password ="1234", AccountBalance=25000
                },

                new User{
                    Id = 3, FirstName = "Yusuf", LastName="Asaf" , AccountNo = "10000002",
                    Password ="4321", AccountBalance=45000
                },
                new User{
                    Id = 4, FirstName = "Ahmet", LastName="Can" ,  AccountNo = "10000003",
                    Password ="1111", AccountBalance=5000
                });
        }
    }
}
