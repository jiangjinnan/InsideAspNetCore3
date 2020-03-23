using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class Profile : IEquatable<Profile>
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public Profile() { }
        public Profile(Gender gender, int age, string emailAddress, string phoneNo)
        {
            Gender = gender;
            Age = age;
            ContactInfo = new ContactInfo
            {
                EmailAddress = emailAddress,
                PhoneNo = phoneNo
            };
        }
        public bool Equals(Profile other)
        {
            return other == null
                ? false
                : Gender == other.Gender &&
                  Age == other.Age &&
                  ContactInfo.Equals(other.ContactInfo);
        }
    }

}
